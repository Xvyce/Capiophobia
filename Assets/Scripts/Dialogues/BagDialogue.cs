using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagDialogue : MonoBehaviour
{
    public Dialogue dialogueset1;

    Event _event;

    [SerializeField] Texture2D normalCursor;
    [SerializeField] Texture2D magnifyingCursor;

    private void Start()
    {
        _event = FindObjectOfType<Event>();
    }

    private void OnMouseEnter()
    {
        if (!_event.isTransitioning && _event.PuzzlesOpened == 1 && !_event.dialogueBoxOpen && !_event.isDead && !_event.GameisPaused)
        {
            Cursor.SetCursor(magnifyingCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    void OnMouseDown()
    {
        if (!_event.isTransitioning && _event.PuzzlesOpened == 1 && !_event.dialogueBoxOpen && !_event.isDead && !_event.GameisPaused)
        {
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
