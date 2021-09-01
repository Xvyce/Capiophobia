using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject collectionScreen;
    public GameObject page2;
    public GameObject Credits;

    public Animator animator;

    [SerializeField] GameObject[] evidence;
    EvidenceManager evidenceManager;

    public GameObject StartButton;
    public GameObject CollectionButton;
    public GameObject QuizButton;

	private void Start()
	{
        evidenceManager = FindObjectOfType<EvidenceManager>();
	}


	public void LoadScene(string sceneName)
    {
        FindObjectOfType<AudioManager>().Play("button");
        StartCoroutine(LoadLevel(sceneName));
        PlayerPrefs.SetFloat("CurrentScore", 0);
        
    }

    public void credits()
    {
        FindObjectOfType<AudioManager>().Play("button");
        Credits.SetActive(true);
        StartButton.SetActive(false);
        CollectionButton.SetActive(false);
        QuizButton.SetActive(false);

    }

    public void Collectibles()
    {
        FindObjectOfType<AudioManager>().Play("button");
        collectionScreen.SetActive(true);
        page2.SetActive(false);
        StartButton.SetActive(false);
        CollectionButton.SetActive(false);
        QuizButton.SetActive(false);
        CheckEvidence();
        //page3.SetActive(false);
        //page4.SetActive(false);
    }

    public void Back()
    {
        if(evidenceManager.navButtonInteractable)
		{
            FindObjectOfType<AudioManager>().Play("button");
            collectionScreen.SetActive(false);
            page2.SetActive(false);
            Credits.SetActive(false);
            StartButton.SetActive(true);
            CollectionButton.SetActive(true);
            QuizButton.SetActive(true);
        }
        //page3.SetActive(false);
        //page4.SetActive(false);
    }
    public void toPage1()
    {
        if(evidenceManager.navButtonInteractable)
		{
            FindObjectOfType<AudioManager>().Play("button");
            collectionScreen.SetActive(true);
            page2.SetActive(false);
        }
    }
    public void toPage2()
    {
        if(evidenceManager.navButtonInteractable)
		{
            FindObjectOfType<AudioManager>().Play("button");
            collectionScreen.SetActive(false);
            page2.SetActive(true);
        }
    }

    public void CheckEvidence()
    {
        Debug.Log("Check Evidence");
        for (int i = 0; i < evidence.Length; i++)
        {
            if (evidenceManager.hasEvidence[i] == true)
            {
                evidence[i].SetActive(true);
            }
        }
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!!!");
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
    IEnumerator LoadLevel(string sceneName)
    {
        animator.SetTrigger("End");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(sceneName);
    }
}
