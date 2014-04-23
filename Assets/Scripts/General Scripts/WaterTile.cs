using UnityEngine;
using System.Collections;

public class WaterTile : HexTile {

	public Animator ani;

	void OnEnable() {
		normalSprite = Resources.Load<Sprite>("Sprites/waterTile");
		highLightSprite = Resources.Load<Sprite>("Sprites/waterTileHighlight");
		occupiedSprite = Resources.Load<Sprite>("Sprites/waterTileHighlight");
		greyOutSprite = Resources.Load<Sprite>("Sprites/greyTile");
		enemyTileSprite = Resources.Load<Sprite>("Sprites/waterTileEnemy");
		gameObject.GetComponent<SpriteRenderer>().sprite = normalSprite;
		greyTile = (GameObject)Instantiate(Resources.Load("Prefabs/greyTile"));
		greyTile.transform.parent = this.transform;
		greyTile.SetActive(false);
		turnOverTile = (GameObject)Instantiate(Resources.Load("Prefabs/blackoutTile"));
		turnOverTile.transform.parent = this.transform;
		turnOverTile.SetActive(false);
		setWidthAndHeight();
		this.ani = gameObject.GetComponent<Animator>();
		ani.SetBool("click", false);
		ani.SetBool("enemy", false);
	}

	public override void highlight() {
		turnOverTile.SetActive(false);
		greyTile.SetActive(false);
		/*SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		if(isOccupied())	sr.sprite = occupiedSprite;
		else 				sr.sprite = highLightSprite;*/
		ani.SetBool("click", true);
	}
	
	public override void deselect() {
		turnOverTile.SetActive(false);
		greyTile.SetActive(false);
		/*SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		sr.sprite = normalSprite;*/
		ani.SetBool("click", false);
		ani.SetBool("enemy", false);
	}
	public override void highlightEnemy() {
		turnOverTile.SetActive(false);
		greyTile.SetActive(false);
		//SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		//sr.sprite = this.enemyTileSprite;
		ani.SetBool("enemy", true);
	}
}
