using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour 
{
	public Vector2 velocity;

	void Start()
	{
		
	}

	void FixedUpdate()
	{

		Vector2 newPosition = new Vector2(transform.position.x + velocity.x, transform.position.y + velocity.y);
		transform.position = newPosition;

		if (transform.position.x >= GameBounds.bounds.x) 
		{
			velocity.x = -velocity.x;
		}
		if (transform.position.y >= GameBounds.bounds.y)
		{
			velocity.y = -velocity.y;
		}
		if (transform.position.x <= -GameBounds.bounds.x) 
		{
			velocity.x = -velocity.x;
		}
		if (transform.position.y <= -GameBounds.bounds.y)
		{
			velocity.y = -velocity.y;
		}
	}

void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "RedBrick") 
		{
			//check to see if it hits the bottom or the top:
			velocity.y = -velocity.y;
			

			//check to see if it hits one of the sides:
			velocity.x = -velocity.x;
		}	
		
		if(coll.gameObject.tag == "BlackBrick")
		{
			SpriteRenderer renderer = GetComponent<SpriteRenderer>();
			renderer.color = new Color(0f, 0, 0, 1f); // Set to black
			
			//check to see if it hits the bottom or the top:
			velocity.y = -velocity.y;
			
			
			//check to see if it hits one of the sides:
			velocity.x = -velocity.x;
		}		
		
		if(coll.gameObject.tag == "WhiteBrick")
		{
			SpriteRenderer renderer = GetComponent<SpriteRenderer>();
			renderer.color = new Color(255f, 255f, 255f); // Set to white?
			
			//check to see if it hits the bottom or the top:
			velocity.y = -velocity.y;
			
			
			//check to see if it hits one of the sides:
			velocity.x = -velocity.x;
			
//			gameObject.active = false;
			
		}
	}
}








