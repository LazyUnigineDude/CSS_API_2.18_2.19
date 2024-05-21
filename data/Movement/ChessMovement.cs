using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "ee0a9d94d0f8fa475d69f3fac7cdecd855f638ce")]
public class ChessMovement : Component
{
	public Node MoveObj;
	quat Rotate = new(0, 0, 15), RRotate = new(0, 0, -15);

    void Update()
	{
		// write here code to be called before updating each render frame
		//Movement();
		//Rot();
		//Combined();
		//FixedMovement();
		FixedCombinedMovement();
	}

	void Movement()
	{

		if (Input.IsKeyDown(Input.KEY.W)) 
		{ 
			MoveObj.WorldPosition += vec3.FORWARD;
			Log.Message("Pressed W, Forward\n");
		}
		if (Input.IsKeyDown(Input.KEY.S)) 
		{ 
			MoveObj.WorldPosition += vec3.BACK;
            Log.Message("Pressed S, Backward\n");
        }
		if (Input.IsKeyDown(Input.KEY.A)) 
		{ 
			MoveObj.WorldPosition += vec3.LEFT;
            Log.Message("Pressed A, Left\n");
        }
		if (Input.IsKeyDown(Input.KEY.D)) 
		{ 
			MoveObj.WorldPosition += vec3.RIGHT;
            Log.Message("Pressed D, Right\n");
        }
	}

	void Rot()
	{
		if (Input.IsKeyDown(Input.KEY.Q)) 
		{ 
			MoveObj.WorldRotate(Rotate); 
            Log.Message("Pressed Q, CounterClockwise Rotation\n");
        }
        if (Input.IsKeyDown(Input.KEY.E)) 
		{ 
			MoveObj.WorldRotate(RRotate);
            Log.Message("Pressed E, Clockwise Rotation\n");
        }
    }

	void Combined()
	{
		Movement();
		Rot();
	}

	void FixedMovement()
	{
		vec3 Pos;

        if (Input.IsKeyDown(Input.KEY.W))
        {
            Pos = MoveObj.GetWorldDirection(MathLib.AXIS.Y);
            MoveObj.WorldPosition += Pos;
			Log.Message("Pressed W, Forward\n");
        }
        if (Input.IsKeyDown(Input.KEY.S))
        {
            Pos = MoveObj.GetWorldDirection(MathLib.AXIS.NY);
            MoveObj.WorldPosition += Pos;
            Log.Message("Pressed S, Backward\n");
        }
        if (Input.IsKeyDown(Input.KEY.A))
        {
            Pos = MoveObj.GetWorldDirection(MathLib.AXIS.NX);
            MoveObj.WorldPosition += Pos;
            Log.Message("Pressed A, Left\n");
        }
        if (Input.IsKeyDown(Input.KEY.D))
        {
            Pos = MoveObj.GetWorldDirection(MathLib.AXIS.X);
            MoveObj.WorldPosition += Pos;
            Log.Message("Pressed D, Right\n");
        }
    }

	void FixedCombinedMovement()
	{
		FixedMovement();
		Rot();
	}
} 