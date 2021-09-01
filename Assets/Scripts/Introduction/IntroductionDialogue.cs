using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroductionDialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public Animator dialogueAnimator;
    public Queue<string> sentences;
    Event _event;

    [SerializeField] GameObject IntroButton;
    [SerializeField] GameObject DialogueButton;
    [SerializeField] GameObject PictureBG;

    public SpriteRenderer bgnextscene;
    CameraPanning cameraPanning;
    Camera MainCam;

    public Animator _anim;

    void Start()
    {
        MainCam = Camera.main;
        cameraPanning = MainCam.GetComponent<CameraPanning>();
        _event = FindObjectOfType<Event>();
        sentences = new Queue<string>();
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
        IntroButton.SetActive(false);
        DialogueButton.SetActive(true);
        StartCoroutine(SceneTransition());
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
        FindObjectOfType<BGMManager>().Stop("picbgm");

        yield return new WaitForSeconds(0.5f);
        PictureBG.SetActive(false);
        FindObjectOfType<BGMManager>().Play("compshop");
        _anim.SetTrigger("Fade");
        _anim.SetTrigger("End");

        yield return new WaitForSeconds(1.0f);
        _event.isTransitioning = false;
        _event.isInIntro = false;
    }
}
