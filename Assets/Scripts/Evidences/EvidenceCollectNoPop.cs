using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceCollectNoPop : MonoBehaviour
{
    public Dialogue dialogueset1;
    [SerializeField] int EvidenceNumber; //set in inspector
    EvidenceManager _evidenceManager;
    Event _event;

    [SerializeField] GameObject EvidenceObjective;
    [SerializeField] Animator EvidenceAnim;
    bool FirstInteract = false;

    [SerializeField] int PopUpLayer = 0;
    void Start()
    {
        _evidenceManager = FindObjectOfType<EvidenceManager>();
        _event = FindObjectOfType<Event>();

        if(_evidenceManager.hasEvidence[EvidenceNumber] == true)
		{
            FirstInteract = true;
		}
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
        if(!_event.isTransitioning && _event.PuzzlesOpened == PopUpLayer)
		{
            FindObjectOfType<AudioManager>().Play("interactgeneral");
            _evidenceManager.hasEvidence[EvidenceNumber] = true;
            _event.dialogueBoxOpen = true;
            TriggerDialogue(dialogueset1);

            if (!FirstInteract)
            {
                FindObjectOfType<AudioManager>().Play("quotesfx");
                StartCoroutine(ShowEvidenceCollected());
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

    IEnumerator ShowEvidenceCollected()
	{
        FirstInteract = true;
        EvidenceObjective.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        EvidenceAnim.SetTrigger("Fade_Out");
        yield return new WaitForSecondsRealtime(2);
        EvidenceObjective.SetActive(false);
    }


}
