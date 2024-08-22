using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "f18d254471ec733d447d5ca601151c05710aa908")]
public class ChessGUI : Component
{
	[ShowInEditor]
	Node ChessNode;
	ChessMovement ChessLogic;

	WidgetButton Up, Down, Left, Right, RLeft, RRight;

	void ClickUp(Widget Button) => ChessLogic.MoveUp();
    void ClickDown(Widget Button) => ChessLogic.MoveDown();
	void ClickLeft(Widget Button) => ChessLogic.MoveLeft();
	void ClickRight(Widget Button) => ChessLogic.MoveRight();
	void ClickRLeft(Widget Button) => ChessLogic.RotLeft();
	void ClickRRight(Widget Button) => ChessLogic.RotRight();
    
	void Enter(Widget widget)
	{
		WidgetButton widgetButton = widget as WidgetButton;
		if (widgetButton != null) { widgetButton.ButtonColor = vec4.RED; }
	}

	void Leave(Widget widget)
	{
        WidgetButton widgetButton = widget as WidgetButton;
        if (widgetButton != null) { widgetButton.ButtonColor = vec4.WHITE; }
    }

	void Init()
	{
		ChessLogic = GetComponent<ChessMovement>(ChessNode);

		Up = new WidgetButton("UP");
		Down = new WidgetButton("DOWN");
		Left = new WidgetButton("LEFT");
		Right = new WidgetButton("RIGHT");
		RLeft = new WidgetButton("ROTATE LEFT");
		RRight = new WidgetButton("ROTATE RIGHT");

		Up.SetPosition(250, 125);
        Down.SetPosition(250, 175);
        Left.SetPosition(200, 150);
        Right.SetPosition(300, 150);
        RLeft.SetPosition(150, 100);
        RRight.SetPosition(300, 100);

        Up.EventEnter.Connect(Enter);
        Up.EventLeave.Connect(Leave);
        Up.EventClicked.Connect(ClickUp);


        Down.EventEnter.Connect(Enter);
        Down.EventLeave.Connect(Leave);
        Down.EventClicked.Connect(ClickDown);


        Left.EventEnter.Connect(Enter);
        Left.EventLeave.Connect(Leave);
        Left.EventClicked.Connect(ClickLeft);


        Right.EventEnter.Connect(Enter);
        Right.EventLeave.Connect(Leave);
        Right.EventClicked.Connect(ClickRight);


        RLeft.EventEnter.Connect(Enter);
        RLeft.EventLeave.Connect(Leave);
        RLeft.EventClicked.Connect(ClickRLeft);


        RRight.EventEnter.Connect(Enter);
        RRight.EventLeave.Connect(Leave);
        RRight.EventClicked.Connect(ClickRRight);

        Gui GUI = Gui.GetCurrent();
        GUI.AddChild(Up, Gui.ALIGN_EXPAND | Gui.ALIGN_OVERLAP);
        GUI.AddChild(Down, Gui.ALIGN_EXPAND | Gui.ALIGN_OVERLAP);
        GUI.AddChild(Left, Gui.ALIGN_EXPAND | Gui.ALIGN_OVERLAP);
        GUI.AddChild(Right, Gui.ALIGN_EXPAND | Gui.ALIGN_OVERLAP);
        GUI.AddChild(RLeft, Gui.ALIGN_EXPAND | Gui.ALIGN_OVERLAP);
        GUI.AddChild(RRight, Gui.ALIGN_EXPAND | Gui.ALIGN_OVERLAP);
    }

    void Shutdown()
    {
        Gui GUI = Gui.GetCurrent();
        if (GUI.IsChild(Up) == 1) GUI.RemoveChild(Up);
        if (GUI.IsChild(Down) == 1) GUI.RemoveChild(Down);
        if (GUI.IsChild(Left) == 1) GUI.RemoveChild(Left);
        if (GUI.IsChild(Right) == 1) GUI.RemoveChild(Right);
        if (GUI.IsChild(RLeft) == 1) GUI.RemoveChild(RLeft);
        if (GUI.IsChild(RRight) == 1) GUI.RemoveChild(RRight);

        Up.DeleteLater();
        Down.DeleteLater();
        Left.DeleteLater();
        Right.DeleteLater();
        RLeft.DeleteLater();
        RRight.DeleteLater();
    }
}