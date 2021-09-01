using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    Event _event;

	private void Start()
	{
		_event = FindObjectOfType<Event>();
    }
	void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && _event.PuzzlesOpened == 0)
        {
            MouseCursor.instance.ActivateNormalCursor();
            if (!_event.GameisPaused && !_event.isTransitioning)
            {
                Debug.Log("paused");
                Pause();
            }
            else
            {
                Resume();
                Debug.Log("unpaused");
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _event.GameisPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _event.GameisPaused = true;
    }
    public void LoadMenu()
    {
        MouseCursor.instance.ActivateNormalCursor();
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
