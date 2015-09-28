using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	
	public MapGenerator mapGenerator;
	Tile[,] tiles;
	int width = 3;
	int height = 3;
	int numberOfBombs = 2;
	int numberOfNonBombs;
	int numberOfRevealedTiles = 0;
	
	bool gameOver;
	Ray ray;
	RaycastHit hit;
	
	Tile clickedTile;
	
	public Canvas gameOverScreen;
	public GameScreenScript gameScreen;
	
	

	void Start () 
	{
		//Instantiate(mapGenerator);
	}
	
	public void GameStart(int _width, int _height, int _numberOfBombs)
	{
		gameOver = false;
		width = _width;
		height = _height;
		numberOfBombs = _numberOfBombs;
		numberOfNonBombs = (width * height) - numberOfBombs;
		mapGenerator.SetDimensions(width, height);
		mapGenerator.SetNumberOfBombs(numberOfBombs);
		tiles = mapGenerator.GenerateMap();
		gameScreen.OpenMenu(numberOfBombs);
	}
	
	void Update()
	{
		if(!gameOver)
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit, 100.0f))
			{
				if(hit.collider.tag == "Tile")
				{
					
					if(Input.GetMouseButtonUp(0))
					{
						clickedTile = hit.collider.gameObject.GetComponent<Tile>();
						print (clickedTile);
						if(clickedTile != null)
						{
							//print ("Fart");
							//clickedTile.Reveal();
							if(!clickedTile.IsFlagged())
							{
								RevealTile(clickedTile);
								if(numberOfRevealedTiles >= numberOfNonBombs)
								{
									EndGame(true);
								}
							}
							
						}
					}
					else if(Input.GetMouseButtonUp(1))
					{
						clickedTile = hit.collider.gameObject.GetComponent<Tile>();
						if(clickedTile.IsFlagged())
						{
							gameScreen.RemovedAFlag();
							
						}
						else
						{
							gameScreen.UsedAFlag();
						}
						clickedTile.PlaceFlag();
					}
				}
			}
		}
	}
	
	void EndGame(bool playerWon)
	{
		gameOverScreen.enabled = true;
		gameOver = true;
		gameScreen.GameOver();
		if(playerWon)
		{
			Text[] texts = gameOverScreen.GetComponentsInChildren<Text>();
			foreach(Text t in texts)
			{
				if(t.text.Contains("YOU LOSE"))
				{
					t.text = "YOU WIN!!\n PLAY AGAIN?";
				}
			}
		}
	}
	
	void RevealTile(Tile tileToReveal)
	{
		if(!tileToReveal.IsRevealed())
		{
			tileToReveal.SetRevealed(true);
			if(tileToReveal.IsBomb())
			{
				tileToReveal.SetBombMaterial();
				EndGame(false);
			}
			else
			{
				numberOfRevealedTiles++;
				tileToReveal.SetText();
				
				if(tileToReveal.GetValue() == 0)
				{
					RevealAdjacentTiles(tileToReveal);
					//zeroReveal.Invoke();
				}
				
			}
			//tileToReveal.SetRevealed(true);
		}
	}
	
	public void RevealAdjacentTiles(Tile startTile)
	{
		//print ("RevealAdjacentTiles");
		int startX = startTile.getX();
		int startY = startTile.getY();
		for(int x = startX - 1; x <= startX + 1; x++)
		{
			for(int y = startY - 1; y <= startY + 1; y++)
			{
				//print ("x: " + x + "Y: " + y);
				//print ("fart");
				if(x != startX || y != startY)
				{
					if(x >= 0 && x < width && y >= 0 && y < height)
					{
						//print(tiles.Length);
						//print(tiles[x,y].IsRevealed());
						if(!tiles[x,y].IsRevealed())
						{
							RevealTile(tiles[x,y]);
						}
						
						//tiles[x,y].Reveal();
					}
				}
			}
		}
	}
	
	
}
