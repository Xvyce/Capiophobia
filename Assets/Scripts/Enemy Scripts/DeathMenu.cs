using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenuUI;
    public GameObject jumpscare;
    CameraPanning cameraPanning;
    Camera MainCam;

    Event _event; 
    EnemyAI enemyAI;


    public SpriteRenderer bgnextscene;

    private void Start()
    {
        MainCam = Camera.main;
        _event = FindObjectOfType<Event>();
        cameraPanning = MainCam.GetComponent<CameraPanning>();
        enemyAI = bgnextscene.GetComponentInParent<EnemyAI>();
    }

    public void Retry()
    {
        deathMenuUI.SetActive(false);
        //Time.timeScale = 1f;
        //_event.GameisPaused = false;
        cameraPanning.UpdatePanning(bgnextscene);
        jumpscare.SetActive(false);
        enemyAI.ResetAI();
        _event.isDead = false;
        _event.isHoveringOverChangeScene = false;
        _event.PuzzlesOpened = 0;
    }
    public void LoadMenu()
    {
        //pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _event.GameisPaused = false;
        SceneManager.LoadScene("TitleScreen");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
