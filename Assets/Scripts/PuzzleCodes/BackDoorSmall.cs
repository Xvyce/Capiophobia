using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackDoorSmall : MonoBehaviour
{
	public Dialogue dialogue1;

	Event _event;

	bool firstInteracted = false;

	private void Start()
	{
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

	void OnMouseDown()
	{
		if(_event.cabinetMiddleRoomFirstInteract)
		{
            FindObjectOfType<AudioManager>().Play("interactgeneral");
            if (!firstInteracted)
			{
				TriggerDialogue(dialogue1);
				firstInteracted = true;
				_event.backDoorSmallInteracted = true;
				_event.dialogueBoxOpen = true;
				return;
			}
			else
			{
				TriggerDialogue(dialogue1);
				_event.dialogueBoxOpen = true;
				return;
			}
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
