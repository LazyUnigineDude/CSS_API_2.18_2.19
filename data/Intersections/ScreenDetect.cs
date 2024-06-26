using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "d1e46db15dfec96c65210c6c9888d4b89e0cff4f")]
public class ScreenDetect : CameraDetect
{
	[ParameterMask(MaskType = ParameterMaskAttribute.TYPE.INTERSECTION)]
	protected int GroundMask = 0x01;

	void Init()
	{
		// write here code to be called on component initialization
		InitPtr();
		InitLabel();
		Label.FontColor = vec4.GREEN;

		Input.MouseHandle = Input.MOUSE_HANDLE.SOFT;

		// If you dont have reference to the main Camera
		Camera = Game.Player;

		//// If you have reference to the main Camera
		//Camera = CameraNode as Player;

		//// If you attach this Component to the main Camera
		//Camera = node as Player;
    }

    void Update()
	{
		// write here code to be called before updating each render frame
		
		ivec2 MousePos = Input.MousePosition;
		P0 = Camera.WorldPosition;
		P1 = (dvec3)Camera.GetDirectionFromMainWindow(MousePos.x, MousePos.y) * 100;
		P1 += P0;

		Unigine.Object Ground = World.GetIntersection(P0, P1, GroundMask, Ptr);
		if (Ground != null )
		{
			Label.Text = "Caught Ground";
			Visualizer.RenderPoint3D(Ptr.Point, 0.05f, vec4.RED);
		}

		Unigine.Object Obj = World.GetIntersection(P0, P1, MaskVal, NPtr);
		if ( Obj != null )
		{
			Label.Text = Obj.Name;
			Visualizer.RenderPoint3D(NPtr.Point, 0.05f, vec4.GREEN);
			Visualizer.RenderVector(NPtr.Point, NPtr.Point + (dvec3)NPtr.Normal, vec4.GREEN);
		}
	}
}