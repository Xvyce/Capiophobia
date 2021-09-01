using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMedal03 : MonoBehaviour
{
	public Dialogue dialogue;
	public BadgeInventory badgeInventory;
	public GameObject inventoryIcon;
	[HideInInspector] public Event _event;


	void Awake()
	{
		badgeInventory = Camera.main.GetComponent<BadgeInventory>();
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
		if (!_event.dialogueBoxOpen)
		{
			for (int i = 0; i < badgeInventory.slots.Length; i++)
			{
				if (badgeInventory.isFull[i] == false)
				{
					badgeInventory.isFull[i] = true;
					//create an inventory prefab version of the pickup
					FindObjectOfType<AudioManager>().Play("pickup");
					Instantiate(inventoryIcon, badgeInventory.slots[i].transform, false);
					_event.hasMedal03 = true;
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
