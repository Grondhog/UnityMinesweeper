using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HelpMenuScript : MonoBehaviour {

	public Canvas helpMenu;
	public Canvas titleScreen;
	public Button backButton;
	
	
	public void backButtonPressed()
	{
		helpMenu.enabled = false;
		titleScreen.enabled = true;
	}
	
	
	
	
}
