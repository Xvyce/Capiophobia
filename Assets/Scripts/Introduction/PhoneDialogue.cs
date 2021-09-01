using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneDialogue : MonoBehaviour
{
    public Dialogue dialogueset1;

    Event _event;

    private void Start()
    {
        _event = FindObjectOfType<Event>();
    }

    private void OnMouseEnter()
    {
        if (!_event.isTransitioning && _event.PuzzlesOpened == 0 && !_event.dialogueBoxOpen && !_event.GameisPaused && _event.chasisOpened)
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
        if (!_event.isTransitioning && _event.PuzzlesOpened == 0 && !_event.dialogueBoxOpen && !_event.GameisPaused && _event.chasisOpened)
        {
            FindObjectOfType<AudioManager>().Play("interactgeneral");
            TriggerDialogue(dialogueset1);
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
