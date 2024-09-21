using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "31d6c4d5d2d76cab0f580ee2948e7f696d153543")]
public class IKSolver : Component
{
	[ShowInEditor, ParameterMask(MaskType = ParameterMaskAttribute.TYPE.INTERSECTION)]
	int GroundMask;

	[ShowInEditor]
	float AnkleHeight = 0.1f;

	[ShowInEditor]
	List<int> LLeg, RLeg;

	ObjectMeshSkinned Animation;
	int IKLeft, IKRight;
	WorldIntersectionNormal Ptr;

	void Init()
	{
		// write here code to be called on component initialization
		Ptr = new WorldIntersectionNormal();
		Animation = node as ObjectMeshSkinned;

		IKLeft = Animation.AddIKChain();
		foreach(int i in LLeg) { Animation.AddIKChainBone(i, IKLeft); }

		IKRight = Animation.AddIKChain();
		foreach(int i in RLeg) { Animation.AddIKChainBone(i, IKRight); }

		Animation.AddVisualizeIKChain(IKLeft);
        Animation.AddVisualizeIKChain(IKRight);
	}

    void Update()
	{
		// write here code to be called before updating each render frame
		
		Animation.SetIKChainPoleWorldPosition(node.GetChild(0).WorldPosition, IKLeft);
		Animation.SetIKChainPoleWorldPosition(node.GetChild(1).WorldPosition, IKRight);

		AlignFoot(Animation.GetBoneWorldTransform(LLeg[0]), IKLeft);
		AlignFoot(Animation.GetBoneWorldTransform(RLeg[0]), IKRight);
    }

	void Shutdown()
	{
		Ptr.DeleteLater();
	}

	void AlignFoot(dmat4 BoneTransform, int BoneNum)
	{
		Unigine.Object Obj = World.GetIntersection(BoneTransform.Translate, BoneTransform.Translate + (vec3.DOWN * 5), GroundMask, Ptr);
		if (Obj == null) return;

		dvec3 Pos = Ptr.Point;
		quat Rot = BoneTransform.GetRotate();
        
		vec3 Nor = Ptr.Normal;
        Rot *= MathLib.RotationFromTo(vec3.UP, Nor);

		Animation.SetIKChainTargetWorldPosition(Pos + (vec3.UP * AnkleHeight), BoneNum);
		Animation.SetIKChainEffectorRotation(Rot, BoneNum);
	}
}