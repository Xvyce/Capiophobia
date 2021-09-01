using System.Collections;
using UnityEngine;

public class Event : MonoBehaviour
{
    public bool isInIntro = true;

    public bool dialogueBoxOpen = false;
    public bool isPuzzleOpen = false;
    public bool isTransitioning = false;

    public bool monitorInteracted = false;
    public bool chasisOpened = false;
    public bool hasIntroKey = false;
    public bool hasIntroFlashlight = false;
    public bool hasIntroPhone = false;

    public int PuzzlesOpened = 0;
    public bool enemyAnimPlaying = false;
    public bool hasCrowbar = false;
    public bool safeOpen = false;

	public int KeyCount = 0;
    public int MedalInserted = 0;

    public bool hasMedal01 = false;
    public bool hasMedal02 = false;
    public bool hasMedal03 = false;

    public bool isDead = false;
    public bool GameisPaused = false;
    public bool isHoveringOverChangeScene = false;

    public bool cabinetMiddleRoomFirstInteract = false;
    public bool backDoorSmallInteracted = false;
    public bool cabinetMiddleRoomIsMoved = false;

    public bool lastDoorFirstInteract = false;

    public bool isInLastScene = false;

    public Animator _explore;
    public Animator _findkeys;

    [SerializeField] GameObject jumpscare;
    [SerializeField] GameObject DeathMenu;


    [SerializeField] GameObject explore;
    [SerializeField] GameObject findkeys;

	private void Update()
	{
        CheckForNegatives();
	}

    void CheckForNegatives()
	{
        if(PuzzlesOpened < 0)
		{
            PuzzlesOpened = 0;
		}
	}

	public void CheckForEvents()
	{
        if (lastDoorFirstInteract == true)
		{
            FindObjectOfType<AudioManager>().Play("objective");
            _explore.SetTrigger("Start");
            _explore.SetTrigger("During");
            explore.SetActive(false);
            findkeys.SetActive(true);
            _findkeys.SetTrigger("Start");
            _findkeys.SetTrigger("During");
		}
	}

	public IEnumerator PlayJumpScare()
	{
        yield return new WaitForSeconds(2f);
        jumpscare.SetActive(true);
        int jumpscareRandom = Random.Range(0, 2);

        switch(jumpscareRandom)
		{
            case 0:
                FindObjectOfType<AudioManager>().Play("kill1");
                break;
            case 1:
                FindObjectOfType<AudioManager>().Play("kill2");
                break;
        }
        
        Debug.Log("Game Over");
        yield return new WaitForSeconds(1f);
        DeathMenu.SetActive(true);
        MouseCursor.instance.ActivateNormalCursor();
	}
}