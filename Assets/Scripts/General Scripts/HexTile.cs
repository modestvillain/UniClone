using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexTile:MonoBehaviour {

	public Vector2 location;
	public Sprite normalSprite;
	public Sprite highLightSprite;
	public Sprite occupiedSprite;
	public Sprite greyOutSprite;
	public Sprite enemyTileSprite;
	public GameObject turnOverTile;
	public Map map;
	public int hexWidth;
	public int hexHeight;
	public int x;
	public int y;
	public Vector2 position;
	public Vector2 center;
	public GameObject occupant;
	public List<HexTile> neighbors = new List<HexTile>();

	
	void Start () {

	}

	void OnEnable() {
		normalSprite = Resources.Load<Sprite>("Sprites/whiteHexTile");
		highLightSprite = Resources.Load<Sprite>("Sprites/highLightHextTile");
		occupiedSprite = Resources.Load<Sprite>("Sprites/occupiedHexTile");
		greyOutSprite = Resources.Load<Sprite>("Sprites/greyTile");
		enemyTileSprite = Resources.Load<Sprite>("Sprites/redHextTile");
		turnOverTile = (GameObject)Instantiate(Resources.Load("Prefabs/blackoutTile"));
		turnOverTile.SetActive(false);
		gameObject.GetComponent<SpriteRenderer>().sprite = normalSprite;
		setWidthAndHeight();
	}
	
	void Update () {
	
	}

	public bool isOccupied() {
		return occupant!=null;
	}

	public void highlight() {
		turnOverTile.SetActive(false);
		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		if(isOccupied())	sr.sprite = occupiedSprite;
		else 				sr.sprite = highLightSprite;
	}

	public void deselect() {
		turnOverTile.SetActive(false);
		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		sr.sprite = normalSprite;
	}

	public void greyOut() {
		turnOverTile.transform.position = gameObject.transform.position;
		turnOverTile.SetActive(true);
	}
	

	public void highlightEnemy() {
		turnOverTile.SetActive(false);
		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		sr.sprite = this.enemyTileSprite;
	}
	
	public void setWidthAndHeight() {
		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
		this.hexWidth =  sr.sprite.texture.width;
		this.hexHeight = sr.sprite.texture.height;
	}

	public void removeOccupant(List<Player> players) {
		if(this.isOccupied()){
			Player script = this.occupant.GetComponent<Player>();
			players.Remove(script);
			Destroy(this.occupant);
			this.occupant = null;
		}
	}
}//class
