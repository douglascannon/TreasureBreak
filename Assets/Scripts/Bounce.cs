using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bounce : MonoBehaviour 
{
	const float ZERO = 0f;
	public Vector2 velocity;
	public static string ballColor;
	public Button RightButton;
	public Button LeftButton;	
	public float initialYVelocity;
	public float initialXVelocity;
	public static int brickCount;
	public string lastHitDirection;
	
	//---------------------raycasting------------------
	
	public bool interact = false;
	public bool hitBrick = false;
	public bool hitBounds = false;
	public Transform upLineStart, upLineEnd;
	public Transform downLineStart, downLineEnd;
	public Transform leftLineStart, leftLineEnd;
	public Transform rightLineStart, rightLineEnd;
	
	RaycastHit2D whatIHit;
	
	//-------------------------------------------------

	void Start()
	{
		ballColor = "Red";
		initialXVelocity = 0.15f;
		initialYVelocity = velocity.y;
		lastHitDirection = "up";
//		SetLastHitDirection();
		brickCount = 31;
	}

	void FixedUpdate()
	{
		Movement();
		RayCasting();
	}
	
	
	void RayCasting()
	{
		LineCastCheck(upLineStart, upLineEnd);
		LineCastCheck(downLineStart, downLineEnd);
		LineCastCheck(leftLineStart, leftLineEnd);
		LineCastCheck(rightLineStart, rightLineEnd);
	}
	
	void LineCastCheck(Transform lineStart, Transform lineEnd)
	{
		//--------------------rightLine--------------------
		Debug.DrawLine(lineStart.position,lineEnd.position, Color.green);
		
		if(Physics2D.Linecast(lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer("Brick")))
		{
			whatIHit = Physics2D.Linecast(lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer("Brick"));
			hitBrick = true;
		}
		else if(Physics2D.Linecast(lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer("Bounds")))
		{
			whatIHit = Physics2D.Linecast(lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer("Bounds"));
			hitBounds = true;
		}
		else
		{
			hitBrick = false;
			hitBounds = false;
		}
		
		IfHitBrick();
		IfHitBounds();
	}
	
	// check for an up or down interaction
	void IfHitBrick()
	{
		// check for interaction with a brick
		if(hitBrick == true)
		{
			Destroy (whatIHit.collider.gameObject);
			ChangeYVelocity();
			brickCount--;
			
			/*
			if (lastHitDirection == "up")
			{
				lastHitDirection = "down";
				velocity.y = initialYVelocity;
			}
			else
			{
				lastHitDirection = "up";
				velocity.y = -initialYVelocity;
			}
			*/
		}
	}
	
	void IfHitBounds()
	{
		// check for an interaction with the bounds
		
		if(hitBounds == true)
		{		
			ChangeYVelocity();
			
			/*
			if (lastHitDirection == "up")
			{
				lastHitDirection = "down";
				velocity.y = initialYVelocity;
			}
			else
			{
				lastHitDirection = "up";
				velocity.y = -initialYVelocity;
			}
		*/
		}
	}
	
	void Movement()
	{
		Vector2 newPosition = new Vector2(transform.position.x + velocity.x, transform.position.y + velocity.y);
		transform.position = newPosition;
		
		if (transform.position.x >= GameBounds.bounds.x) 
		{
			velocity.x = ZERO;
		}
		if (transform.position.y >= GameBounds.bounds.y)
		{
			ChangeYVelocity();
//			lastHitDirection = "up";
		}
		if (transform.position.x <= -GameBounds.bounds.x) 
		{
			velocity.x = ZERO;
		}
		if (transform.position.y <= -GameBounds.bounds.y)
		{
			ChangeYVelocity();
//			lastHitDirection = "down";
		}
		if(brickCount == 0)
		{
			velocity.x = ZERO;
			velocity.y = ZERO;
			SpriteRenderer renderer = GetComponent<SpriteRenderer>();
			renderer.color = new Color(0f, 180f, 0f); // Set to green
			print("You win!");
		}
	}
	
	//
	// Use PointerEnter and PointerUp to denote when a button is pressed and released?
	//
	
	public void MoveRight()
	{
		if(Input.touchCount > 1)
		{
			velocity.x = ZERO;
		}
		else
		{
			velocity.x = initialXVelocity;
		}
	}
	public void MoveLeft()
	{
		if(Input.touchCount > 1)
		{
			velocity.x = ZERO;
		}
		else
		{
			velocity.x = -initialXVelocity;
		}
	}
	public void StopMoving()
	{
		velocity.x = ZERO;
	}
	
	/* don't really need this collision code anymore
	void OnCollisionEnter2D(Collision2D coll)
	{
		//collision for red bricks
		if (coll.gameObject.tag == "RedBrick" && velocity.x == 0f) 
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
		if(coll.gameObject.tag == "BlackBrick" && velocity.x == 0f)
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
		
		if(coll.gameObject.tag == "RedPaintBrick")
		{
			SpriteRenderer renderer = GetComponent<SpriteRenderer>();
			renderer.color = new Color(255f, 0f, 0f); // Set to white?
			ballColor = "Red";
			
			ChangeYVelocity();
		}
	}
	*/
	
	void ChangeXVelocity()
	{
		velocity.x = -velocity.x;
	}
	
	void ChangeYVelocity()
	{
		velocity.y = -velocity.y;
	}
	
	void SetLastHitDirection()
	{
		if (velocity.y > 0)
		{
			lastHitDirection = "down";
		}
		else
		{
			lastHitDirection = "up";
		}
	}
}








