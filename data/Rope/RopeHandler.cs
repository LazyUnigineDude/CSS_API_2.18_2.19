using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Unigine;

[Component(PropertyGuid = "85ea0e7d57829f4fd559d1faa76715c34104cee8")]
public class RopeHandler : Component
{
    [ParameterMask(MaskType = ParameterMaskAttribute.TYPE.INTERSECTION)]
	[ShowInEditor]
    protected int GroundMask = 0x01, RopeGrabMask = 0x02, StickPointMask = 0x04;

    WidgetLabel Instructions;

    WorldIntersection GPoint, Point;
    Player Camera = Game.Player;
	dvec3 P0, P1;

    JointFixed FixPoint;
    Unigine.Object _RopeEnd, ConnectedObj;

    enum ROPE_STATE { DANGLING, GRABBED, CONNECTED };
    ROPE_STATE RState = ROPE_STATE.DANGLING;

    void Init()
	{
		// write here code to be called on component initialization
		GPoint = new WorldIntersection();
		Point = new WorldIntersection();
        Input.MouseHandle = Input.MOUSE_HANDLE.USER;

        Instructions = new(
            "Hover over the End and Click to grab rope, can be placed on green points\n" +
            "Hover and Click to place on green box\n" +
            "Right click to release rope while holding\n" +
            "Click when connected to disconnect and grab again"
            );

        Gui GUI = Gui.GetCurrent();
        GUI.AddChild(Instructions);
        Console.Onscreen = true;
    }

    void Update()
	{
        ivec2 MousePos = Input.MousePosition;
        P0 = Camera.WorldPosition;
        P1 = (dvec3)Camera.GetDirectionFromMainWindow(MousePos.x, MousePos.y) * 100;
        P1 += P0;
        Unigine.Object Ground = World.GetIntersection(P0, P1, GroundMask, GPoint);

        switch (RState) {
            case ROPE_STATE.DANGLING:

                Unigine.Object Obj = World.GetIntersection(P0, P1, RopeGrabMask, Point);
                if (Obj) Visualizer.RenderPoint3D(Point.Point, 0.05f, vec4.BLUE);
                if (Obj && Input.IsMouseButtonDown(Input.MOUSE_BUTTON.LEFT)) 
                { 
                    _RopeEnd = Obj;
                    RState = ROPE_STATE.GRABBED;
                }
            break;


            case ROPE_STATE.GRABBED:
                _RopeEnd.WorldPosition = GPoint.Point;
                Obj = World.GetIntersection(P0, P1, StickPointMask, Point);
                
                if (Input.IsMouseButtonDown(Input.MOUSE_BUTTON.RIGHT)) 
                {
                    _RopeEnd = null;
                    RState = ROPE_STATE.DANGLING;
                    return;
                }

                if(Obj) 
                {
                    _RopeEnd.WorldPosition = Obj.WorldPosition;
                    if (Input.IsMouseButtonDown(Input.MOUSE_BUTTON.LEFT))
                    {
                        ConnectedObj = Obj;
                        BodyRigid _rb = _RopeEnd.BodyRigid;
                        BodyDummy _rc = ConnectedObj.Body as BodyDummy;
                        FixPoint = new JointFixed();
                        FixPoint.Body0 = _rb;
                        FixPoint.Body1 = _rc;

                        _rb.AddJoint(FixPoint);
                        _rc.AddJoint(FixPoint);
                        RState = ROPE_STATE.CONNECTED;
                        return;
                    }
                }
            break;


            case ROPE_STATE.CONNECTED:

                Obj = World.GetIntersection(P0, P1, StickPointMask, Point);
                if (Obj) Visualizer.RenderPoint3D(Point.Point, 0.05f, vec4.BLUE);
                if (Obj && Obj == ConnectedObj && Input.IsMouseButtonDown(Input.MOUSE_BUTTON.LEFT))
                {
                    FixPoint.DeleteLater();
                    ConnectedObj = null;
                    RState = ROPE_STATE.GRABBED;
                    return;
                }
            break;


            default: break;
        }
    }

	void Shutdown()
	{
		GPoint.DeleteLater();
		Point.DeleteLater();
        Gui GUI = Gui.GetCurrent();
        if (GUI.IsChild(Instructions) == 1) { GUI.RemoveChild(Instructions); }
        Instructions.DeleteLater();
    }
}