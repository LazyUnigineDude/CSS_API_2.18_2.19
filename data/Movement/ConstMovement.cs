using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "5a48ce051d44fc32ab03d9375e03338e0d48f7d9")]
public class ConstMovement : Component
{
	public Node MoveNode;
    quat Rotate = new(0, 0, 1), RRotate = new(0, 0, -1);
	
	void Update()
	{
        // write here code to be called before updating each render frame

        //Movement();
        //Rot();
        Combined();
	}

	void Movement()
	{
        vec3 Pos;

        if (Input.IsKeyPressed(Input.KEY.W))
        {
            Pos = MoveNode.GetWorldDirection(MathLib.AXIS.Y);
            MoveNode.WorldPosition += Pos * Game.IFps;
            //Log.Message("Pressed W, Forward\n");
        }
        if (Input.IsKeyPressed(Input.KEY.S))
        {
            Pos = MoveNode.GetWorldDirection(MathLib.AXIS.NY);
            MoveNode.WorldPosition += Pos * Game.IFps;
            //Log.Message("Pressed S, Backward\n");
        }
        if (Input.IsKeyPressed(Input.KEY.A))
        {
            Pos = MoveNode.GetWorldDirection(MathLib.AXIS.NX);
            MoveNode.WorldPosition += Pos * Game.IFps;
            //Log.Message("Pressed A, Left\n");
        }
        if (Input.IsKeyPressed(Input.KEY.D))
        {
            Pos = MoveNode.GetWorldDirection(MathLib.AXIS.X);
            MoveNode.WorldPosition += Pos * Game.IFps;
            //Log.Message("Pressed D, Right\n");
        }
    }

    void Rot()
    {
        if (Input.IsKeyPressed(Input.KEY.Q)) 
        { 
            MoveNode.WorldRotate(Rotate);
           // Log.Message("Pressed Q, CounterClockwise Rotation\n");
        }
        if (Input.IsKeyPressed(Input.KEY.E))
        { 
            MoveNode.WorldRotate(RRotate);
           // Log.Message("Pressed E, Clockwise Rotation\n");
        }
    }

    void Combined()
    {
        Movement();
        Rot();
    }
}