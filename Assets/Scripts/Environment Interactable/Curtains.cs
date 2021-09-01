using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtains : MonoBehaviour
{
    [SerializeField] GameObject BathTile;
    [SerializeField] GameObject BodyCams;

	Event _event;

	private void Start()
	{
		BathTile.SetActive(false);
        BodyCams.SetActive(false);
		_event = FindObjectOfType<Event>();
	}

	private void OnMouseEnter()
	{
		if (!_event.dialogueBoxOpen && _event.PuzzlesOpened == 0 && !_event.isTransitioning && !_event.isDead && !_event.GameisPaused)
		{
			MouseCursor.instance.ActivateMagnifyingCursor();
		}
	}

	private void OnMouseExit()
	{
		MouseCursor.instance.ActivateNormalCursor();
	}

	public void OnMouseDown()
    {
        FindObjectOfType<AudioManager>().Play("curtain");
        this.gameObject.SetActive(false);
		BathTile.SetActive(true);
        BodyCams.SetActive(true);
	}
}
