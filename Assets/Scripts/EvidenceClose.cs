using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvidenceClose : MonoBehaviour
{
	public GameObject PopupWindow;
    public GameObject blurBack;

	EvidenceManager evidenceManager;

	private void Start()
	{
		evidenceManager = FindObjectOfType<EvidenceManager>();
	}

	private void Update()
	{
		ClosePuzzleEscape();
	}

	void OnMouseDown()
	{
		evidenceManager.navButtonInteractable = true;
		evidenceManager.evidenceIsInteractable = true;
        PopupWindow.SetActive(false);
        blurBack.SetActive(false);
        
	}

	void ClosePuzzleEscape()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			evidenceManager.navButtonInteractable = true;
			evidenceManager.evidenceIsInteractable = true;
			PopupWindow.SetActive(false);
            blurBack.SetActive(false);
        }
	}
}
