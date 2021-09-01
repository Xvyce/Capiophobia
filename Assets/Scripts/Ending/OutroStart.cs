using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroStart : MonoBehaviour
{
    public Dialogue dialogue;
	public SpriteRenderer bgnextscene;
	CameraPanning cameraPanning;
	Camera MainCam;

	[SerializeField] GameObject DialogueButton;
    [SerializeField] GameObject EndingDialogue01;

	Event _event;

	public Animator _anim;

	private void Start()
	{
		MainCam = Camera.main;
		cameraPanning = MainCam.GetComponent<CameraPanning>();
		_event = FindObjectOfType<Event>();
	}

	public IEnumerator SceneTransition()
	{
		_event.isTransitioning = true;
		_anim.SetTrigger("Start");
		yield return new WaitForSeconds(1.0f);
		cameraPanning.UpdatePanning(bgnextscene);
		MainCam.transform.position = new Vector3(bgnextscene.transform.position.x, bgnextscene.transform.position.y, MainCam.transform.position.z);

		yield return new WaitForSeconds(0.5f);
		//FindObjectOfType<BGMManager>().Play("picbgm");
		_anim.SetTrigger("Fade");
		_anim.SetTrigger("End");

		yield return new WaitForSeconds(1.0f);
		_event.isTransitioning = false;
		_event.isInIntro = false;
		TriggerDialogue(dialogue);
	}

	void TriggerDialogue(Dialogue dialogue)
	{
		FindObjectOfType<IntroductionDialogue>().StartDialogue(dialogue);
		FindObjectOfType<IntroductionDialogue>().DisplayNextSentence();
	}
}
