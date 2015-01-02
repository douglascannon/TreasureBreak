using UnityEngine;
using System.Collections;

public class GameBounds : MonoBehaviour 
{
	const float boundsOffset = 0.5f;
	public float xBounds;
	public float yBounds;
	public static Vector2 bounds { get; private set;}

	void Start()
	{
		Vector3 width = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
		xBounds = width.x - boundsOffset; //the ball was going .5 past the sides, so I subtracted .5 from the x bounds.
		yBounds -= boundsOffset;
		bounds = new Vector2 (xBounds, yBounds);
	}
}
