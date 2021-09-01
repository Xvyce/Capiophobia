using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SafeCode : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI codeText;
    string codeTextValue = "";
	readonly string codeToUnlock = "9324";

	[SerializeField] GameObject PanelWindow;
	[SerializeField] GameObject OpenedSafe;

	[SerializeField] GameObject SafeClose;
	[SerializeField] GameObject SafeOpen;

	Event _event;

	private void Start()
	{
		_event = FindObjectOfType<Event>();
		this.gameObject.SetActive(false);
	}

	private void Update()
	{
		codeText.text = codeTextValue;

		if(codeTextValue == codeToUnlock)
		{
			FindObjectOfType<AudioManager>().Play("safeaccess");
			_event.safeOpen = true;

			_event.PuzzlesOpened--;

			SafeOpen.SetActive(true);
			SafeClose.SetActive(false);
			OpenedSafe.SetActive(true);

			OpenedSafe.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, OpenedSafe.transform.position.z);

			PanelWindow.SetActive(false);
		}

		if(codeTextValue.Length >= 4 && codeTextValue != codeToUnlock)
		{
			FindObjectOfType<AudioManager>().Play("safeerror");
			codeTextValue = "";
		}
	}

	public void AddDigit(string digit)
	{
		FindObjectOfType<AudioManager>().Play("safepress");
		codeTextValue += digit;
	}

	public void ClosePanel()
	{
		PanelWindow.SetActive(false);
		_event.PuzzlesOpened--;
	}
}
