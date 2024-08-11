using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "ee0a9d94d0f8fa475d69f3fac7cdecd855f638ce")]
public class ChessMovement : Component
{
	quat Rotate = new(0, 0, 15), RRotate = new(0, 0, -15);

    void Update()
	{
		// write here code to be called before updating each render frame
		//Movement();
		//Rot();
		//Combined();
		//FixedMovement();
		//FixedCombinedMovement();
	}

	public void MoveUp() => node.WorldPosition += node.GetWorldDirection(MathLib.AXIS.Y);
	public void MoveDown() => node.WorldPosition += node.GetWorldDirection(MathLib.AXIS.NY);
	public void MoveLeft() => node.WorldPosition += node.GetWorldDirection(MathLib.AXIS.NX);
	public void MoveRight() => node.WorldPosition += node.GetWorldDirection(MathLib.AXIS.X);
	public void RotLeft() => node.WorldRotate(Rotate);
    public void RotRight() => node.WorldRotate(RRotate);

    void Movement()
	{

		if (Input.IsKeyDown(Input.KEY.W)) 
		{ 
			node.WorldPosition += vec3.FORWARD;
			Log.Message("Pressed W, Forward\n");
		}
		if (Input.IsKeyDown(Input.KEY.S)) 
		{ 
			node.WorldPosition += vec3.BACK;
            Log.Message("Pressed S, Backward\n");
        }
		if (Input.IsKeyDown(Input.KEY.A)) 
		{ 
			node.WorldPosition += vec3.LEFT;
            Log.Message("Pressed A, Left\n");
        }
		if (Input.IsKeyDown(Input.KEY.D)) 
		{ 
			node.WorldPosition += vec3.RIGHT;
            Log.Message("Pressed D, Right\n");
        }
	}

	void Rot()
	{
		if (Input.IsKeyDown(Input.KEY.Q)) 
		{
			RotLeft();
            Log.Message("Pressed Q, CounterClockwise Rotation\n");
        }
        if (Input.IsKeyDown(Input.KEY.E)) 
		{
			RotRight();
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
        if (Input.IsKeyDown(Input.KEY.W))
        {
			MoveUp();
			Log.Message("Pressed W, Forward\n");
        }
        if (Input.IsKeyDown(Input.KEY.S))
        {
			MoveDown();
            Log.Message("Pressed S, Backward\n");
        }
        if (Input.IsKeyDown(Input.KEY.A))
        {
			MoveLeft();
            Log.Message("Pressed A, Left\n");
        }
        if (Input.IsKeyDown(Input.KEY.D))
        {
			MoveRight();
            Log.Message("Pressed D, Right\n");
        }
    }

	public void FixedCombinedMovement()
	{
		FixedMovement();
		Rot();
	}
} 