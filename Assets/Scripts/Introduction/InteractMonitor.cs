using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMonitor : MonoBehaviour
{
    public Dialogue dialogueset1;

    Event _event;

    private void Start()
    {
        _event = FindObjectOfType<Event>();
    }

    private void OnMouseEnter()
    {
        if(_event.PuzzlesOpened == 0 && !_event.dialogueBoxOpen)
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
        if (!_event.isTransitioning && _event.PuzzlesOpened == 0 && !_event.dialogueBoxOpen && !_event.isDead && !_event.GameisPaused)
        {
            FindObjectOfType<AudioManager>().Play("interactgeneral");
            _event.monitorInteracted = true;
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
