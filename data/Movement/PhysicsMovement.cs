using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "77b7ae4e53c344a2c58387e492c9ef76139536d8")]
public class PhysicsMovement : Component
{
	public Node MoveObj;
	BodyRigid _Physics;
    float Speed = 10;

	void Init() => _Physics = MoveObj.ObjectBodyRigid;

	void UpdatePhysics()
	{
        // write here code to be called before updating each render frame
        //Movement();
        //Rot();
        //FixedMovement();
        Combined();
	}

    float GetSpeed(vec3 Vel) { float Speed = MathLib.Length2(Vel); return MathLib.Sqrt(Speed); }

	void Movement()
	{
        if (Input.IsKeyPressed(Input.KEY.W))
        {
            _Physics.AddLinearImpulse(vec3.FORWARD);
            Log.Message("Pressed W, Forward\n");
        }
        if (Input.IsKeyPressed(Input.KEY.S))
        {
            _Physics.AddLinearImpulse(vec3.BACK);
            Log.Message("Pressed S, Backward\n");
        }
        if (Input.IsKeyPressed(Input.KEY.A))
        {
            _Physics.AddLinearImpulse(vec3.LEFT);
            Log.Message("Pressed A, Left\n");
        }
        if (Input.IsKeyPressed(Input.KEY.D))
        {
            _Physics.AddLinearImpulse(vec3.RIGHT);
            Log.Message("Pressed D, Right\n");
        }
    }

    void FixedMovement()
    {
        float _Speed = Speed * Physics.IFps;

        if (Input.IsKeyPressed(Input.KEY.W))
        {
            _Physics.AddLinearImpulse(MoveObj.GetWorldDirection(MathLib.AXIS.Y) * _Speed);
            Log.Message("Pressed W, Forward\n");
        }
        if (Input.IsKeyPressed(Input.KEY.S))
        {
            _Physics.AddLinearImpulse(MoveObj.GetWorldDirection(MathLib.AXIS.NY) * _Speed);
            Log.Message("Pressed S, Backward\n");
        }
        if (Input.IsKeyPressed(Input.KEY.A))
        {
            _Physics.AddLinearImpulse(MoveObj.GetWorldDirection(MathLib.AXIS.NX) * _Speed);
            Log.Message("Pressed A, Left\n");
        }
        if (Input.IsKeyPressed(Input.KEY.D))
        {
            _Physics.AddLinearImpulse(MoveObj.GetWorldDirection(MathLib.AXIS.X) * _Speed);
            Log.Message("Pressed D, Right\n");
        }

       //Log.Message($"Speed: {GetSpeed(_Physics.LinearVelocity)}\n");
    }

    void Rot()
    {
        if (Input.IsKeyPressed(Input.KEY.Q))
        {
            _Physics.AddAngularImpulse(-MoveObj.GetWorldDirection() * Physics.IFps);
            Log.Message("Pressed Q, CounterClockwise Rotation\n");
        }
        if (Input.IsKeyPressed(Input.KEY.E))
        {
            _Physics.AddAngularImpulse(MoveObj.GetWorldDirection() * Physics.IFps);
            Log.Message("Pressed E, CounterClockwise Rotation\n");
        }
    }

    void Combined()
    {
        FixedMovement();
        Rot();
    }
}