using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "024ba9d225e0acdc6d3189c9be43c232c047a5b6")]
public class ComponentCheckInherit : ComponentCheck
{
	void Init()
	{
		// write here code to be called on component initialization
		Log.Message("Derived Init\n");
	}
	
	void Update()
	{
		// write here code to be called before updating each render frame
		
	}
}