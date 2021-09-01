using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceManager : MonoBehaviour
{
    public static EvidenceManager instance;

    public bool[] hasEvidence;

    public bool navButtonInteractable = true;
    public bool evidenceIsInteractable = true;
    
    private void Start()
    {
        instance = this;
    }
}
