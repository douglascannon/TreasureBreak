using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bounce : MonoBehaviour 
{
	public Vector2 velocity;
	public static string ballColor;
	public Button RightButton;
	public Button LeftButton;

	void Start()
	{
		
	}

	void FixedUpdate()
	{

		Vector2 newPosition = new Vector2(transform.position.x + velocity.x, transform.position.y + velocity.y);
		transform.position = newPosition;

		if (transform.position.x >= GameBounds.bounds.x) 
		{
			velocity.x = 0f;
		}
		if (transform.position.y >= GameBounds.bounds.y)
		{
			ChangeYVelocity();
		}
		if (transform.position.x <= -GameBounds.bounds.x) 
		{
			velocity.x = 0f;
		}
		if (transform.position.y <= -GameBounds.bounds.y)
		{
			ChangeYVelocity();
		}
	}
	
	//
	// Use PointerEnter and PointerUp to denote when a button is pressed and released?
	//
	

	
	public void MoveRight()
	{
		velocity.x = 0.2f;
	}
	public void MoveLeft()
	{
			velocity.x = -0.2f;
	}
	public void StopMoving()
	{
		velocity.x = 0f;
	}
	
	
	void OnCollisionEnter2D(Collision2D coll)
	{
		//collision for red bricks
		if (coll.gameObject.tag == "RedBrick") 
		{
			ChangeYVelocity();
		}	
		
		//collision for black paint bricks
		if(coll.gameObject.tag == "BlackPaintBrick")
		{
			SpriteRenderer renderer = GetComponent<SpriteRenderer>();
			renderer.color = new Color(0f, 0, 0, 1f); // Set to black
			ballColor = "Black";

			ChangeYVelocity();
		}		
		
		//collision for black bricks
		if(coll.gameObject.tag == "BlackBrick")
		{			
			ChangeYVelocity();
		}	
		
		//collision for white bricks
		if(coll.gameObject.tag == "WhitePaintBrick")
		{
			SpriteRenderer renderer = GetComponent<SpriteRenderer>();
			renderer.color = new Color(255f, 255f, 255f); // Set to white?
			ballColor = "White";
			
			ChangeYVelocity();
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








