using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "e0664521c7a758bfd1ccb307ed512fbd90a1412f")]
public class TriggerExample : Component
{
	[ShowInEditor]
	Node WT, NT, PT;

	WorldTrigger _WT;
	PhysicalTrigger _PT;
	NodeTrigger _NT;

	WidgetLabel Label;
	EventConnection EC, LC;

	void Move(NodeTrigger Node)
	{
		if (Label != null) { Label.Text = string.Format("{0:.##} {1:.##}", Node.WorldPosition.x, Node.WorldPosition.y); }
	}

	static void EnterPhysical(Body body) => Log.Message("Enter Physics\n");
	static void LeavePhysical(Body body) => Log.Message("Leave Physics\n");

	static void Enter(Node Node) => Log.Message($"Enter {Node.Name}\n");
	static void Leave(Node Node) => Log.Message($"Leave {Node.Name}\n");

    void Init()
	{
		// write here code to be called on component initialization

		Label = new WidgetLabel();
		Label.FontSize = 21;
		Label.FontColor = vec4.GREEN;
		Label.FontOutline = 2;

		Gui GUI = Gui.GetCurrent();
		GUI.AddChild(Label, Gui.ALIGN_CENTER);

		_NT = NT as NodeTrigger;
		_PT = PT as PhysicalTrigger;
		_WT = WT as WorldTrigger;

		if (_NT != null) { _NT.EventPosition.Connect(Move); }
		if (_PT != null)
		{
			_PT.EventEnter.Connect(body => EnterPhysical(body));
			_PT.EventLeave.Connect(body => LeavePhysical(body));
		}  	
		if (_WT != null)
		{
            EC = _WT.EventEnter.Connect(Enter);
			LC = _WT.EventLeave.Connect(Leave);
        }
	}
	
	void Update()
	{
		// write here code to be called before updating each render frame
		if (_PT != null) _PT.RenderVisualizer();
		if (_WT != null) _WT.RenderVisualizer();

        if (Input.IsKeyDown(Input.KEY.U)) { EC.Enabled = (EC.Enabled) ? false : true; }
        if (Input.IsKeyDown(Input.KEY.I)) { LC.Enabled = (LC.Enabled) ? false : true; }
	}

	void Shutdown()
	{
		Gui GUI = Gui.GetCurrent();
		if(GUI.IsChild(Label) == 1) { GUI.RemoveChild(Label); }
		Label.DeleteLater();
	}
}