﻿using UnityEngine;
using System.Collections;

public class LoseScreen : MonoBehaviour {
	
	// Use this for initialization
	void Start () 
	{
		//display text with time bonus, score, lives left, etc.
	}
	
	public void ButtonPress()
	{
		Application.LoadLevel(Bounce.currentLevel); // This doesn't increment currentLevel, because they'll have to do the same one over again.
	}
}
