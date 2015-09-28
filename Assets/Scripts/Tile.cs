using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	//[Range(0, 100)]
	//public float bombChance = 20.0f;
	
	public GameObject tile;
	public GameObject flag;
	GameObject cloneFlag;
	
	bool isFlaged = false;
	bool isRevealed = false;
	
	int value = 0;
	bool isBomb = false;
	
	public Material startMaterial;
	Material material;
	MeshRenderer rend;
	TextMesh textMesh;
	
	int x;
	int y;
	
	public Signal zeroReveal;
	 
	
	public GameScreenScript gameScreen;
	
	//RaycastHit hit;
	//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	
	
	
	void Start()
	{
		zeroReveal = new Signal();
		//zeroReveal.target = mapGenerator;
		textMesh = GetComponentInChildren<TextMesh>();
		//SetBombMaterial();
		//mapGenScript = mapGenerator.GetComponent<MapGenerator>();
	}
	
	void Update()
	{
		//textMesh.text = "" + value;
		//if(tile.GetComponent<BoxCollider>().Raycast(ray, out hit, 50))
		//{
			//print ("rayCastHit");
			//Reveal();
		//}
	}
	
	/*void OnMouseOver()
	{
		//print ("mouseOVer");
		if(Input.GetMouseButtonDown(0))
		{
			//print ("Reveal?");
			Reveal();
		}
		if(Input.GetMouseButton(1))
		{
			PlaceFlag();
		}
	}*/
	
	public void SetBombMaterial()
	{
		material = new Material(startMaterial);
		
		rend = tile.GetComponent<MeshRenderer>();
		if(isBomb)
		{
			
			material.color = Color.red;
		}
		
		rend.material = material;
	}
	
	void SetClickedMaterial()
	{
		material = new Material(startMaterial);
		rend = tile.GetComponent<MeshRenderer>();
		material.color = Color.gray;
		rend.material = material;
	}
	
	public void SetValue(int newVal)
	{
		if(newVal >= 0)
		{
			value = newVal;
			//textMesh.text = "" + value;
		}
	}
	
	public int GetValue()
	{
		return value;
	}
	
	public void BecomeBomb()
	{
		isBomb = true;
		//SetBombMaterial();
	}
	
	void Reveal()
	{
		if(!isRevealed)
		{
			
			if(isBomb)
			{
				SetBombMaterial();
			}
			else
			{
				
				
				if(value == 0)
				{
					//mapGenScript.RevealAdjacentTiles(x, y);
					//zeroReveal.Invoke();
				}
				else
				{
					textMesh.text = "" + value;
				}
			}
			isRevealed = true;
		}
		
	}
	
	public void SetText()
	{
		if(value != 0)
		{
			textMesh.text = "" + value;
		}
		SetClickedMaterial();
	}
	
	public bool IsRevealed()
	{
		return isRevealed;
	}
	
	public void SetRevealed(bool b)
	{
		isRevealed = b;
	}
	
	public bool IsFlagged()
	{
		return isFlaged;
	}
	
	public void PlaceFlag()
	{
		if(!isFlaged)
		{
			if(!isRevealed)
			{
				cloneFlag = Instantiate(flag, new Vector3(transform.position.x - .11f, transform.position.y + .3f, transform.position.z - .05f), Quaternion.Euler(90,0,0)) as GameObject;
				
				isFlaged = true;
			}
		}
		else
		{
			Destroy(cloneFlag);
			isFlaged = false;
		}
		
	}
	
	public void SetCoords(int newX, int newY)
	{
		x = newX;
		y = newY;
	}
	
	public int getX()
	{
		return x;
	}
	
	public int getY()
	{
		return y;
	}
	
	public bool IsBomb()
	{
		return isBomb;
	}
}
