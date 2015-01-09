using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bounce : MonoBehaviour 
{
	public bool debugBool = false;

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
	//more to fix bug -- didn't work. Not currently using these:
	public Transform upLeftLineStart, upLeftLineEnd;
	public Transform upRightLineStart, upRightLineEnd;
	public Transform downLeftLineStart, downLeftLineEnd;
	public Transform downRightLineStart, downRightLineEnd;
	RaycastHit2D whatIHit;
	
	public bool dougctest = true;
	//here are the special brick tags:
	const string redChange = "redchange";
	const string blueChange = "bluechange";
	const string greenChange = "greenchange";
	const string yellowChange = "yellowchange";
	const string arrows = "arrows";
	const string padlock = "padlock";
	const string key = "key";
	const string death = "death";
	const string treasure = "treasure";
	
	bool switchBool = false;
	bool keyBool = false;
	
	//player info:
	int playerLives = 5; // Will this need to be reset each time a player moves to the next level?
	
	//use Time.time to calculate the time bonus. Time.time is the time in seconds since the start of the game. 
	//Have a startTime that sets Time.time to the current time, to start counting from the start of the level.
	int timeBonus; //will this differ with each level? Or will it be the same? 60 seconds? 120?
	
	//initialize these here as 1. They'll get incremented when the player wins a level. They will stay the same to load the same level if they die.
	public static int levelNum = 1;
	public static string currentLevel = "level01";

	void Start()
	{
		ballColor = "white";
		brickTag = "white";
		initialXVelocity = 0.1f;
		initialYVelocity = velocity.y;
		Transform brickCountObject;
		brickCountObject = GameObject.Find ("BrickNumber").transform;
		brickCount = (int)brickCountObject.position.x + 1; // I subtracted 1 here because the count math was off somehow, and I don't know where. 
		treasureBrickCount = (int)brickCountObject.position.y;
		if(debugBool == true)
		{
			print (brickCount);
			print (treasureBrickCount);
		} 
	}

	void FixedUpdate()
	{
		Movement();
		RayCasting();

		if(Input.touchCount == 0)
		{
			StopMoving();
		}
	}
	
	void RayCasting()
	{
		LineCastCheck(upLineStart, upLineEnd, "up");
		LineCastCheck(downLineStart, downLineEnd, "down");
		LineCastCheck(leftLineStart, leftLineEnd, "left");
		LineCastCheck(rightLineStart, rightLineEnd, "right");
		//more to fix bug?
		LineCastCheck(upLeftLineStart, upLeftLineEnd, "up");
		LineCastCheck(upRightLineStart, upRightLineEnd, "up");
		LineCastCheck(downLeftLineStart, downLeftLineEnd, "down");
		LineCastCheck(downRightLineStart, downRightLineEnd, "down");
		//These need to be fixed^^^
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
				renderer.color = new Color(1.0f, 0f, 0f);
				CheckInteraction(ballSide);
			}
			//bluechange code
			else if (brickTag == blueChange)
			{
				ballColor = "blue";
				SpriteRenderer renderer = GetComponent<SpriteRenderer>();
				renderer.color = new Color(0.4f, 0.7f, 1f);
				CheckInteraction(ballSide);
			}
			//greenchange code
			else if(brickTag == greenChange)
			{
				ballColor = "green";
				SpriteRenderer renderer = GetComponent<SpriteRenderer>();
				renderer.color = new Color(0f, 1f, 0f);
				CheckInteraction(ballSide);
			}
			//yellowchange code
			else if(brickTag == yellowChange)
			{
				ballColor = "yellow";
				SpriteRenderer renderer = GetComponent<SpriteRenderer>();
				renderer.color = new Color(1f, 1f, 0f);
				CheckInteraction(ballSide);
			}
			else if(brickTag == arrows)
			{
				SwitchDirections();
				Destroy (whatIHit.collider.gameObject);
				CheckInteraction(ballSide);
			}
			else if(brickTag == padlock)
			{
				if(keyBool == true)
				{
					Destroy (whatIHit.collider.gameObject);
					CheckInteraction(ballSide);
					keyBool = false;
				}
				else
				{
					CheckInteraction(ballSide);
				}
			}
			else if(brickTag == key)
			{
				if(keyBool == true)
				{
					CheckInteraction(ballSide);
				}
				else
				{
					Destroy (whatIHit.collider.gameObject);
					CheckInteraction(ballSide);
					keyBool = true;
				}
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
				if(debugBool == false)
				{
					if(playerLives > 0)
					{
						//display life lost text and start the scene over, with one less life, etc
//						CheckInteraction(ballSide);
						StopMoving();
						velocity.y = ZERO;
						playerLives--;
						Application.LoadLevel(currentLevel);
					}
					else
					{
						Application.LoadLevel("lose_screen");
						playerLives = 5; 
					}
				}
				else
				{
					CheckInteraction(ballSide);
				}
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
		if(ballSide == "upLeft" || ballSide == "downLeft")
		{
			ChangeYVelocity();
			Vector2 newPosition = new Vector2(transform.position.x + xRecoil, transform.position.y);
			transform.position = newPosition;
		}
		if(ballSide == "upRight" || ballSide == "downRight")
		{
			ChangeYVelocity();
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
			
			Application.LoadLevel("win_screen");
			//display a win text and score here, etc
		}
	}
	
	public void LeftButtonPress()
	{
		if(switchBool == false) MoveLeft ();
		else MoveRight();
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
	
	public void RightButtonPress()
	{
		if(switchBool == false) MoveRight ();
		else MoveLeft();
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
	
	void SwitchDirections()
	{
		if(switchBool == true) 
		{
			switchBool = false;
		}
		else
		{
			switchBool = true;
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
