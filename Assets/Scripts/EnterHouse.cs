using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHouse : MonoBehaviour
{
	readonly float transitionTime = 1.0f;
	readonly float changeSceneTime = 0.5f;

	public SpriteRenderer bgnextscene;
	CameraPanning cameraPanning;
	Camera MainCam;
	[HideInInspector] Event _event;

	public Animator _anim;

	private void Awake()
	{
		MainCam = Camera.main;
		_event = FindObjectOfType<Event>();
		cameraPanning = MainCam.GetComponent<CameraPanning>();
	}

	void OnMouseEnter()
	{
		if (!_event.dialogueBoxOpen && _event.PuzzlesOpened == 0 && !_event.isTransitioning && !_event.isDead && !_event.GameisPaused)
		{
			MouseCursor.instance.ActivateSceneCursor();
		}
	}
	private void OnMouseExit()
	{
		if (!_event.dialogueBoxOpen && _event.PuzzlesOpened == 0 && !_event.isTransitioning && !_event.isDead && !_event.GameisPaused)
		{
			MouseCursor.instance.ActivateNormalCursor();
		}
	}

	void OnMouseDown()
	{
		if (!_event.dialogueBoxOpen && _event.PuzzlesOpened == 0 && !_event.isTransitioning && !_event.isDead && !_event.GameisPaused)
		{
			StartCoroutine(SceneTransition());
		}
	}

	IEnumerator SceneTransition()
	{
		FindObjectOfType<AudioManager>().Play("footstepscement");
		_event.isTransitioning = true;
		_anim.SetTrigger("Start");

		yield return new WaitForSeconds(transitionTime);
		cameraPanning.UpdatePanning(bgnextscene);
		MainCam.transform.position = new Vector3(bgnextscene.transform.position.x, bgnextscene.transform.position.y, MainCam.transform.position.z);
		FindObjectOfType<BGMManager>().Stop("windbgm");

		yield return new WaitForSeconds(changeSceneTime);
		FindObjectOfType<BGMManager>().Play("gamebgm");
		_anim.SetTrigger("Fade");
		_anim.SetTrigger("End");

		yield return new WaitForSeconds(1.0f);
		_event.isTransitioning = false;
	}
}
