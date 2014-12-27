using UnityEngine;
using System.Collections;

public class BlackBrick : MonoBehaviour 
{
	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.gameObject.tag == "Ball")
		{
			if (Bounce.ballColor == "Black")
			{
				gameObject.active = false;
			}
		}
	}
}
