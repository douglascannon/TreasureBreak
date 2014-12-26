﻿using UnityEngine;
using System.Collections;

public class GameBounds : MonoBehaviour 
{
	public float xBounds;
	public float yBounds;
	public static Vector2 bounds { get; private set;}

	void Start()
	{
		Vector3 width = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
		xBounds = width.x - 0.5f;
		bounds = new Vector2 (xBounds, yBounds);
	}
}