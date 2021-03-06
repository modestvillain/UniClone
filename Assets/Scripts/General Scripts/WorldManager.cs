﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour {

	public static Map map;
//	public static AI AI;
//	public static AI AI2;
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
						WINSTATE = false,
						AITHINKING = false,
						DUMMY = true;
				
	public static TeamManager nuetralScript;
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
		nuetralScript = new TeamManager();
		aerialStats = new AerialStats();
		soldierStats = new SoldierStats();
		heavyStats = new HeavyStats();

		//AI = GameObject.FindGameObjectWithTag("AI").GetComponent<AI>();

		AI = GameObject.FindGameObjectWithTag("AI").GetComponent<DummyAI>();

	}

	public static void endPlayerTurn() {
		WorldManager.AITHINKING =true;
		PLAYERMODE = false;
		switchPlayer = false;
		switchAI = true;
		blueScript.removePlayersFromCapturedBases();
		redScript.removePlayersFromCapturedBases();
		removeTurnOverTiles();
		foreach(Player p in blueScript.team) {
			p.endTurn();
		}
	}

	public static void endAITurn() {
		WorldManager.AITHINKING = false;
		PLAYERMODE = true;
		switchAI = false;
		switchPlayer = true;
		blueScript.removePlayersFromCapturedBases();
		redScript.removePlayersFromCapturedBases();
		foreach(Player p in redScript.team) {
			p.endTurn();
		}
	}

	void Update () {
		if(redWon()) {
			WINSTATE = true;
			WINNER = "RED";
			switchPlayer = false;
			switchAI = false;
		}
		if(blueWon()) {
			WINSTATE = true;
			WINNER = "BLUE";
			switchPlayer = false;
			switchAI = false;
		}
		if(switchPlayer) {
			beginPlayerTurn();
		}
		else if(switchAI) {
			beginAITurn();
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
		foreach(Player player in blueScript.team) {
			player.disableTurnOverTile();
		}
		foreach(Player player in redScript.team) {
			player.disableTurnOverTile();
		}
	}//method

	void OnGUI() {
		if(WorldManager.AITHINKING){
			gameObject.GetComponent<WorldMenu>().AIStatus();
		}
	
		if(WorldManager.WINSTATE) {
			gameObject.GetComponent<WorldMenu>().goToWinState();
		}

		if(!WorldManager.AITHINKING){
			gameObject.GetComponent<WorldMenu>().makeMenu();
		}

	}

	public static void beginPlayerTurn() {
		switchPlayer = false;
		WorldManager.AITHINKING = false;
		blueScript.addCredits();
		removeTurnOverTiles();
		PLAYERMODE = true;
	}

	public static void beginAITurn() {
		switchAI = false;
		WorldManager.AITHINKING = true;
		WorldManager.PLAYERMODE = false;
		redScript.addCredits();	

		//AI.startTurn(WorldManager.redScript,WorldManager.blueScript,WorldManager.map);
		

		AI.startTurn();

		endAITurn();
	}
}
