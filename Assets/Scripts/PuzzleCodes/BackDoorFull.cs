using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackDoorFull : OpenPuzzle
{
	[SerializeField] GameObject cabinetCollider;

	public override void OnMouseDown()
	{
		_event.PuzzlesOpened++;
		PopupWindow.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, PopupWindow.transform.position.z);
		PopupWindow.SetActive(true);
		cabinetCollider.SetActive(false);
		this.gameObject.SetActive(false);
	}
}
