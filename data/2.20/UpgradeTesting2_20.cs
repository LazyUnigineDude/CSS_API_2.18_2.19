using System.Collections;
using System.Collections.Generic;
using Unigine;
using Unigine.Toolkit;
using Unigine.Toolkit.UI;

[Component(PropertyGuid = "56ae0a605ed6ac5f505433a157e7410041ebc9fc")]
public class UpgradeTesting2_20 : Component
{
	Sprite S;

    void Init()
	{
		S = CppComponentSystem.GetComponent<Sprite>(node);
		Console.Message("{0}\n", S.GetName());
    }

    void Update()
	{
		// write here code to be called before updating each render frame
    }

	void Shutdown()
	{
		S = null;
	}
}