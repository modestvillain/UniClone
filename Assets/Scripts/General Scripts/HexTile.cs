using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexTile:MonoBehaviour {

	public Vector2 location;
	public Sprite normalSprite;
	public Sprite highLightSprite;
	public Sprite occupiedSprite;
	public Sprite greyOutSprite;
	public Map map;
	public int hexWidth;
	public int hexHeight;
	public int x;
	public int y;
	public Vector2 position;
	public Vector2 center;
	public GameObject occupant;
	public List<HexTile> neighbors = new List<HexTile>();


	// Use this for initialization
	void Start () {

	}


	
	// Update is called once per frame
	void Update () {
	
	}

	public bool isOccupied(){
		return occupant!=null;
	}

	public void highlight(){
		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		if(isOccupied())	sr.sprite = occupiedSprite;
		else 				sr.sprite = highLightSprite;
	}

	public void deselect(){
		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		sr.sprite = normalSprite;
	}

	public void greyOut(){
		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		sr.sprite = this.greyOutSprite;
	}

	public void setWidthAndHeight(){

		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		this.hexWidth =  sr.sprite.texture.width;
		this.hexHeight = sr.sprite.texture.height;
	}


}
