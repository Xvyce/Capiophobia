using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetIntroFlashlight : MonoBehaviour
{
	public Dialogue dialogue;
	[HideInInspector] public IntroInventory introInventory;
	public GameObject inventoryIcon;
	[HideInInspector] public Event _event;

	void Awake()
	{
		introInventory = Camera.main.GetComponent<IntroInventory>();
		_event = FindObjectOfType<Event>();
	}
	private void OnMouseEnter()
	{
		if (!_event.dialogueBoxOpen && _event.PuzzlesOpened == 0 && !_event.GameisPaused)
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
					_event.hasIntroFlashlight = true;
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
