using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player  {
	GameObject player;
	Sprite normalSprite;


	public Player(List<GameObject> tileList,  Sprite normalSprite){
		this.createPlayerInRandomLocation(tileList, normalSprite);
	}


	void randomSpawn(List<GameObject> tileList){
		//int rand = Random.Range(0, tileList.Count-1);
		//this.player.transform.position = new Vector2(tileList[rand].transform.position.x, tileList[rand].transform.position.y);

	}

	GameObject createPlayerInRandomLocation(List<GameObject> tileList, Sprite normalSprite){
		this.normalSprite = normalSprite;
		this.player = new GameObject("player");

		player.AddComponent("PolygonCollider2D");
		player.AddComponent("SpriteRenderer");
		player.GetComponent<SpriteRenderer>().sprite = normalSprite;
		//int rand = Random.Range(0, tileList.Count -1);
		player.transform.position = new Vector2(tileList[0].transform.position.x, tileList[0].transform.position.y);
		Debug.Log("player pos " + player.transform.position);
		player.AddComponent("Player");
		GameObject.Instantiate(this.player);
		return player;
		
	}
}
