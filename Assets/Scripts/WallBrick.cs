using UnityEngine;
using System.Collections;

public class WallBrick : MonoBehaviour 
{		
	void OnCollisionEnter2D(Collision2D coll)
	{
		//make the brick disappear when the ball hits it.
		if(coll.gameObject.tag == "Ball")
		{
			if (Bounce.ballColor == "Wall")
			{	
				Destroy(gameObject);
				print("Hit!");
//				--Bounce.brickCount;
			}
		}
	}
}
