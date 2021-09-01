using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetMiddleRoom : MonoBehaviour
{
	public Dialogue dialogue1; // A Cabinet
	public Dialogue dialogue2; // I can move this

	[SerializeField] GameObject Cabinet;
	[SerializeField] GameObject BackDoorColliderFull;
	[SerializeField] GameObject BackDoorColliderSmall;

	Event _event;

	bool firstInteracted = false;

	private void Start()
	{
		_event = FindObjectOfType<Event>();
		BackDoorColliderFull.SetActive(false);
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
		if (!firstInteracted && _event.PuzzlesOpened == 0 && !_event.dialogueBoxOpen)
		{
            FindObjectOfType<AudioManager>().Play("interactgeneral");
            TriggerDialogue(dialogue1);
			firstInteracted = true;
			_event.cabinetMiddleRoomFirstInteract = true;
			_event.dialogueBoxOpen = true;
			return;
		}
		if (firstInteracted && _event.PuzzlesOpened == 0 && !_event.dialogueBoxOpen)
		{
			if (_event.cabinetMiddleRoomFirstInteract && _event.backDoorSmallInteracted && !_event.cabinetMiddleRoomIsMoved)
			{
                FindObjectOfType<AudioManager>().Play("moveCabinet");
                 TriggerDialogue(dialogue2);
				Cabinet.gameObject.transform.position = new Vector3(Cabinet.transform.position.x + 2.5f, Cabinet.gameObject.transform.position.y, Cabinet.gameObject.transform.position.z);
				_event.cabinetMiddleRoomIsMoved = true;
				_event.dialogueBoxOpen = true;
				BackDoorColliderFull.SetActive(true);
				BackDoorColliderSmall.SetActive(false);
				return;
			}
			if(_event.cabinetMiddleRoomFirstInteract && _event.backDoorSmallInteracted && _event.cabinetMiddleRoomIsMoved)
			{
                FindObjectOfType<AudioManager>().Play("interactgeneral");
                TriggerDialogue(dialogue2);
				_event.dialogueBoxOpen = true;
				return;
			}
            FindObjectOfType<AudioManager>().Play("interactgeneral");
            TriggerDialogue(dialogue1);
			_event.dialogueBoxOpen = true;
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
