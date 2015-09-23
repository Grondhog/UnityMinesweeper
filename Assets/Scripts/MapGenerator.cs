using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	public int width = 3;
	public int height = 3;
	public int numberOfBombs = 2;
	int numberOfNonBombs;
	int numberOfRevealedTiles = 0;
	public GameObject tile;
	
	Tile[,] tiles;
	
	float distanceBetweenTiles = 1.2f;
	
	Ray ray;
	RaycastHit hit;
	
	Tile clickedTile;
	
	public Canvas gameOverScreen;
	bool gameOver = false;
	
	public void SetDimensions(int newWidth, int newHeight)
	{
		width = newWidth;
		//width = 11;
		height = newHeight;
	}
	
	public void SetNumberOfBombs(int newNumBombs)
	{
		numberOfBombs = newNumBombs;
	}
	
	public void GameStart(int _width, int _height, int _numberOfBombs)
	{
		gameOver = false;
		if(tiles != null)
		{
			clearTiles();
		}
		width = _width;
		height = _height;
		numberOfBombs = _numberOfBombs;
		tiles = new Tile[width, height];
		numberOfNonBombs = (width * height) - numberOfBombs;
		numberOfRevealedTiles = 0;
		SetCamera();
		GenerateMap();
		AssignBombs();
		AssignValues();
	}
	
	public void clearTiles()
	{
		for(int x = 0; x < width; x++)
		{
			for(int y = 0; y < height; y++)
			{
				Destroy(tiles[x,y]);
			}
		}
	}
	
	void Start()
	{
		/*tiles = new Tile[width, height];
		SetCamera();
		GenerateMap();
		AssignBombs();
		AssignValues();*/
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
									gameOverScreen.enabled = true;
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
							
						}
					}
					else if(Input.GetMouseButtonUp(1))
					{
						clickedTile = hit.collider.gameObject.GetComponent<Tile>();
						clickedTile.PlaceFlag();
					}
				}
			}
		}
	}
	
	void SetCamera()
	{
		Vector3 tempCameraPos = Camera.main.transform.position;
		tempCameraPos.x = ((width * distanceBetweenTiles) / 2) - .5f;
		if(width > height)
		{
			tempCameraPos.y = width * (distanceBetweenTiles - .1f);
		}
		else
		{
			tempCameraPos.y = height * (distanceBetweenTiles - .1f);
		}
		Camera.main.orthographicSize = .2f + (height * distanceBetweenTiles) / 2;
		
		tempCameraPos.z = ((height * distanceBetweenTiles) / 2) - .5f;
		Camera.main.transform.position = tempCameraPos;
	}
	
	void RevealTile(Tile tileToReveal)
	{
		if(!tileToReveal.IsRevealed())
		{
			tileToReveal.SetRevealed(true);
			if(tileToReveal.IsBomb())
			{
				tileToReveal.SetBombMaterial();
				gameOverScreen.enabled = true;
				gameOver = true;
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
	
	void GenerateMap()
	{
		if(height > 0 && width > 0)
		{
			GameObject temp;
			for(int x = 0; x < width; x++)
			{
				for(int y = 0; y < height; y++)
				{
					temp = Instantiate(tile, new Vector3(x * distanceBetweenTiles, 0, y * distanceBetweenTiles), Quaternion.identity) as GameObject;
					tiles[x,y] = temp.GetComponent<Tile>();
					tiles[x,y].SetCoords(x,y);
				}
			}
		}
	}
	
	void AssignBombs()
	{
		int randomXIndex = 0;
		int randomYIndex = 0;
		if(numberOfBombs < width * height)
		{
			for(int i = 0; i < numberOfBombs; i++)
			{
				randomXIndex = Random.Range(0, width);
				randomYIndex = Random.Range(0, height);
				if(!tiles[randomXIndex,randomYIndex].IsBomb())
				{
					tiles[randomXIndex, randomYIndex].BecomeBomb();				
				}
				else
				{
					i--;
				}
					
			}
		}
		else
		{
			numberOfBombs = Mathf.RoundToInt((width * height) * .15f);
			AssignBombs();
		}
	}
	
	void AssignValues()
	{
		for(int x = 0; x < width; x++)
		{
			for(int y = 0; y < height; y++)
			{
				
				tiles[x,y].SetValue(CheckAdjacent(x, y));
			}
		}
	}
	
	int CheckAdjacent(int startX, int startY)
	{
		int count = 0;
		for(int x = startX - 1; x <= startX + 1; x++)
		{
			for(int y = startY - 1; y <= startY + 1; y++)
			{
				if(x >= 0 && x < width && y >= 0 && y < height && (x != startX || y != startY))
				{
					if(tiles[x,y].IsBomb())
					{
							
						count++;
					}
					
				}
			}
		} ;
		return count;
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
