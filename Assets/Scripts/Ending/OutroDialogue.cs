using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OutroDialogue : MonoBehaviour
{
    [SerializeField] Dialogue DialogueBeforeBonk;
    [SerializeField] Dialogue DialogueAfterBonk;

    public TextMeshProUGUI dialogueText;
    public Animator dialogueAnimator;
    public Queue<string> sentences;
    Event _event;
    EvidenceManager evidenceManager;

    [SerializeField] GameObject EndingDialogue01;
    [SerializeField] GameObject EndingDialogue02;

    [SerializeField] SpriteRenderer BlackScreen;
    [SerializeField] SpriteRenderer TrueEnding;

    [SerializeField] GameObject thunder;
    [SerializeField] GameObject hand;
    [SerializeField] GameObject blackcover;

    [SerializeField] GameObject EndingMenu;
    [SerializeField] GameObject EndingMessage;
    

    public SpriteRenderer bgnextscene;
    CameraPanning cameraPanning;
    Camera MainCam;

    public Animator _anim;

    bool hasAllEvidences = true;

    int eventLayer = 0;

    void Start()
    {
        MainCam = Camera.main;
        cameraPanning = MainCam.GetComponent<CameraPanning>();
        _event = FindObjectOfType<Event>();
        sentences = new Queue<string>();
        evidenceManager = FindObjectOfType<EvidenceManager>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueText.text = "";

        sentences.Clear();
        _event.dialogueBoxOpen = true;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        dialogueAnimator.SetBool("IsOpen", true);
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    void EndDialogue()
    {
        dialogueAnimator.SetBool("IsOpen", false);
        _event.dialogueBoxOpen = false;
        
        switch(eventLayer)
		{
            case 0:
                StartCoroutine(SceneTransition());
                break;
            case 1:
                StartCoroutine(SceneTransitionToBlack());
                break;
            case 2:
                CheckForAllCollectedEvidences();
                break;
        }
        
    }

    void CheckForAllCollectedEvidences()
    {
        for (int i = 0; i < evidenceManager.hasEvidence.Length; i++)
        {
            if (evidenceManager.hasEvidence[i] == false)
            {
                hasAllEvidences = false;
                break;
            }
        }

        if (hasAllEvidences)
        {
            StartCoroutine(SceneTransitionEnding2());
        }
        else
        {
            StartCoroutine(SceneTransitionEnding1());
        }
    }


    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    IEnumerator SceneTransition()
    {
        _event.isTransitioning = true;
        _anim.SetTrigger("Start");

        yield return new WaitForSeconds(1.0f);
        cameraPanning.UpdatePanning(bgnextscene);
        MainCam.transform.position = new Vector3(bgnextscene.transform.position.x, bgnextscene.transform.position.y, MainCam.transform.position.z);

        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<BGMManager>().Play("thrillerbgm");
        _anim.SetTrigger("Fade");
        _anim.SetTrigger("End");

        yield return new WaitForSeconds(1.0f);
        _event.isTransitioning = false;
        EndingDialogue01.SetActive(false);
        EndingDialogue02.SetActive(true);
        TriggerDialogue(DialogueBeforeBonk);
        
        eventLayer = 1;
    }

    IEnumerator SceneTransitionToBlack()
    {
        _event.isTransitioning = true;
        _anim.SetTrigger("Start");
        Debug.Log("whack");
        FindObjectOfType<AudioManager>().Play("whack");
        yield return new WaitForSeconds(1.0f);
        cameraPanning.UpdatePanning(BlackScreen);
        MainCam.transform.position = new Vector3(BlackScreen.transform.position.x, BlackScreen.transform.position.y, MainCam.transform.position.z);
        FindObjectOfType<BGMManager>().Stop("thrillerbgm");

        yield return new WaitForSeconds(0.5f);
        _anim.SetTrigger("Fade");
        _anim.SetTrigger("End");

        yield return new WaitForSeconds(1.0f);
        _event.isTransitioning = false;
        TriggerDialogue(DialogueAfterBonk);
        eventLayer = 2;
    }

    IEnumerator SceneTransitionEnding1()
    {
        _event.isTransitioning = true;
        _anim.SetTrigger("Start");

        yield return new WaitForSeconds(1.0f);
        FindObjectOfType<BGMManager>().Stop("thrillerbgm");
        cameraPanning.UpdatePanning(BlackScreen);
        MainCam.transform.position = new Vector3(BlackScreen.transform.position.x, BlackScreen.transform.position.y, MainCam.transform.position.z);

        yield return new WaitForSeconds(0.5f);
        EndingMenu.SetActive(true);
        _anim.SetTrigger("Fade");
        _anim.SetTrigger("End");

        yield return new WaitForSeconds(1.0f);
        _event.isTransitioning = false;
    }

    IEnumerator SceneTransitionEnding2()
    {
        _event.isTransitioning = true;
        _anim.SetTrigger("Start");

        yield return new WaitForSeconds(1.0f);
        FindObjectOfType<BGMManager>().Stop("thrillerbgm");
        cameraPanning.UpdatePanning(TrueEnding);
        MainCam.transform.position = new Vector3(TrueEnding.transform.position.x, TrueEnding.transform.position.y, MainCam.transform.position.z);

        yield return new WaitForSeconds(0.5f);
        
        _anim.SetTrigger("Fade");
        _anim.SetTrigger("End");

        yield return new WaitForSeconds(1.0f);
        FindObjectOfType<AudioManager>().Play("rain");
        _event.isTransitioning = false;

        yield return new WaitForSecondsRealtime(2f);
        FindObjectOfType<AudioManager>().Play("thunder");
        thunder.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        FindObjectOfType<AudioManager>().Play("jumpscare");
        hand.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        blackcover.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        EndingMenu.SetActive(true);
        EndingMessage.SetActive(false);
    }

    void TriggerDialogue(Dialogue dialogue)
    {
        if (dialogue == null)
            return;
        else
        {
            StartDialogue(dialogue);
            DisplayNextSentence();
        }
    }
}
