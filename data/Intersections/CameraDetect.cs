using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "33591e4352c0b72f8b009d446e02ff64b61a557a")]
public class CameraDetect : SimpleDetect
{
	[ShowInEditor]
	protected Node CameraNode;
	protected Player Camera;

	void Init()
	{
		// write here code to be called on component initialization
		Camera = CameraNode as Player;
	}
	
	void Update()
	{
		// write here code to be called before updating each render frame

		SetPoints(MathLib.AXIS.NZ, 2, CameraNode);
		Visualizer.RenderVector(P0, P1, vec4.BLACK);
		Visualizer.RenderFrustum(Camera.Projection, Camera.WorldTransform, vec4.WHITE);
	}
}