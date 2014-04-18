using UnityEngine;
using System.Collections;

public class WaterTile : HexTile {

	void OnEnable() {
		normalSprite = Resources.Load<Sprite>("Sprites/waterTile");
		highLightSprite = Resources.Load<Sprite>("Sprites/waterTileHighlight");
		occupiedSprite = Resources.Load<Sprite>("Sprites/waterTileHighlight");
		greyOutSprite = Resources.Load<Sprite>("Sprites/greyTile");
		enemyTileSprite = Resources.Load<Sprite>("Sprites/waterTileEnemy");
		gameObject.GetComponent<SpriteRenderer>().sprite = normalSprite;
		turnOverTile = (GameObject)Instantiate(Resources.Load("Prefabs/blackoutTile"));
		turnOverTile.SetActive(false);
		setWidthAndHeight();
	}
}
