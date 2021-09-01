using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;

    public Animator animator; 

    public Queue<string> sentences;

    Event _event;

    void Start()
    {
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

        animator.SetBool("IsOpen", true);
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

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    void EndDialogue()
	{
        animator.SetBool("IsOpen", false);
        //yield return new WaitForSeconds(1.0f);
        _event.dialogueBoxOpen = false;
	}
}
