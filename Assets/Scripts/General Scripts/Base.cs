using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Base : HexTile {

	public Sprite blueBaseSprite;
	public Sprite redBaseSprite;
	public Sprite greyBaseSprite;
	public TeamManager team;
	
	void Start () {
		
	}

	void Update () {
		
	}

	public void spawnPlayer() {

	}

	GameObject instantiatePlayer(string prefabName) {

		string path = "Prefabs/" + prefabName;
		GameObject player = (GameObject)Instantiate(Resources.Load(path));
		player.name = prefabName;
		player.tag = prefabName;
		Player playerScript = WorldManager.getPlayerScript(player);
		playerScript.player = player;
		map.player = playerScript;
		player.transform.parent = team.transform;
		return player;
	}
	
	public void highlight() {
		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
	}
	
	public void deselect() {
		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
	}
	
	public void setBase(int baseType) {
		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		switch(baseType) {
		case 0:
			team = GameObject.FindGameObjectWithTag("BLUE").GetComponent<TeamManager>();
			gameObject.transform.parent = team.transform;
			team.GetComponent<TeamManager>().bases.Add (this);
			normalSprite = blueBaseSprite;
			highLightSprite = blueBaseSprite;
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
