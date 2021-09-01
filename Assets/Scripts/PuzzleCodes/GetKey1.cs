using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKey1 : GetObject
{
	public override void OnMouseDown()
	{
		if(!_event.dialogueBoxOpen)
		{
			_event.KeyCount++;
		}
		base.OnMouseDown();
	}
}
