using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "64cb64cbe8499ee471b85ad5e0f055a3c92089ee")]
public class MapMaker : Component
{
	[ShowInEditor]
	Player MapCamera;

	[ShowInEditor]
	Node FollowObj;

    WidgetSpriteViewport Map;
	vec3 Offset = vec3.UP * 50;
	
	void Init()
	{
		// write here code to be called on component initialization
		Map = new WidgetSpriteViewport(200, 200);
        Gui GUI = Gui.GetCurrent();

        Map.SetPosition(GUI.Width - 215, 15);
        GUI.AddChild(Map, Gui.ALIGN_EXPAND | Gui.ALIGN_OVERLAP);
    }

    void Update()
	{
		// write here code to be called before updating each render frame
		Map.SetCamera(MapCamera.Camera);
		MapCamera.WorldPosition = FollowObj.WorldPosition + Offset;
	}

	void Shutdown()
	{
        Gui GUI = Gui.GetCurrent();
		if (GUI.IsChild(Map) == 1) GUI.RemoveChild(Map);
		Map.DeleteLater();
    }
}