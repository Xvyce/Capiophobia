using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMedal01 : MonoBehaviour
{
    public Dialogue dialogueset1;
    public Dialogue dialogueset2;
    public Dialogue dialogueset3;

    [SerializeField] GameObject PlaceMedal;
	[SerializeField] GameObject DoorMedal;
	Event _event;

	bool firstInteract = false;


    private void Start()
	{
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


    private void OnMouseDown()
	{
        FindObjectOfType<AudioManager>().Play("interactgeneral");
        if (!_event.hasMedal01 && firstInteract)
        {
            TriggerDialogue(dialogueset1);
        }
        if (!_event.hasMedal01 && !firstInteract)
        {
            TriggerDialogue(dialogueset2);
        }

        if (_event.hasMedal01 && !firstInteract)
		{
			PlaceMedal.SetActive(true);
			DoorMedal.SetActive(true);
			_event.MedalInserted++;
            TriggerDialogue(dialogueset3);
            firstInteract = true;
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
