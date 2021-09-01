using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueAfterBonk : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public Animator dialogueAnimator;
    public Queue<string> sentences;
    Event _event;

    [SerializeField] GameObject EndingDialogue02;
    

    public SpriteRenderer ending1;
    public SpriteRenderer ending2;

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
}
