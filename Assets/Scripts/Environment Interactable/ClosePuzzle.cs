using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePuzzle : MonoBehaviour
{
	public GameObject PopupWindow;
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
		PopupWindow.SetActive(false);
	}

	void ClosePuzzleEscape()
	{
		if(Input.GetKey(KeyCode.Escape))
		{
			_event.PuzzlesOpened--;
			PopupWindow.SetActive(false);
		}
	}
}
