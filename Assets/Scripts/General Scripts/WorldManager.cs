using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour {
	
	public static Map map;
	public static DummyAI AI;
	public GameObject player;
	public Sprite playerSprite;
	public bool playerSet = false;
	public GameObject BLUE;
	public GameObject RED;
	public static TeamManager blueScript;
	public static TeamManager redScript;
	public static bool MOVEMODE = false;
	public static bool NORMALMODE = true;
	public static bool ATTACKMODE = false;
	public static bool PLAYERMODE = true;
	public static bool WINSTATE = false;
	public static string WINNER;
	public static int MODE;// int 1 is move mode, 2 means normal mode
	public static AerialStats aerialStats;
	public static SoldierStats soldierStats;
	public static List<Player> players;
	public static int numBases = 0;

	// Use this for initialization
	void Start () {
		WorldManager.map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
		BLUE = GameObject.FindGameObjectWithTag("BLUE");
		blueScript = BLUE.GetComponent<TeamManager>();
		RED = GameObject.FindGameObjectWithTag("RED");
		redScript = RED.GetComponent<TeamManager>();
		WorldManager.aerialStats = new AerialStats();
		WorldManager.soldierStats = new SoldierStats();
		WorldManager.players = new List<Player>();
		WorldManager.AI = GameObject.FindGameObjectWithTag("AI").GetComponent<DummyAI>();
	}

	public static void endPlayerTurn() {
		PLAYERMODE = false;
		GameObject.FindGameObjectWithTag("BLUE").GetComponent<TeamManager>().removePlayersFromCapturedBases();
		GameObject.FindGameObjectWithTag("RED").GetComponent<TeamManager>().removePlayersFromCapturedBases();
		foreach(Player p in blueScript.team) {
			p.endTurn();
		}
		AI.startTurn();
	}
	
	// Update is called once per frame
	void Update () {
		this.spawnPlayer();
	}

	public static void setNormal() {
		NORMALMODE = true;
		ATTACKMODE = false;
		MOVEMODE = false;
	}

	public static void setMove() {
		NORMALMODE = false;
		ATTACKMODE = false;
		MOVEMODE = true;
	}

	public static void setAttack() {
		NORMALMODE = false;
		ATTACKMODE = true;
		MOVEMODE = false;
	}

	public static void setMoveAndAttack() {
		NORMALMODE = false;
		ATTACKMODE = true;
		MOVEMODE = true;
	}

	public void spawnPlayer() {
		if(!this.playerSet && !map.empty) {

			//createPlayerInRandomLocation("Soldier", "BLUE");
			//createPlayerInRandomLocation("Aerial", "BLUE");
			//createPlayerInRandomLocation("BadGuyTest", "RED");
		
			//playerSet = true;
		}
	}

	public static void createPlayerInRandomLocation(string prefabname, string side) {
		//side = BLUE or RED
		int rand = Random.Range(0, map.tileList.Count -1);
		GameObject player = WorldManager.instantiatePlayer(prefabname, side);
		WorldManager.positionPlayer(player, WorldManager.map.tileList[rand]);
	}


	public static GameObject instantiatePlayer(string prefabName, string side) {
		string path = "Prefabs/" + prefabName;
		GameObject player = (GameObject)Instantiate(Resources.Load(path));
		player.name = prefabName;
		player.tag = prefabName;
		Player playerScript = (Player)player.GetComponent(player.tag);
		player.transform.parent = GameObject.FindGameObjectWithTag(side).transform;
		playerScript.TM = GameObject.FindGameObjectWithTag(side).GetComponent<TeamManager>();
		playerScript.TM.team.Add(playerScript);
		return player;
	}

	public static void positionPlayer(GameObject player, HexTile hextile) {
		player.transform.position = hextile.center;
		hextile.occupant = player;
		((Player)player.GetComponent(player.tag)).currentTileScript = hextile;
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
		else if(g.tag == "badAerial")
			return (Player)g.GetComponent("Aerial");
		else{
			return null;
		}
	}

	void GUIMenuTest() {
		ActionsMenu menu = (ActionsMenu)this.gameObject.AddComponent<ActionsMenu>();
		menu.canMove = true;
		menu.canAttack = true;
		menu.isOn = true;
	}

	//only for blue team
	public static void removeTurnOverTiles() {
		foreach(Player player in WorldManager.blueScript.team) {
			player.disableTurnOverTile();
		}
	}//method

	void OnGUI() {
		gameObject.GetComponent<WorldMenu>().makeMenu();
		if(WorldManager.WINSTATE) {
			gameObject.GetComponent<WorldMenu>().goToWinState();
		}
	}

	public static void beginPlayerTurn() {
		blueScript.addCredits();
		WorldManager.PLAYERMODE = true;
		removeTurnOverTiles(); 
	}
}
