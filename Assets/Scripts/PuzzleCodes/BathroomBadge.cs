using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomBadge : OpenPuzzle
{
    public Dialogue dialogueset1;

    public override void OnMouseDown()
	{
        FindObjectOfType<AudioManager>().Play("interactgeneral");
        TriggerDialogue(dialogueset1);
        PopupWindow.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, PopupWindow.transform.position.z);
        PopupWindow.SetActive(true);
        _event.PuzzlesOpened++;
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
