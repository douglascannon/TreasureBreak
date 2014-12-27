using UnityEngine;
using System.Collections;

public class RedBrick : MonoBehaviour 
{
	void OnCollisionEnter2D(Collision2D coll)
	{
	//make the brick disappear when the ball hits it.
		if(coll.gameObject.tag == "Ball")
		{
			if (Bounce.ballColor == "Red")
			{
				gameObject.active = false;
			}
		}		
	}
}
