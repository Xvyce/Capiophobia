using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	[SerializeField] GameObject[] transitionButtons;
	[SerializeField] GameObject[] policeObjects;

	// Enemy AI plays the paramdam when you put your cursor in the collider 

	GameObject EnemyAudioSource;
	Event _event;

	int transitionCalculate = 0;

	private void Start()
	{
		EnemyAudioSource = GameObject.Find("EnemyAudio");
		_event = FindObjectOfType<Event>();
	}

	public void CalculateAI()
	{
		float enemyAppear;
		enemyAppear = Random.value;

		if(_event.KeyCount == 0) //0% Chance
		{
			//Debug.Log("0% Chance of Spawning");

			return;
		}

		if (_event.KeyCount == 1)
		{
			Debug.Log("45% Chance of Spawning");
			if (enemyAppear > 0.55) //45% Chance
			{
				SpawnAI();
			}
			return;
		}

		if (_event.KeyCount == 2)
		{
			Debug.Log("60% Chance of Spawning");
			if (enemyAppear > 0.40) // 60% Chance
			{
				SpawnAI();
			}
			return;
		}

		if (_event.KeyCount == 3)
		{
			Debug.Log("80% Chance of Spawning");
			if (enemyAppear > 0.20) // 80% Chance
			{
				SpawnAI();
			}
			return;
		}
	}

	void SpawnAI()
	{
		transitionCalculate = Random.Range(0, transitionButtons.Length);
		transitionButtons[transitionCalculate].GetComponent<ChangeScene>().enemyIsHere = true;
		Debug.Log("Enemy at " + transitionButtons[transitionCalculate].name);
		policeObjects[transitionCalculate].SetActive(true);
		EnemyAudioSource.SetActive(true);
		EnemyAudioSource.transform.position = transitionButtons[transitionCalculate].transform.position;
		StartCoroutine(PlayEnemySound());
	}

    public void ResetAI()
	{
		for(int i = 0; i <= transitionButtons.Length - 1; i++)
		{
			transitionButtons[i].GetComponent<ChangeScene>().enemyIsHere = false;
			transitionButtons[i].GetComponent<ChangeScene>().firstHover = true;
		}

		for (int i = 0; i <= policeObjects.Length - 1; i++)
		{
			policeObjects[i].SetActive(false);
			_event.isHoveringOverChangeScene = false;
		}
	}

	public void ResetSound()
	{
		StopCoroutine(PlayEnemySound());
	}

	IEnumerator PlayEnemySound()
	{
		yield return new WaitForSeconds(Random.Range(3f,4f));

		int RandomSound;

		RandomSound = Random.Range(0, 2);
		
		switch(RandomSound)
		{
			case 0:
				EnemyAudioSource.GetComponent<EnemyAudio>().Play("squeakyfloor");
				break;
			case 1:
				EnemyAudioSource.GetComponent<EnemyAudio>().Play("steppingglass");
				break;
		}
	}
}
