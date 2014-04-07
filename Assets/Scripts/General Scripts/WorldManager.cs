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
		GUIMenuTest();
		this.map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
		BLUE = GameObject.FindGameObjectWithTag("BLUE");
		RED = GameObject.FindGameObjectWithTag("RED");
		//player.SendMessage("crecreatePlayerInRandomLocation", map.tileList);
	}
	
	// Update is called once per frame
	void Update () {
		this.spawnPlayer();
			
	}

	public void spawnPlayer(){
		if(!this.playerSet && !map.empty){

			this.createPlayerInRandomLocation("Soldier", "BLUE");
			this.createPlayerInRandomLocation("Aerial", "BLUE");
			this.createPlayerInRandomLocation("BadGuyTest", "RED");
		
			playerSet = true;
		}
	}


	void createPlayerInRandomLocation(string prefabname, string side){
		//side = BLUE or RED
		int rand = Random.Range(0, map.tileList.Count -1);
		this.positionPlayer( this.instantiatePlayer(prefabname, side), map.tileList[rand]);
	}


	GameObject instantiatePlayer(string prefabName, string side){
		string path = "Prefabs/" + prefabName;
		GameObject player = (GameObject)Instantiate(Resources.Load(path));
		player.name = prefabName;
		player.tag = prefabName;
		Player playerScript = WorldManager.getPlayerScript(player);
		playerScript.player = player;
		map.player = playerScript;
		player.transform.parent = GameObject.FindGameObjectWithTag(side).transform;
		return player;
	}

	GameObject instantiateRedPlayer(string prefabName){
		string path = "Prefabs/" + prefabName;
		GameObject player = (GameObject)Instantiate(Resources.Load(path));
		player.name = prefabName;
		player.tag = prefabName;
		Player playerScript = WorldManager.getPlayerScript(player);
		playerScript.player = player;
		map.player = playerScript;
		player.transform.parent = GameObject.FindGameObjectWithTag("BLUE").transform;
		return player;
	}


	void positionPlayer(GameObject player, HexTile hextile){
		player.transform.position = hextile.center;
		hextile.occupant = player;
		WorldManager.getPlayerScript(player).currentTileScript = hextile;
	}

	public static Player getPlayerScript(GameObject g) {
		if(g.tag == "Player")
			return(Player)g.GetComponent("Player");
		else if(g.tag == "Soldier")
			return (Player)g.GetComponent("Soldier");
		else if(g.tag == "Aerial")
			return (Player)g.GetComponent("Aerial");
		else if(g.tag == "BadGuyTest")
			return (Player)g.GetComponent("BadGuyTest");
		else{
			return null;
		}
		
	}

	void GUIMenuTest(){
		this.gameObject.AddComponent<ActionsMenu>();
	}





}
