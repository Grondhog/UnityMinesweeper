using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScreenScript : MonoBehaviour {

	public Canvas gameScreen;
	
	public Text minesRemainingField;
	int minesRemaining;
	
	public Text timeField;
	double time = 0;
	
	bool isOpen = false;
	bool gameOver = false;
	
	public void Update()
	{
		if(isOpen && !gameOver)
		{
			time += Time.deltaTime;
			//print(time + ", " + gameOver);
			timeField.text = "" + Mathf.Floor((float) time);
			minesRemainingField.text = "" + minesRemaining;
		}
		
	}
	
	public void OpenMenu(int numberOfmines)
	{
		minesRemaining = numberOfmines;
		gameScreen.enabled = true;
		isOpen = true;
	}
	
	public void GameOver()
	{
		//print ("GamScreenScript.GameOver()");
		gameOver = true;
	}
	
	public void UsedAFlag()
	{
		minesRemaining--;
	}
	
	public void RemovedAFlag()
	{
		minesRemaining++;
	}
	
}
