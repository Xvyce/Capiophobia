using UnityEngine;

public class LastDoor : MonoBehaviour
{
    public Dialogue dialogue1;
    public Dialogue dialogue2;
    public Dialogue dialogue3;
    public Dialogue dialogue4;
	public Dialogue dialogue5;

	[SerializeField] GameObject SceneTransition;

	Event _event;

	bool firstInteracted = false;

	private void Start()
	{
		_event = FindObjectOfType<Event>();
		SceneTransition.SetActive(false);
	}

	private void OnMouseEnter()
	{
		if (!_event.dialogueBoxOpen && _event.PuzzlesOpened == 0 && !_event.isTransitioning && !_event.isDead && !_event.GameisPaused)
		{
			MouseCursor.instance.ActivateMagnifyingCursor();
		}
	}
	private void OnMouseExit()
	{
		MouseCursor.instance.ActivateNormalCursor();
	}

	void OnMouseDown()
	{
		if(!firstInteracted && !_event.dialogueBoxOpen)
		{
			TriggerDialogue(dialogue1);
			firstInteracted = true;
			_event.lastDoorFirstInteract = true;
			_event.CheckForEvents();
			return;
		}
		if(firstInteracted && !_event.dialogueBoxOpen)
		{
			if (_event.KeyCount == 0)
			{
				TriggerDialogue(dialogue2);
				return;
			}
			if (_event.KeyCount == 1) 
			{
				TriggerDialogue(dialogue3);
				return;
			}
			if (_event.KeyCount == 2) 
			{
				TriggerDialogue(dialogue4);
				return;
			}
			if (_event.KeyCount == 3)
			{
                FindObjectOfType<AudioManager>().Play("dooropen");
                TriggerDialogue(dialogue5);
				SceneTransition.SetActive(true);
				this.gameObject.SetActive(false);
				return;
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
}
