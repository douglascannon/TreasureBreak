using UnityEngine;
using UnityEngine;
using System.Collections;

public class LoseScreen : MonoBehaviour {
	
	// Use this for initialization
	void Start () 
	{
		//display text with time bonus, score, lives left, etc.
	}
	
	public void ButtonPress()
	{
		Application.LoadLevel(Bounce.nextLevel); //this doesn't increment nextLevel, because they'll have to do the same one over again.
	}
}
