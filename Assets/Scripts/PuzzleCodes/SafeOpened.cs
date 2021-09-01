using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeOpened : OpenPuzzle
{
	public override void OnMouseDown()
	{
		if(!_event.dialogueBoxOpen && _event.PuzzlesOpened == 1 && !_event.isTransitioning && !_event.isDead && !_event.GameisPaused)
		{
			_event.PuzzlesOpened++;
			PopupWindow.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, PopupWindow.transform.position.z);
			PopupWindow.SetActive(true);
		}
	}
}
