using UnityEngine.Audio;
using System;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
	public Sound[] sounds;
	Camera MainCam;
	public float distance;
	public float panStereoValue;

	private void Update()
	{
		CalculateDistanceFromCamera();
		CalculatePanStereo();
	}

	void Awake()
	{
		MainCam = Camera.main;
		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.panStereo = s.panStereo;
			s.source.minDistance = s.minDistance;
			s.source.maxDistance = s.maxDistance;
			s.source.playOnAwake = s.playOnAwake;
		}
	}

	public void Play(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.panStereo = panStereoValue;

		if (!s.source.enabled)
		{
			return;
		}
		else
		{
			
			s.source.Play();
		}
		
	}

	public void Stop(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);

		if (!s.source.enabled)
		{
			return;
		}
		else
		{
			s.source.Stop();
		}

	}

	private void CalculateDistanceFromCamera()
	{
		distance = MainCam.transform.position.x - this.transform.position.x;
	}

	private void CalculatePanStereo()
	{
		if(distance >= 5) //far left
		{
			panStereoValue = -1;
		}

		if (distance >= -5 && distance <= 5) //middle
		{
			panStereoValue = 0;
		}

		if (distance <= -5) //far middle
		{
			panStereoValue = 1;
		}

		//if (distance < 5 && distance > 2) //middle left
		//{
		//	panStereoValue = -((Mathf.Abs(distance) / 5) * 1);
		//}
	}
}
