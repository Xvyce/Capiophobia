using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvidencePopUpWithText : MonoBehaviour
{
    public GameObject PopupWindow;
    public GameObject blurBack;

    EvidenceManager evidenceManager;

    private void Start()
    {
        evidenceManager = FindObjectOfType<EvidenceManager>();
    }

    void OnMouseDown()
    {
        if (evidenceManager.evidenceIsInteractable == true)
        {
            evidenceManager.evidenceIsInteractable = false;
            evidenceManager.navButtonInteractable = false;
            blurBack.SetActive(true);
            PopupWindow.transform.position = new Vector3(Camera.main.transform.position.x - 4, Camera.main.transform.position.y, PopupWindow.transform.position.z);
            PopupWindow.SetActive(true);
        }
    }
}
