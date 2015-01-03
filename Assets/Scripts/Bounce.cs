using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bounce : MonoBehaviour 
{
	const float ZERO = 0f;
	public Vector2 velocity;
	public static string ballColor; //make sure this is lowercase, same as the brick's tag
	public Button RightButton;
	public Button LeftButton;	
	public float initialYVelocity;
	public float initialXVelocity;
	public static int brickCount;
	public float xMovement = 0.15f; //this is the distance the ball recoils
	public bool hitBrick = false;
	public bool hitBounds = false;
	public string brickTag;
	public Transform upLineStart, upLineEnd;
	public Transform downLineStart, downLineEnd;
	public Transform leftLineStart, leftLineEnd;
	public Transform rightLineStart, rightLineEnd;
	
	RaycastHit2D whatIHit;
	
	//-------------------------------------------------

	void Start()
	{
		ballColor = "red";
		initialXVelocity = 0.1f;
		initialYVelocity = velocity.y;
		brickCount = 100;
	}

	void FixedUpdate()
	{
		Movement();
		RayCasting();
	}
	
	
	void RayCasting()
	{
		LineCastCheck(upLineStart, upLineEnd, "up");
		LineCastCheck(downLineStart, downLineEnd, "down");
		LineCastCheck(leftLineStart, leftLineEnd, "left");
		LineCastCheck(rightLineStart, rightLineEnd, "right");
	}
	
	void LineCastCheck(Transform lineStart, Transform lineEnd, string ballSide)
	{
		Debug.DrawLine(lineStart.position,lineEnd.position, Color.green);
		
		if(Physics2D.Linecast(lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer("Brick")))
		{
			whatIHit = Physics2D.Linecast(lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer("Brick"));
			hitBrick = true;
			brickTag = whatIHit.collider.gameObject.tag;
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
		
		IfHitBrick(ballSide);
		IfHitBounds(ballSide);
	}
	
	// check for interaction with a brick
	void IfHitBrick(string side)
	{
		if(hitBrick == true)
		{
			// if the brickTag and the ballColor are the same, bounce off and destroy the brick.
			if(brickTag == ballColor)
			{
				Destroy (whatIHit.collider.gameObject);
				brickCount--;
				
				// check for an up or down interaction
				if(side == "up" || side == "down")
				{
					ChangeYVelocity();
				}
				// --------------------------recoil movement for hitting the side of a brick------------------------------------
				if(side == "left")
				{
					Vector2 newPosition = new Vector2(transform.position.x + xMovement, transform.position.y);
					transform.position = newPosition;
				}
				if(side == "right")
				{
					Vector2 newPosition = new Vector2(transform.position.x - xMovement, transform.position.y);
					transform.position = newPosition;
				}
			}
			// if the brickTag and the ballColor aren't the same, bounce off, but don't destroy the brick.
			else
			{
				// check for an up or down interaction
				if(side == "up" || side == "down")
				{
					ChangeYVelocity();
				}
				// --------------------------recoil movement for hitting the side of a brick------------------------------------
				if(side == "left")
				{
					Vector2 newPosition = new Vector2(transform.position.x + xMovement, transform.position.y);
					transform.position = newPosition;
				}
				if(side == "right")
				{
					Vector2 newPosition = new Vector2(transform.position.x - xMovement, transform.position.y);
					transform.position = newPosition;
				}
			}
		}
	}

	void IfHitBounds(string side)
	{
		// check for an interaction with the bounds
		if(hitBounds == true)
		{		
			if(side == "up" || side == "down") ChangeYVelocity();
			else StopMoving();
		}
	}
	
	void Movement()
	{
		if (brickCount != 0)
		{
			Vector2 newPosition = new Vector2(transform.position.x + velocity.x, transform.position.y + velocity.y);
			transform.position = newPosition;
			
			if (transform.position.x >= GameBounds.bounds.x) 
			{
				StopMoving();
			}
			if (transform.position.y >= GameBounds.bounds.y)
			{
			ChangeYVelocity();
			}
			if (transform.position.x <= -GameBounds.bounds.x) 
			{
				StopMoving();
			}
			if (transform.position.y <= -GameBounds.bounds.y)
			{
				ChangeYVelocity();
			}
		}
		else //debug brickCount
		{
			velocity.x = ZERO;
			velocity.y = ZERO;
			SpriteRenderer renderer = GetComponent<SpriteRenderer>();
			renderer.color = new Color(0f, 180f, 0f); // Set to green
		}
	}
	
	public void MoveLeft()
	{
		if(Input.touchCount > 1)
		{
			StopMoving();
		}
		else
		{
			// if the ball is all the way to the left, don't let it move left
			if (transform.position.x <= -GameBounds.bounds.x) 
			{
				return;
			}
			else
			{
				velocity.x = -initialXVelocity;
			}
		}
	}
	
	public void MoveRight()
	{
		if(Input.touchCount > 1)
		{
			StopMoving();
		}
		else
		{
		// if the ball is all the way to the right, don't let it move right
			if (transform.position.x >= GameBounds.bounds.x) 
			{
				return;
			}
			else
			{
				velocity.x = initialXVelocity;
			}
		}
	}

	void StopMoving()
	{
		velocity.x = ZERO;
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








