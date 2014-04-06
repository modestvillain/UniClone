using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour {
	
	public Map map;
	public GameObject player;
	public Sprite playerSprite;
	public bool playerSet = false;
	public GameObject BLUE;
	public GameObject RED;
	public static int MOVEMODE = 1;
	public static int NORMALMODE = 2;
	public int mode;// int 1 is move mode, 2 means normal mode

	// Use this for initialization
	void Start () {
		this.map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
		BLUE = new GameObject();
		BLUE.name = "BLUE";
		BLUE.tag = "BLUE";
		RED = new GameObject();
		RED.name = "RED";
		RED.tag = "RED";
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
			this.createSoldierInRandomLocation();
			playerSet = true;
		}
	}

	void createPlayerInRandomLocation(List<HexTile> tileList, Sprite normalSprite){

		this.player = new GameObject("player");//this instantiates already
		player.AddComponent("SpriteRenderer");
		player.AddComponent("Player");
		player.tag = "Player";
		SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
		sr.sprite = normalSprite;
		sr.sortingOrder = 1;

		//place player
		int rand = Random.Range(0, tileList.Count -1);
		player.transform.position = new Vector2(tileList[rand].center.x, tileList[rand].center.y);
		//tile now occupied
		tileList[rand].occupant = player;
		Player playerScript = (Player)player.GetComponent("Player");
		playerScript.normalSprite = normalSprite;
		playerScript.player = player;
		playerScript.currentTileScript = tileList[rand];
		map.player = playerScript;
	}

	void createSoldierInRandomLocation(){

		int rand = Random.Range(0, map.tileList.Count -1);

		this.positionPlayerSoldier(this.instantiatePlayerSoldier(), map.tileList[rand]);


	}

	GameObject instantiatePlayerSoldier(){
		GameObject solider = (GameObject)Instantiate(Resources.Load("Prefabs/Soldier"));
		solider.name = "soldier";
		solider.tag = "Player";
		solider.GetComponent<Soldier>().player = solider;
		return solider;		
	}

	void positionPlayerSoldier(GameObject solider, HexTile hextile){
		solider.transform.position = hextile.center;
		hextile.occupant = solider;
		solider.GetComponent<Soldier>().currentTileScript = hextile;

	}






}
