using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour {
	
	public Map map;
	public GameObject player;
	public Sprite playerSprite;
	bool playerSet = false;
	public static int MOVEMODE = 1;
	public static int NORMALMODE = 2;
	public int mode;// int 1 is move mode, 2 means normal mode

	// Use this for initialization
	void Start () {
		this.map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();


		//player.SendMessage("crecreatePlayerInRandomLocation", map.tileList);
	}
	
	// Update is called once per frame
	void Update () {
			//map.Update();
		this.spawnPlayer();
			
	}

	public void spawnPlayer(){
		if(!this.playerSet && !map.empty){
			this.createPlayerInRandomLocation(this.map.tileList, playerSprite);
			playerSet = true;
		}
	}

	void createPlayerInRandomLocation(List<HexTile> tileList, Sprite normalSprite){

		this.player = new GameObject("player");//this instantiates already
		player.AddComponent("SpriteRenderer");
		player.AddComponent("Player");
		SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
		sr.sprite = normalSprite;
		sr.sortingOrder = 1;
		int rand = Random.Range(0, tileList.Count -1);
		player.transform.position = new Vector2(tileList[rand].center.x, tileList[rand].center.y);
		tileList[rand].occupant = player;

		Player playerScript = (Player)player.GetComponent("Player");
		playerScript.normalSprite = normalSprite;
		playerScript.player = player;

	}






}
