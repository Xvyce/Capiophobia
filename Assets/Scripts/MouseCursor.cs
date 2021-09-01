using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public static MouseCursor instance;

	public Texture2D normalCursor, changeSceneCursor, magnifyingCursor;

	private void Awake()
	{
		instance = this;
	}

	public void ActivateNormalCursor()
	{
		Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
	}

	public void ActivateSceneCursor()
	{
		Cursor.SetCursor(changeSceneCursor, new Vector2(changeSceneCursor.width/ 2, changeSceneCursor.height / 2), CursorMode.Auto);
	}

	public void ActivateMagnifyingCursor()
	{
		Cursor.SetCursor(magnifyingCursor, new Vector2(magnifyingCursor.width / 2, magnifyingCursor.height / 2), CursorMode.Auto);
	}
}
