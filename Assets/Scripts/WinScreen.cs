using UnityEngine;
using System.Collections;

public class WinScreen : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		//display text with time bonus, score, lives left, etc.
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.touchCount >= 1)
		{
			print ("touchCount >= 1");
//			Application.LoadLevel("scene");
		}
	}
	
	public void ButtonPress()
	{
		Application.LoadLevel("level02");
	}
}
