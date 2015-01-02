using UnityEngine;
using System.Collections;

public class DisplayText : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		gameObject.active = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (Bounce.brickCount == 0)
		{
			gameObject.active = true;
		}
	}
}
