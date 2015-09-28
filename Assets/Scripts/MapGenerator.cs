using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	int width = 3;
	int height = 3;
	int numberOfBombs = 2;
	
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
		
		height = newHeight;
	}
	
	public void SetNumberOfBombs(int newNumBombs)
	{
		numberOfBombs = newNumBombs;
	}
	
	public void GameStart(int _width, int _height, int _numberOfBombs)
	{
		/*gameOver = false;
		width = _width;
		height = _height;
		numberOfBombs = _numberOfBombs;
		tiles = new Tile[width, height];
		numberOfNonBombs = (width * height) - numberOfBombs;
		numberOfRevealedTiles = 0;*/
		
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
	
	
	void SetCamera()
	{
		Vector3 tempCameraPos = Camera.main.transform.position;
		tempCameraPos.x = ((width * distanceBetweenTiles) / 2) - .5f;
		
		tempCameraPos.y = width * distanceBetweenTiles;
		
		Camera.main.orthographicSize = .4f + (height * distanceBetweenTiles) / 2;
		
		
		
		tempCameraPos.z = ((height * distanceBetweenTiles) / 2) - 1.0f;
		Camera.main.transform.position = tempCameraPos;
	}
	
	
	
	public Tile[,] GenerateMap()
	{
		tiles = new Tile[width, height];
		if(height > 0 && width > 0)
		{
			GameObject temp;
			for(int x = 0; x < width; x++)
			{
				for(int y = 0; y < height; y++)
				{
					temp = Instantiate(tile, new Vector3(x * distanceBetweenTiles, 0, y * distanceBetweenTiles), Quaternion.identity) as GameObject;
					print (temp.GetComponent<Tile>());
					tiles[x,y] = temp.GetComponent<Tile>();
					tiles[x,y].SetCoords(x,y);
				}
			}
		}
		SetCamera();
		AssignBombs();
		AssignValues();
		return tiles;
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
	
	
	
	
	
}
