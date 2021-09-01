using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObject : MonoBehaviour
{
	public Dialogue dialogue;
	public Inventory inventory;
	public GameObject inventoryIcon;
	[HideInInspector] public Event _event;

	void Awake()
	{
		inventory = Camera.main.GetComponent<Inventory>();
		_event = FindObjectOfType<Event>();
	}

	private void OnMouseEnter()
	{
		MouseCursor.instance.ActivateMagnifyingCursor();
	}

	private void OnMouseExit()
	{
		MouseCursor.instance.ActivateNormalCursor();
	}

	public virtual void OnMouseDown()
	{
		if (!_event.dialogueBoxOpen)
		{
			for (int i = 0; i < inventory.slots.Length; i++)
			{
				if (inventory.isFull[i] == false)
				{
					inventory.isFull[i] = true;
					//create an inventory prefab version of the pickup
					FindObjectOfType<AudioManager>().Play("pickup");
					Instantiate(inventoryIcon, inventory.slots[i].transform, false);
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
