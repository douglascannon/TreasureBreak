using UnityEngine;
using System.Collections;

public class BlueBrick : MonoBehaviour 
{		
	void OnCollisionEnter2D(Collision2D coll)
	{
		//make the brick disappear when the ball hits it.
		if(coll.gameObject.tag == "Ball")
		{
			if (Bounce.ballColor == "Blue")
			{	
				Destroy(gameObject);
				print("Hit!");
//				--Bounce.brickCount;
			}
		}
	}
}
