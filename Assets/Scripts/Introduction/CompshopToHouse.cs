using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompshopToHouse : MonoBehaviour
{
	readonly float transitionTime = 1.0f;
	readonly float changeSceneTime = 0.5f;

	[SerializeField] GameObject objectives;

	[SerializeField] Dialogue dialoguetext;
	[SerializeField] Dialogue arrivalatHouse;

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
		if (!_event.dialogueBoxOpen && !_event.isTransitioning && !_event.isDead && !_event.GameisPaused && _event.chasisOpened)
		{
			MouseCursor.instance.ActivateSceneCursor();
		}
	}
	private void OnMouseExit()
	{
		MouseCursor.instance.ActivateNormalCursor();
	}

	void OnMouseDown()
	{
		if (!_event.dialogueBoxOpen && !_event.isTransitioning && !_event.isDead && !_event.GameisPaused && _event.chasisOpened)
		{
			if(_event.hasIntroKey && _event.hasIntroFlashlight && _event.hasIntroPhone)
			{
				StartCoroutine(SceneTransition());
			}
			else
			{
				TriggerDialogue(dialoguetext);
			}
		}
	}

	void TriggerDialogue(Dialogue dialogue)
	{
		if (dialogue == null)
			return;
		else
		{
			FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
			FindObjectOfType<DialogueManager>().DisplayNextSentence();
		}
	}

	IEnumerator SceneTransition()
	{
		_event.isTransitioning = true;
		_anim.SetTrigger("Start");
		FindObjectOfType<AudioManager>().Play("footstepscement");

		yield return new WaitForSeconds(transitionTime);
		FindObjectOfType<BGMManager>().Stop("compshop");
		cameraPanning.UpdatePanning(bgnextscene);
		MainCam.transform.position = new Vector3(bgnextscene.transform.position.x, bgnextscene.transform.position.y, MainCam.transform.position.z);
		objectives.SetActive(true);

		yield return new WaitForSeconds(changeSceneTime);
		FindObjectOfType<BGMManager>().Play("windbgm");
		_anim.SetTrigger("Fade");
		_anim.SetTrigger("End");

		yield return new WaitForSeconds(1.0f);
		_event.isTransitioning = false;
		TriggerDialogue(arrivalatHouse);
	}
}
