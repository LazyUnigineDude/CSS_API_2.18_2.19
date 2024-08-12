using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "f18d254471ec733d447d5ca601151c05710aa908")]
public class ChessGUI : Component
{
	[ShowInEditor]
	Node ChessNode;

	ChessMovement ChessLogic;
	WidgetButton Button;
    WidgetSlider Slider;
	WidgetCanvas Canvas;

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
		// write here code to be called on component initialization
		Button = new WidgetButton();
		Button.Width = 50;
		Button.FontSize = 21;
		Button.SetPosition(400, 400);
		Button.FontColor = vec4.RED;
		Button.ButtonColor = vec4.BLACK;
		Button.Order = 1;

		Button.EventClicked.Connect(x => Log.Message("Clicked\n"));
		Button.EventEnter.Connect(Enter);
		Button.EventLeave.Connect(Leave);

		Slider = new();
		Slider.Orientation = 0;
		Slider.SetPosition(200, 250);
		Slider.ButtonColor = vec4.BLUE;
		Slider.EventChanged.Connect(x => { Log.Message($"{Slider.Value}\n"); });

		Canvas = new();
		Canvas.SetPosition(400, 400);

		int sq = Canvas.AddPolygon(0);
		Canvas.SetPolygonColor(sq, vec4.GREEN);
		Canvas.AddPolygonPoint(sq, vec3.ZERO);
		Canvas.AddPolygonPoint(sq, vec3.LEFT * 200);
		Canvas.AddPolygonPoint(sq, vec3.FORWARD * 200);
		Canvas.AddPolygonPoint(sq, (vec3.FORWARD + vec3.LEFT) * 200);

		int line = Canvas.AddLine(1);
		Canvas.SetLineColor(line, vec4.RED);
		Canvas.AddLinePoint(line, new vec2(10, 200));
		Canvas.AddLinePoint(line, new vec2(50, 150));

		int text = Canvas.AddText(2);
		Canvas.SetTextSize(text, 26);
		Canvas.SetTextText(text, "Hello");
		Canvas.SetTextColor(text, vec4.BLUE);
		Canvas.SetTextPosition(text, vec2.ONE * 100);

		Unigine.Gui GUI = Gui.GetCurrent();
		GUI.AddChild(Button, Gui.ALIGN_EXPAND | Gui.ALIGN_OVERLAP);
		GUI.AddChild(Slider, Gui.ALIGN_EXPAND | Gui.ALIGN_OVERLAP);
		GUI.AddChild(Canvas, Gui.ALIGN_EXPAND | Gui.ALIGN_OVERLAP);
    }

	void Shutdown()
	{
        Unigine.Gui GUI = Gui.GetCurrent();
		if (GUI.IsChild(Button) == 1) GUI.RemoveChild(Button);
		if (GUI.IsChild(Slider) == 1) GUI.RemoveChild(Slider);
		if (GUI.IsChild(Canvas) == 1) GUI.RemoveChild(Canvas);

		Button.DeleteLater();
        Slider.DeleteLater();
        Canvas.DeleteLater();
    }
}