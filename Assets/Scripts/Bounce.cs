using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour 
{
	public Vector2 velocity;
	public static string ballColor;

	void Start()
	{
		
	}

	void FixedUpdate()
	{

		Vector2 newPosition = new Vector2(transform.position.x + velocity.x, transform.position.y + velocity.y);
		transform.position = newPosition;

		if (transform.position.x >= GameBounds.bounds.x) 
		{
			ChangeXVelocity();
		}
		if (transform.position.y >= GameBounds.bounds.y)
		{
			ChangeYVelocity();
		}
		if (transform.position.x <= -GameBounds.bounds.x) 
		{
			ChangeXVelocity();
		}
		if (transform.position.y <= -GameBounds.bounds.y)
		{
			ChangeYVelocity();
		}
	}

void OnCollisionEnter2D(Collision2D coll)
	{
		//collision for red bricks
		if (coll.gameObject.tag == "RedBrick") 
		{
			ChangeYVelocity();
			ChangeXVelocity();
		}	
		
		//collision for black paint bricks
		if(coll.gameObject.tag == "BlackPaintBrick")
		{
			SpriteRenderer renderer = GetComponent<SpriteRenderer>();
			renderer.color = new Color(0f, 0, 0, 1f); // Set to black
			ballColor = "Black";

			ChangeYVelocity();
			ChangeXVelocity();
		}		
		
		//collision for black bricks
		if(coll.gameObject.tag == "BlackBrick")
		{			
			ChangeYVelocity();
			ChangeXVelocity();
		}	
		
		//collision for white bricks
		if(coll.gameObject.tag == "WhiteBrick")
		{
			SpriteRenderer renderer = GetComponent<SpriteRenderer>();
			renderer.color = new Color(255f, 255f, 255f); // Set to white?
			ballColor = "White";
			
			ChangeYVelocity();
			ChangeXVelocity();
		}
	}
	
	void ChangeXVelocity()
	{
		velocity.x = -velocity.x;
	}
	
	void ChangeYVelocity()
	{
		velocity.y = -velocity.y;
	}
}








