using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDialogue : MonoBehaviour
{
    public Dialogue dialogueset1;

    Event _event;

    [SerializeField] int PopUpLayer = 0;

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


	void OnMouseDown()
    {
        if (!_event.isTransitioning && _event.PuzzlesOpened == PopUpLayer && !_event.dialogueBoxOpen && !_event.isDead && !_event.GameisPaused)
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
