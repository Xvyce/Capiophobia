using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePopUp : OpenPuzzle
{
    public override void OnMouseDown()
    {
        if (!_event.isTransitioning)
        {
            FindObjectOfType<AudioManager>().Play("interactgeneral");
            //FindObjectOfType<AudioManager>().Play("paper");
            _event.PuzzlesOpened++;
            PopupWindow.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, PopupWindow.transform.position.z);
            PopupWindow.SetActive(true);
        }
    }
}
