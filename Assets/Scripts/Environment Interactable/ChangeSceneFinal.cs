using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneFinal : MonoBehaviour
{
    readonly float transitionTime = 1.0f;
    readonly float changeSceneTime = 0.5f;

    public SpriteRenderer bgnextscene;
    CameraPanning cameraPanning;
    Camera MainCam;
    [HideInInspector] Event _event;
    [SerializeField] GameObject EndingMenu;
    
    [SerializeField] GameObject DialogueButton;
    [SerializeField] GameObject EndingDialougue01;

    [SerializeField] Dialogue dialogue;

    [SerializeField] GameObject Tasks;

    [SerializeField] GameObject MainUI;

    public Animator _anim;

    private void Awake()
    {
        MainCam = Camera.main;
        _event = FindObjectOfType<Event>();
        cameraPanning = MainCam.GetComponent<CameraPanning>();
    }

    void OnMouseDown()
    {
        if (!_event.dialogueBoxOpen && _event.PuzzlesOpened == 0 && !_event.isTransitioning && !_event.isDead && !_event.GameisPaused)
        {
            StartCoroutine(SceneTransition());
            _event.isHoveringOverChangeScene = false;
        }
    }

    private void OnMouseEnter()
    {
        if (!_event.dialogueBoxOpen && _event.PuzzlesOpened == 0 && !_event.isTransitioning && !_event.isDead && !_event.GameisPaused)
        {
            MouseCursor.instance.ActivateSceneCursor();
        }
    }

    private void OnMouseExit()
    {
        MouseCursor.instance.ActivateNormalCursor();
    }

    IEnumerator SceneTransition()
    {
        FindObjectOfType<AudioManager>().Play("footstepscement");
        _event.isTransitioning = true;
        _anim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        FindObjectOfType<BGMManager>().Stop("gamebgm");
        cameraPanning.UpdatePanning(bgnextscene);
        MainCam.transform.position = new Vector3(bgnextscene.transform.position.x, bgnextscene.transform.position.y, MainCam.transform.position.z);

        yield return new WaitForSeconds(changeSceneTime);
        Tasks.SetActive(false);
        MainUI.SetActive(false);
        _anim.SetTrigger("Fade");
        _anim.SetTrigger("End");

        yield return new WaitForSeconds(1.0f);
        _event.isTransitioning = false;

        DialogueButton.SetActive(false);
        EndingDialougue01.SetActive(true);
        _event.isInLastScene = true;

        TriggerDialogue(dialogue);
    }

    void TriggerDialogue(Dialogue dialogue)
    {
        if (dialogue == null)
            return;
        else
        {
            FindObjectOfType<OutroDialogue>().StartDialogue(dialogue);
            FindObjectOfType<OutroDialogue>().DisplayNextSentence();
        }
    }
}
