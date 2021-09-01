using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackDoorClose : MonoBehaviour
{
	[SerializeField] GameObject PopupWindow;
	[SerializeField] GameObject BackDoorCollider;
	[SerializeField] GameObject CabinetCollider;

	[HideInInspector] Event _event;

	private void Start()
	{
		_event = FindObjectOfType<Event>();
	}

	private void Update()
	{
		ClosePuzzleEscape();
	}

	void OnMouseDown()
	{
		_event.PuzzlesOpened--;
		BackDoorCollider.SetActive(true);
		CabinetCollider.SetActive(true);
		PopupWindow.SetActive(false);
	}

	void ClosePuzzleEscape()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			_event.PuzzlesOpened--;
			BackDoorCollider.SetActive(true);
			CabinetCollider.SetActive(true);
			PopupWindow.SetActive(false);
		}
	}
}
