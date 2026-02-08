using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "73a03155ddef2c5bc2f82c40b2c4d68ccd0ec9d3")]
public class SimpleDetect : Component
{
	[ParameterMask(MaskType = ParameterMaskAttribute.TYPE.INTERSECTION)]
	public int MaskVal = 0x02;

	protected WorldIntersection Ptr;
	protected WorldIntersectionNormal NPtr;
	protected WidgetLabel Label;
	protected dvec3 P0, P1;
	dvec3 Offset = new dvec3(0, 0, 0.5);

    protected void InitPtr()
	{
		Ptr = new WorldIntersection();
		NPtr = new WorldIntersectionNormal();
	}

	protected void InitLabel()
	{
		Label = new WidgetLabel();
		Label.FontSize = 21;
		Label.FontOutline = 2;

		Gui GUI = Gui.GetCurrent();
		GUI.AddChild(Label, Gui.ALIGN_CENTER | Gui.ALIGN_OVERLAP);
	}

	protected void SetPoints(MathLib.AXIS Axis, int Distance, Node Node)
	{
		P0 = Node.WorldPosition;
		P1 = P0 +  (dvec3)Node.GetWorldDirection(Axis) * Distance;
	}

	void Init()
	{
		// write here code to be called on component initialization
		InitPtr();
		SetPoints(MathLib.AXIS.Y, 10, node);

		P0 += Offset;
		P1 += Offset;

		InitLabel();
    }
	
	void Update()
	{
		// write here code to be called before updating each render frame

		Unigine.Object Obj = World.GetIntersection(P0, P1, MaskVal, /*Ptr*/ NPtr);

        if (Obj != null)
		{
			Label.Text = Obj.Name;
			Label.FontColor = vec4.GREEN;

			//// For Point Only
			//Visualizer.RenderPoint3D(Ptr.Point, 0.05f, vec4.GREEN);

			//// With Normal
			Visualizer.RenderVector(NPtr.Point, NPtr.Point + (dvec3)NPtr.Normal, vec4.GREEN);
        }
		else
		{
			Label.Text = "Caught Nothing";
			Label.FontColor = vec4.RED;
		}

		Visualizer.RenderLine3D(P0, P1, vec4.BLUE);
    }

	void Shutdown()
	{
		if (Ptr != null) Ptr.Dispose();
		if (NPtr != null) NPtr.Dispose();

		Gui GUI = Gui.GetCurrent();
		if (GUI.IsChild(Label) == 1) GUI.RemoveChild(Label);
		if (Label != null)			 Label.DeleteLater();
	}
}