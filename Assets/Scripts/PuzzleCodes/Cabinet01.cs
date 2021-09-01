using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet01 : OpenPuzzle
{
	public Dialogue dialogueset1; // for no crowbar
	public Dialogue dialogueset2; // for no crowbar

	bool firstOpen = true;

	public override void OnMouseDown()
	{
		if(!_event.isTransitioning && _event.PuzzlesOpened == 0)
		{
			if (_event.hasCrowbar)
			{
				if (firstOpen)
				{
					TriggerDialogue(dialogueset2);
					firstOpen = false;
					_event.PuzzlesOpened++;
					PopupWindow.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, PopupWindow.transform.position.z);
					PopupWindow.SetActive(true);
					FindObjectOfType<AudioManager>().Play("crowbarhit");
				}
				else
				{
					_event.PuzzlesOpened++;
					PopupWindow.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, PopupWindow.transform.position.z);
					PopupWindow.SetActive(true);
					//FindObjectOfType<AudioManager>().Play("crowbarhit");
				}
			}
			else
			{
                FindObjectOfType<AudioManager>().Play("interactgeneral");
                TriggerDialogue(dialogueset1);
			}
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
