using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCrowbar : GetObject
{
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		_event.hasCrowbar = true;
	}
}
