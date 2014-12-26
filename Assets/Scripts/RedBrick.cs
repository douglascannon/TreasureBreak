using UnityEngine;
using System.Collections;

public class RedBrick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter2D(Collision2D coll)
	{
	//make the brick disappear when the ball hits it.
		if(coll.gameObject.tag == "Ball")
		{
			gameObject.active = false;
		}		
	}
}
