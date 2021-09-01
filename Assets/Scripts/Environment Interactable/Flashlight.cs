using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
	[SerializeField] float offset;
	[SerializeField] GameObject _flashlight;
	bool IsOn = true;
	bool failSafe = false;

	void Update()
	{
		FlashlightMovement();
		ToggleFlashlight();
	}

	void FlashlightMovement()
	{
		Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		_flashlight.transform.position = new Vector3(cursorPos.x, cursorPos.y, Camera.main.transform.position.z - offset);
	}

	void ToggleFlashlight()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			//Debug.Log("E Press");
			if (IsOn == false && failSafe == false)
			{
				failSafe = true;
				_flashlight.SetActive(true);
				IsOn = true;
				StartCoroutine(FailSafe());
			}

			if (IsOn == true && failSafe == false)
			{
				failSafe = true;
				_flashlight.SetActive(false);
				IsOn = false;
				StartCoroutine(FailSafe());
			}
		}
	}

	IEnumerator FailSafe()
	{
		yield return new WaitForSeconds(0.02f);
		failSafe = false;
	}
}
