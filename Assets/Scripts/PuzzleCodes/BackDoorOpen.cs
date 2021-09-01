using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackDoorOpen : MonoBehaviour
{
    [SerializeField] GameObject BackDoor;
    [SerializeField] GameObject TransitionObject;

	Event _event;

	private void Start()
	{
		_event = FindObjectOfType<Event>();
	}

	private void Update()
	{
		CheckForMedals();
	}

	void CheckForMedals()
	{
		if(_event.MedalInserted == 3)
		{
			BackDoor.SetActive(false);
			TransitionObject.SetActive(true);
		}
	}

}
