using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetIntroPhone : MonoBehaviour
{
	public Dialogue dialogue;
	public IntroInventory introInventory;
	public GameObject inventoryIcon;
	[HideInInspector] public Event _event;

	void Awake()
	{
		introInventory = Camera.main.GetComponent<IntroInventory>();
		_event = FindObjectOfType<Event>();
	}
	private void OnMouseEnter()
	{
		if (!_event.dialogueBoxOpen && _event.PuzzlesOpened == 0  && !_event.GameisPaused && _event.chasisOpened)
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
		if (!_event.dialogueBoxOpen && _event.PuzzlesOpened == 0 && _event.chasisOpened)
		{
			for (int i = 0; i < introInventory.slots.Length; i++)
			{
				if (introInventory.isFull[i] == false)
				{
					introInventory.isFull[i] = true;
					FindObjectOfType<AudioManager>().Play("pickup");
					Instantiate(inventoryIcon, introInventory.slots[i].transform, false);
					_event.hasIntroPhone = true;
					TriggerDialogue();
					Destroy(gameObject);
					break;
				}
			}
		}
	}

	void TriggerDialogue()
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
