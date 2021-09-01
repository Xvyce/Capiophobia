using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPopUp : OpenPuzzle
{
    public Dialogue dialogueset1;

    [SerializeField] int PopUpLayer = 0;

    public override void OnMouseDown()
    {
        if (!_event.isTransitioning && _event.PuzzlesOpened == PopUpLayer)
        {
            FindObjectOfType<AudioManager>().Play("paper");
            _event.PuzzlesOpened++;
            PopupWindow.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, PopupWindow.transform.position.z);
            TriggerDialogue(dialogueset1);
            PopupWindow.SetActive(true);
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
