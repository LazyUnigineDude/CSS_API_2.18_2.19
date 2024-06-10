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

        //World ID
        Node _Node = World.GetNodeByID(469325426);
		Log.Message($"Caught {_Node.Name}, ID: {_Node.ID}\n");

        //World Name
        _Node = World.GetNodeByName("MatBall");
		Log.Message($"Caught {_Node.Name}, ID: {_Node.ID}\n");

        //Get Custom Component from NodeItem
        _Movement = GetComponent<ChessMovement>(NodeItem);

        //Get Unigine Component from the Node
        Unigine.Object Obj = NodeItem as Unigine.Object;

        // Example
        if (Obj.ClutterInteractionEnabled)
            Log.Message("Clutter Intersection is Enabled\\n");
        else
            Log.Message("Clutter Intersection is Disabled\\n");
    }

    void Update()
	{

		if (_Movement) _Movement.FixedCombinedMovement();
	}
}