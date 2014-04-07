using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Base : HexTile {

	public Sprite blueBaseSprite;
	public Sprite blueBaseHighlightSprite;
	public Sprite redBaseSprite;
	public Sprite greyBaseSprite;

	
	void Start () {
		
	}

	void Update () {
		
	}
	
	public void highlight() {
		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		sr.sprite = this.highLightSprite;
	}
	
	public void deselect() {
		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
	}
	
	public void setBase(int baseType) {
		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		switch(baseType) {
		case 0:
			gameObject.transform.parent = GameObject.FindGameObjectWithTag("BLUE").transform;
			normalSprite = blueBaseSprite;
			highLightSprite = blueBaseHighlightSprite;
			occupiedSprite = blueBaseSprite;
			greyOutSprite = blueBaseSprite;
			sr.sprite = normalSprite;
			break;
		case 1:
			gameObject.transform.parent = GameObject.FindGameObjectWithTag("RED").transform;
			normalSprite = redBaseSprite;
			highLightSprite = redBaseSprite;
			occupiedSprite = redBaseSprite;
			greyOutSprite = redBaseSprite;
			sr.sprite = normalSprite;
			break;
		default:
			normalSprite = greyBaseSprite;
			highLightSprite = greyBaseSprite;
			occupiedSprite = greyBaseSprite;
			greyOutSprite = greyBaseSprite;
			sr.sprite = normalSprite;
			break;
		}
	}
}
