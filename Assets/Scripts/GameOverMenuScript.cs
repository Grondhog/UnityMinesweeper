using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverMenuScript : MonoBehaviour {

	public Canvas playAgainMenu;
	public Button yesButton;
	public Button noButton;
	
	public MapGenerator mapGen;
	
	public void YesPressed()
	{
		playAgainMenu.enabled = false;
		Application.LoadLevel(Application.loadedLevel);
		//mapGen.GameStart(5, 5, 3);
		
	}
	
	public void NoPressed()
	{
		Application.Quit();
	}
}
