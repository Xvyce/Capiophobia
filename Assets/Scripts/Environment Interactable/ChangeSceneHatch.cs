using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneHatch : MonoBehaviour
{
	readonly float transitionTime = 1.0f;
	readonly float changeSceneTime = 0.5f;

	[SerializeField] Dialogue hatchDialogue;

	bool firstInteract = false;

	public SpriteRenderer bgnextscene;
	CameraPanning cameraPanning;
	Camera MainCam;
	[HideInInspector] Event _event;

	EnemyAI enemyAI;
	EnemyAI currentSceneAI;

	GameObject EnemyAudioSource;

	public Animator _anim;

	private void Awake()
	{
		MainCam = Camera.main;
		_event = FindObjectOfType<Event>();
		cameraPanning = MainCam.GetComponent<CameraPanning>();
		enemyAI = bgnextscene.GetComponentInParent<EnemyAI>();
		currentSceneAI = this.GetComponentInParent<EnemyAI>();
		EnemyAudioSource = GameObject.Find("EnemyAudio");
	}

	void OnMouseEnter()
	{
		if (!_event.dialogueBoxOpen && _event.PuzzlesOpened == 0 && !_event.isTransitioning && !_event.isDead && !_event.GameisPaused)
		{
			if(!firstInteract)
			{
				MouseCursor.instance.ActivateMagnifyingCursor();
			}
			else
			{
				MouseCursor.instance.ActivateSceneCursor();
			}
			
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
			if(!firstInteract)
			{
				TriggerDialogue(hatchDialogue);
				firstInteract = true;
				ResetAI();
				return;
			}
			else
			{
				StartCoroutine(SceneTransition());
				_event.isHoveringOverChangeScene = false;
				EnemyAudioSource.GetComponent<EnemyAudio>().Stop("squeakyfloor");
				EnemyAudioSource.GetComponent<EnemyAudio>().Stop("steppingglass");
				ResetAI();
				CalculateAI();
			}
		}
	}

	void ResetAI()
	{
		if (currentSceneAI != null)
		{
			currentSceneAI.ResetSound();
			currentSceneAI.ResetAI();
		}
	}

	void CalculateAI()
	{
		if (enemyAI != null)
		{
			enemyAI.ResetAI();
			enemyAI.CalculateAI();
		}
	}

	IEnumerator SceneTransition()
	{
		FindObjectOfType<AudioManager>().Play("fall");
		_event.isTransitioning = true;
		_anim.SetTrigger("Start");
		yield return new WaitForSeconds(transitionTime);
		cameraPanning.UpdatePanning(bgnextscene);
		MainCam.transform.position = new Vector3(bgnextscene.transform.position.x, bgnextscene.transform.position.y, MainCam.transform.position.z);
		yield return new WaitForSeconds(changeSceneTime);
		_anim.SetTrigger("Fade");
		_anim.SetTrigger("End");
		yield return new WaitForSeconds(1.0f);
		_event.isTransitioning = false;
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
}
