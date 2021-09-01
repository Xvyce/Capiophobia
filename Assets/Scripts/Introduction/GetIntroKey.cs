using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetIntroKey : MonoBehaviour
{
	public Dialogue dialogue;
	[HideInInspector] public IntroInventory introInventory;
	public GameObject inventoryIcon;
	[HideInInspector] public Event _event;
	[SerializeField] GameObject outerKey;

	void Awake()
	{
		introInventory = Camera.main.GetComponent<IntroInventory>();
		_event = FindObjectOfType<Event>();
	}
	private void OnMouseEnter()
	{
		if (!_event.dialogueBoxOpen && !_event.isTransitioning && !_event.GameisPaused)
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
		if (!_event.dialogueBoxOpen)
		{
			for (int i = 0; i < introInventory.slots.Length; i++)
			{
				if (introInventory.isFull[i] == false)
				{
					introInventory.isFull[i] = true;
					FindObjectOfType<AudioManager>().Play("pickup");
					Instantiate(inventoryIcon, introInventory.slots[i].transform, false);
					_event.hasIntroKey = true;
					TriggerDialogue();
					outerKey.SetActive(false);
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
