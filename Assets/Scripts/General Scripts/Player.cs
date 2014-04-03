using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player  {
	GameObject player;
	Sprite normalSprite;


	public Player(List<HexTile> tileList,  Sprite normalSprite){
		this.createPlayerInRandomLocation(tileList, normalSprite);
	}




	GameObject createPlayerInRandomLocation(List<HexTile> tileList, Sprite normalSprite){
		this.normalSprite = normalSprite;
		this.player = new GameObject("player");//this instantiates already

		player.AddComponent("PolygonCollider2D");
		player.AddComponent("SpriteRenderer");
		SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
		sr.sprite = normalSprite;
		sr.sortingOrder = 1;
		int rand = Random.Range(0, tileList.Count -1);
		player.transform.position = new Vector2(tileList[rand].center.x, tileList[rand].center.y);
		return player;
		
	}
}
