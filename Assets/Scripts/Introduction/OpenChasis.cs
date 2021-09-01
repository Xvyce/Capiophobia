using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChasis : MonoBehaviour
{
	public Dialogue dialogue;

	[SerializeField] GameObject ChasisCover;
	[SerializeField] GameObject Drugs;

	Event _event;

	private void Start()
	{
		_event = FindObjectOfType<Event>();
		Drugs.SetActive(false);
	}

	private void OnMouseEnter()
	{
		if (_event.monitorInteracted && !_event.dialogueBoxOpen && _event.PuzzlesOpened == 0 && !_event.isTransitioning && !_event.GameisPaused)
		{
			MouseCursor.instance.ActivateMagnifyingCursor();
		}
	}

	private void OnMouseExit()
	{
		MouseCursor.instance.ActivateNormalCursor();
	}


	void OnMouseDown()
	{
		if (_event.monitorInteracted && _event.PuzzlesOpened == 0 && !_event.dialogueBoxOpen)
		{
            FindObjectOfType<AudioManager>().Play("interactgeneral");
            TriggerDialogue(dialogue);
			_event.dialogueBoxOpen = true;
			Drugs.SetActive(true);
			ChasisCover.SetActive(false);
			_event.chasisOpened = true;
			return;
		}
	}

	void TriggerDialogue(Dialogue dialogue)
	{
		if (dialogue == null)
			return;
		else
		{
			FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
			FindObjectOfType<DialogueManager>().DisplayNextSentence();
		}
	}
}
