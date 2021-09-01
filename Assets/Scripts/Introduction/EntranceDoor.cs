using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceDoor : MonoBehaviour
{
	public Dialogue dialogue;

	[SerializeField] GameObject Door;
	[SerializeField] GameObject ChangeScene;

	Event _event;

	private void Start()
	{
		_event = FindObjectOfType<Event>();
	}

	private void OnMouseEnter()
	{
		if (!_event.dialogueBoxOpen && _event.PuzzlesOpened == 0 && !_event.isTransitioning && !_event.GameisPaused)
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
		if(!_event.dialogueBoxOpen && _event.PuzzlesOpened == 0 && !_event.isTransitioning && !_event.GameisPaused)
		{
			FindObjectOfType<AudioManager>().Play("dooropen");
			TriggerDialogue(dialogue);
			_event.dialogueBoxOpen = true;
			ChangeScene.SetActive(true);
			Door.SetActive(false);
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
