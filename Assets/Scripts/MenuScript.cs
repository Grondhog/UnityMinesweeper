using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public Canvas titleScreen;
	
	public Canvas quitMenu;
	public Canvas helpMenu;
	public Button BeginnerButton;
	public Button IntermediateButton;
	public Button ExpertButton;
	public Button HelpButton;
	public Button exitText;
	
	
	public GameObject mapGen;
	
	void Start () 
	{
		quitMenu = quitMenu.GetComponent<Canvas>();
		BeginnerButton = BeginnerButton.GetComponent<Button>();
		IntermediateButton = IntermediateButton.GetComponent<Button>();
		ExpertButton = ExpertButton.GetComponent<Button>();
		exitText = exitText.GetComponent<Button>();
		quitMenu.enabled = false;
	}
	
	public void HelpButtonPressed()
	{
		print("FART");
		titleScreen.enabled = false;
		helpMenu.enabled = true;
		
	}
	
	public void ExitPressed()
	{
		quitMenu.enabled = true;
		BeginnerButton.enabled = false;
		exitText.enabled = false;
		
	}
	
	public void NoButtonPressed()
	{
		quitMenu.enabled = false;
		BeginnerButton.enabled = true;
		exitText.enabled = true;
	}
	
	public void YesButtonPressed()
	{
		Application.Quit();
	}
	
	public void BeginnerButtonPressed()
	{
		//Instantiate(mapGen);
		StartCoroutine(startGameAfterWait(0.5f, 9, 9, 10));
		
	}
	
	public void IntermediateButtonPressed()
	{
		StartCoroutine(startGameAfterWait(0.5f, 16, 16, 40));
	}
	
	public void ExpertButtonPressed()
	{
		StartCoroutine(startGameAfterWait(0.5f, 16, 30, 99));
	}
	
	
	
	IEnumerator startGameAfterWait(float waitTime, int width, int height, int numBombs)
	{
		yield return new WaitForSeconds(waitTime);
		MapGenerator mgScript = mapGen.GetComponent<MapGenerator>();
		mgScript.GameStart(width, height, numBombs);
		
		titleScreen.enabled = false;
	}
	
	
}
