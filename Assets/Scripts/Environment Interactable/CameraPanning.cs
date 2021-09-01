using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanning : MonoBehaviour
{
	[SerializeField] Camera cam;
	readonly float panSpeed = 5f;
	readonly float panBorderThickness = 250f;

	[SerializeField] SpriteRenderer currentScene;

	float sceneMinX, sceneMaxX, sceneMinY, sceneMaxY;

	Vector3 pos;

	Event _event;

	private void Start()
	{
		_event = FindObjectOfType<Event>();

		sceneMinX = currentScene.transform.position.x - currentScene.bounds.size.x / 2f; 
		sceneMaxX = currentScene.transform.position.x + currentScene.bounds.size.x / 2f;
		sceneMinY = currentScene.transform.position.y - currentScene.bounds.size.y / 2f;
		sceneMaxY = currentScene.transform.position.y + currentScene.bounds.size.y / 2f;

		pos.x = currentScene.transform.position.x;
		pos.y = currentScene.transform.position.y;
		pos.z = currentScene.transform.position.z - 5;
	}

	private void Update()
	{
		MousePanCamera();
	}

	void MousePanCamera()
	{
		if(!_event.dialogueBoxOpen && _event.PuzzlesOpened == 0 && !_event.isTransitioning && !_event.isInIntro && !_event.isInLastScene)
		{
			if (Input.mousePosition.x >= Screen.width - panBorderThickness)
			{
				//Debug.Log("Camera Moving Right");
				pos.x += panSpeed * Time.deltaTime; //Move Camera Right
			}
			if (Input.mousePosition.x <= panBorderThickness)
			{
				//Debug.Log("Camera Moving Left");
				pos.x -= panSpeed * Time.deltaTime; //Move Camera Left
			}
			if (Input.mousePosition.y >= Screen.height - panBorderThickness)
			{
				//Debug.Log("Camera Moving Up");
				pos.y += panSpeed * Time.deltaTime; //Move Camera Up
			}
			if (Input.mousePosition.y <= panBorderThickness)
			{
				//Debug.Log("Camera Moving Down");
				pos.y -= panSpeed * Time.deltaTime; //Move Camera Down
			}
		}

		float camHeight = cam.orthographicSize;
		float camWidth = cam.orthographicSize * cam.aspect;

		float minX = sceneMinX + camWidth;
		float maxX = sceneMaxX - camWidth;
		float minY = sceneMinY + camHeight;
		float maxY = sceneMaxY - camHeight;

		pos.x = Mathf.Clamp(pos.x, minX, maxX);
		pos.y = Mathf.Clamp(pos.y, minY, maxY);

		cam.transform.position = pos;
	}

	public void UpdatePanning(SpriteRenderer newScene)
	{
		currentScene = newScene;

		cam.transform.position = new Vector3(newScene.transform.position.x, newScene.transform.position.y, newScene.transform.position.z - 10);

		sceneMinX = newScene.transform.position.x - newScene.bounds.size.x / 2f;
		sceneMaxX = newScene.transform.position.x + newScene.bounds.size.x / 2f;
		sceneMinY = newScene.transform.position.y - newScene.bounds.size.y / 2f;
		sceneMaxY = newScene.transform.position.y + newScene.bounds.size.y / 2f;
		
		pos.x = newScene.transform.position.x;
		pos.y = newScene.transform.position.y;
		pos.z = newScene.transform.position.z - 5;
	}
}
