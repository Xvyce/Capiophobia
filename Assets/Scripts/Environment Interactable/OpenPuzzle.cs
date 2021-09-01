using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPuzzle : MonoBehaviour
{
	public GameObject PopupWindow;
	[HideInInspector] public Event _event;

	public virtual void Start()
	{
		PopupWindow.SetActive(false);
		_event = FindObjectOfType<Event>();
	}

	private void OnMouseEnter()
	{
		if (!_event.dialogueBoxOpen && _event.PuzzlesOpened == 0 && !_event.isTransitioning && !_event.isDead && !_event.GameisPaused)
		{
			MouseCursor.instance.ActivateMagnifyingCursor();
		}
	}

	private void OnMouseExit()
	{
		MouseCursor.instance.ActivateNormalCursor();
	}

	public virtual void OnMouseDown()
	{

	}
}
