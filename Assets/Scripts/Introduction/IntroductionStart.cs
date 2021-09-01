using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionStart : MonoBehaviour
{
	public Dialogue dialogueset;
	public SpriteRenderer bgnextscene;
	CameraPanning cameraPanning;
	Camera MainCam;

	Event _event;

	public Animator _anim;

	private void Start()
	{
		MainCam = Camera.main;
		cameraPanning = MainCam.GetComponent<CameraPanning>();
		_event = FindObjectOfType<Event>();
		StartCoroutine("StartGame");
	}

	IEnumerator StartGame()
	{
		yield return new WaitForSecondsRealtime(1.5f);
		FindObjectOfType<AudioManager>().Play("quotesfx");
		yield return new WaitForSecondsRealtime(5);
		StartCoroutine("SceneTransition");
	}

	IEnumerator SceneTransition()
	{
		_event.isTransitioning = true;
		_anim.SetTrigger("Start");
		yield return new WaitForSeconds(1.0f);
		cameraPanning.UpdatePanning(bgnextscene);
		MainCam.transform.position = new Vector3(bgnextscene.transform.position.x, bgnextscene.transform.position.y, MainCam.transform.position.z);

		yield return new WaitForSeconds(0.5f);
		FindObjectOfType<BGMManager>().Play("picbgm");
		_anim.SetTrigger("Fade");
		_anim.SetTrigger("End");

		yield return new WaitForSeconds(1.0f);
		_event.isTransitioning = false;
		_event.isInIntro = false;
		TriggerDialogue();
	}

	void TriggerDialogue()
	{
		FindObjectOfType<IntroductionDialogue>().StartDialogue(dialogueset);
		FindObjectOfType<IntroductionDialogue>().DisplayNextSentence();
	}
}
