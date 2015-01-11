using UnityEngine;
using System.Collections;

public class WinScreen : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		//display text with time bonus, score, lives left, etc.
	}
	
	public void ButtonPress()
	{
		ChangeNextLevel(); // Increment currentLevel before you load it.
	
		Application.LoadLevel(Bounce.currentLevel); // currentLevel has been incrememted, now load it.
	}
	
	public static void ChangeNextLevel()
	{
		Bounce.levelNum += 1;
		if(Bounce.levelNum < 10)
		{
			Bounce.currentLevel = "level0" + Bounce.levelNum.ToString();
		}
		else
		{
			Bounce.currentLevel = "level" + Bounce.levelNum.ToString();
		}
	}
}
