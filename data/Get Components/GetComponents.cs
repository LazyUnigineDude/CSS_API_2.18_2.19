using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "80e2c191987045453e40a0030e634002758de119")]
public class GetComponents : Component
{

	public Node NodeItem;
	public ChessMovement _Movement;

	void Init()
	{
		// write here code to be called on component initialization

		//1. Component Click and Drag

		//2. World ID
		Node _Node = World.GetNodeByID(469325426);
		Log.Message($"Caught {_Node.Name}, ID: {_Node.ID}\n");

		//3. World Name;
		_Node = World.GetNodeByName("MatBall");
		Log.Message($"Caught {_Node.Name}, ID: {_Node.ID}\n");

		//4. Get Custom Component via Step 1
		_Movement = GetComponent<ChessMovement>(NodeItem);

		//5. Get Components
		Unigine.Object Obj = NodeItem as Unigine.Object;

		if (Obj.ClutterInteractionEnabled) Log.Message("Clutter Intersection is Enabled\n");
		else Log.Message("Clutter Intersection is Disabled\n");
	}

    void Update()
	{
		// write here code to be called before updating each render frame

		if (_Movement) _Movement.FixedCombinedMovement();
	}
}