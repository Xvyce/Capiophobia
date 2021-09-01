using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrugs : OpenPuzzle
{
    public override void OnMouseDown()
    {
        if (!_event.isTransitioning && _event.PuzzlesOpened == 0)
        {
            _event.PuzzlesOpened++;
            PopupWindow.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, PopupWindow.transform.position.z);
            PopupWindow.SetActive(true);
        }
    }
}
