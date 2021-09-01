using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceCollect : InteractPopUp
{
    [SerializeField] int EvidenceNumber; //set in inspector
    EvidenceManager _evidenceManager;

    [SerializeField] GameObject EvidenceObjective;
    [SerializeField] Animator EvidenceAnim;
    bool FirstInteract = false;

    public override void Start()
    {
        base.Start();
        _evidenceManager = FindObjectOfType<EvidenceManager>();
    }
    public override void OnMouseDown()
    {
        base.OnMouseDown();
        _evidenceManager.hasEvidence[EvidenceNumber] = true;

        if (!FirstInteract)
		{
            FindObjectOfType<AudioManager>().Play("quotesfx");
            StartCoroutine(ShowEvidenceCollected());
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
