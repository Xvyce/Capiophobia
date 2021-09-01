using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skipToEnding : MonoBehaviour
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            switchToLastScene();
        }
    }
    void switchToLastScene()
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
            }

            if (!enemyIsHere && !_event.isDead) //&& _event.KeyCount > 0
            {
                if (currentSceneAI != null)
                {
                    currentSceneAI.ResetSound();
                    currentSceneAI.ResetAI();
                }

                if (enemyAI == null)
                {
                    return;
                }
                else
                {
                    enemyAI.ResetAI();
                    enemyAI.CalculateAI();
                }
            }
        }
    }

    IEnumerator SceneTransition()
    {
        if (isWood)
        {
            FindObjectOfType<AudioManager>().Play("footstepswood");
        }
        if (isCement)
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
