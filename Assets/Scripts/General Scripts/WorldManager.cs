using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour {

	public static Map map;
	public static DummyAI AI;
	public static DummyAI AI2;
	public GameObject player;
	public Sprite playerSprite;
	public bool playerSet = false;
	public GameObject BLUE;
	public GameObject RED;
	public static TeamManager blueScript;
	public static TeamManager redScript;
	public static bool 	MOVEMODE = false,
						NORMALMODE = true,
						ATTACKMODE = false,
						PLAYERMODE = true,
						WINSTATE = false;
	public static string WINNER;
	public static int MODE;// int 1 is move mode, 2 means normal mode
	public static AerialStats aerialStats;
	public static SoldierStats soldierStats;
	public static HeavyStats heavyStats;
	public static List<Player> players;
	public static int numBases = 0;
	public static bool 	switchAI=false,
						switchPlayer=true;


	// Use this for initialization

	void OnEnable () {
		WorldManager.map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
		BLUE = GameObject.FindGameObjectWithTag("BLUE");
		blueScript = new TeamManager(BLUE);
		RED = GameObject.FindGameObjectWithTag("RED");
		redScript = new TeamManager(RED);
		aerialStats = new AerialStats();
		soldierStats = new SoldierStats();
		heavyStats = new HeavyStats();
		AI = GameObject.FindGameObjectWithTag("AI").GetComponent<DummyAI>();
		AI2 = GameObject.FindGameObjectWithTag("AI2").GetComponent<DummyAI>();
		AI.TM = blueScript;
		AI.BLUE = redScript;
		AI2.TM = redScript;
		AI2.BLUE = redScript;
		//WorldManager.mainCamera = Camera.m(Camera)GameObject.FindGameObjectWithTag("MainCamera");
	}

	public static void endPlayerTurn() {

		PLAYERMODE = false;
		blueScript.removePlayersFromCapturedBases();
		redScript.removePlayersFromCapturedBases();
		foreach(Player p in blueScript.team) {
			p.endTurn();
		}
		switchAI = true;
	}

	public static void endAITurn() {
		//PLAYERMODE = true;
		blueScript.removePlayersFromCapturedBases();
		redScript.removePlayersFromCapturedBases();
		foreach(Player p in redScript.team) {
			p.endTurn();
		}
		switchPlayer = true;
	}

	void Update () {
		if(redWon()) {
			WINSTATE = true;
			WINNER = "RED";
		}
		if(blueWon()) {
			WINSTATE = true;
			WINNER = "BLUE";
		}
		if(switchPlayer) {
			Debug.Log("AI");
			beginPlayerTurn();
			switchPlayer = false;
		}
		else if(switchAI) {
			Debug.Log("AI2");
			beginAITurn();
			switchAI = false;
		}
	}

	public static bool redWon() {
		return blueScript.bases.Count==0;
	}

	public static bool blueWon() {
		return redScript.bases.Count==0;
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

	public static GameObject instantiatePlayer(string prefabName, string side) {
		string path = "Prefabs/" + prefabName;
		GameObject player = (GameObject)Instantiate(Resources.Load(path));
		player.name = prefabName;
		player.tag = prefabName;
		Player playerScript = (Player)player.GetComponent(player.tag);
		player.transform.parent = GameObject.FindGameObjectWithTag(side).transform;
		playerScript.TM = getTM(side);
		playerScript.TM.team.Add(playerScript);

		return player;
	}

	public static TeamManager getTM(string side) {
		if(side=="BLUE")
			return blueScript;
		else
			return redScript;
	}

	public static void positionPlayer(GameObject player, HexTile hextile) {
		player.transform.position = hextile.center;
		hextile.occupant = player;
		((Player)player.GetComponent(player.tag)).currentTileScript = hextile;
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
		AI.startTurn();
		AI.endAITurn();
		switchPlayer = false;
		switchAI = true;
	}

	public static void beginAITurn() {
		redScript.addCredits();
		WorldManager.PLAYERMODE = false;
		AI2.startTurn();
		AI2.endAITurn();
		switchPlayer = true;
		switchAI = false;
	}
}
