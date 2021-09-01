using System.Collections;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
	readonly float transitionTime = 1.0f;
	readonly float changeSceneTime = 0.5f;

	public bool enemyIsHere = false;

	public bool firstHover = true;

	[SerializeField] bool isWood;
	[SerializeField] bool isCement;

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
			MouseCursor.instance.ActivateSceneCursor();
		}
			
		if (enemyIsHere && !_event.isTransitioning)
		{
			_event.isHoveringOverChangeScene = true;

			if (firstHover)
			{
				int hoverSound = Random.Range(0, 2);

				if(hoverSound == 0)
				{
					FindObjectOfType<AudioManager>().Play("showbody1");
				}
				if (hoverSound == 1)
				{
					FindObjectOfType<AudioManager>().Play("showbody2");
				}


				firstHover = false;
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
			StartCoroutine(SceneTransition());
			_event.isHoveringOverChangeScene = false;
			EnemyAudioSource.GetComponent<EnemyAudio>().Stop("squeakyfloor");
			EnemyAudioSource.GetComponent<EnemyAudio>().Stop("steppingglass");
			if (enemyIsHere)
			{
				_event.isDead = true;
				ResetAI();
			}

			if (!enemyIsHere && !_event.isDead) //&& _event.KeyCount > 0
			{
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
		if(isWood)
		{
			FindObjectOfType<AudioManager>().Play("footstepswood");
		}
		if(isCement)
		{
			FindObjectOfType<AudioManager>().Play("footstepscement");
		}
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

		if (_event.isDead)
		{
			StartCoroutine(_event.PlayJumpScare());
		}
	}
}
