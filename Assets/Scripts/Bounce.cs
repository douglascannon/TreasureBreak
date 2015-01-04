using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bounce : MonoBehaviour 
{
	public bool debugBool = true;

	const float ZERO = 0f;
	public Vector2 velocity;
	public static string ballColor; //make sure this is lowercase, same as the brick's tag
	public Button RightButton;
	public Button LeftButton;	
	public float initialYVelocity;
	public float initialXVelocity;
	public int brickCount;
	public int treasureBrickCount;
	public float xRecoil = 0.15f; //this is the distance the ball recoils
	public bool hitBrick = false;
	public bool hitBounds = false;
	public string brickTag;
	public Transform upLineStart, upLineEnd;
	public Transform downLineStart, downLineEnd;
	public Transform leftLineStart, leftLineEnd;
	public Transform rightLineStart, rightLineEnd;
	RaycastHit2D whatIHit;
	
	public bool dougctest = true;
	//here are the special brick tags:
	const string redChange = "redchange";
	const string blueChange = "bluechange";
	const string greenChange = "greenchange";
	const string yellowChange = "yellowchange";
	const string death = "death";
	const string treasure = "treasure";
	
	//player info:
	public int playerLives;
	//use Time.time to calculate the time bonus. Time.time is the time in seconds since the start of the game. 
	//Have a startTime that sets Time.time to the current time, to start counting from the start of the level.
	public int timeBonus; 

	void Start()
	{
		ballColor = "white";
		brickTag = "white";
		initialXVelocity = 0.1f;
		initialYVelocity = velocity.y;
		brickCount = 57;
		treasureBrickCount = 20;
		
		//this needs to be somewhere else: It can't reload the amount of lives every time the player loses one.
		//if the level doesn't reset the bricks when the player loses a life, this will be done differently anyway.
		playerLives = 5;
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
		if (debugBool == true) Debug.DrawLine(lineStart.position,lineEnd.position, Color.green);

		// This seems to fix the initial ball bug where it goes through bricks for a while.
		// Why does this fix it?
		if (dougctest == true) {
			hitBrick = true;
			dougctest = false;
		}
		
		if(Physics2D.Linecast(lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer("Brick")))
		{
			whatIHit = Physics2D.Linecast(lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer("Brick"));
			hitBrick = true;
			brickTag = whatIHit.collider.gameObject.tag;
			if(debugBool ==  true) print (brickTag);
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
	void IfHitBrick(string ballSide)
	{
		if(hitBrick == true)
		{
			// if the brickTag and the ballColor are the same, bounce off and destroy the brick.
			if(brickTag == ballColor)
			{
				Destroy (whatIHit.collider.gameObject);
				brickCount--;
				CheckInteraction(ballSide);
			}
			//redchange code
			else if(brickTag == redChange)
			{
				ballColor = "red";
				SpriteRenderer renderer = GetComponent<SpriteRenderer>();
				renderer.color = new Color(255f, 0f, 0f);
				CheckInteraction(ballSide);
			}
			//bluechange code
			else if (brickTag == blueChange)
			{
				ballColor = "blue";
				SpriteRenderer renderer = GetComponent<SpriteRenderer>();
				renderer.color = new Color(0f, 128f, 255f);
				CheckInteraction(ballSide);
			}
			//greenchange code
			else if(brickTag == greenChange)
			{
				ballColor = "green";
				SpriteRenderer renderer = GetComponent<SpriteRenderer>();
				renderer.color = new Color(0f, 255f, 0f);
				CheckInteraction(ballSide);
			}
			//yellowchange code
			else if(brickTag == yellowChange)
			{
				ballColor = "yellow";
				SpriteRenderer renderer = GetComponent<SpriteRenderer>();
				renderer.color = new Color(255f, 255f, 0f);
				CheckInteraction(ballSide);
			}
			else if(brickTag == treasure)
			{
				if(brickCount == 0)
				{
					Destroy (whatIHit.collider.gameObject);
					treasureBrickCount--;
					CheckInteraction(ballSide);
				}
				else
				{					
					CheckInteraction(ballSide);
				}
			}
			//death code
			else if(brickTag == death)
			{
				if(playerLives != 0)
				{
					CheckInteraction(ballSide);
					
					if(debugBool == false)
					{
						playerLives--;
						Application.LoadLevel("scene");
					}
				}
				else
				{
					//don't do this while debugging. It makes it too hard to debug other changes. lol.
					if(debugBool == false)
					{
						velocity.x = ZERO;
						velocity.y = ZERO;
						SpriteRenderer renderer = GetComponent<SpriteRenderer>();
						renderer.color = new Color(0f, 0f, 0f);
					}

				}
				//display life lost text and start the scene over, with one less life, etc
			}
			// if the brickTag and the ballColor aren't the same, bounce off, but don't destroy the brick.
			else
			{
				CheckInteraction(ballSide);
			}
		}
	}
	
	void CheckInteraction(string ballSide)
	{
		// check for an up or down interaction
		if(ballSide == "up" || ballSide == "down")
		{
			ChangeYVelocity();
		}
		// --------------------------recoil movement for hitting the side of a brick------------------------------------
		if(ballSide == "left")
		{
			Vector2 newPosition = new Vector2(transform.position.x + xRecoil, transform.position.y);
			transform.position = newPosition;
		}
		if(ballSide == "right")
		{
			Vector2 newPosition = new Vector2(transform.position.x - xRecoil, transform.position.y);
			transform.position = newPosition;
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
		if (treasureBrickCount != 0)
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
		else
		{
			velocity.x = ZERO;
			velocity.y = ZERO;
			
			Application.LoadLevel("winScreen");
			//display a win text and score here, etc
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
