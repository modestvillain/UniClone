using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player:MonoBehaviour  {
	public GameObject player;
	public Sprite normalSprite;
	public HexTile currentTileScript;
	public ArrayList actionsList;
	public TeamManager TM;
	public int HP;//health
	public int DMG;//damage - attack strength
	public int DEF;//defense
	public int MOB;//mobility
	public int cost;
	public int attackRange;
	public bool canCapture;
	public bool canAttackAfterMove;
	public int repair;
	public bool turnIsOver = false;
	public GameObject turnOverTile;
	public bool canMove = false;
	public bool canAttack = false;  	//for menu
	public bool isOn = false;

	void Start() {

	}

	void OnEnable() {
		setup ();
	}

	public void setup() {
		player = gameObject;
		turnOverTile = (GameObject)Instantiate(Resources.Load("Prefabs/blackoutTile"));
		turnOverTile.SetActive (false);
	}

	public void move(GameObject hextile) {
		player = gameObject;
		HexTile hexscript = hextile.GetComponent<HexTile>();
		this.player.transform.position = hexscript.center;
		hexscript.occupant = this.player;
		this.currentTileScript.occupant = null;
		hexscript.deselect();
		this.currentTileScript = hexscript;
	}
	
	public void attack(Player enemyScript) {
		enemyScript.HP -= this.DMG*(this.DMG/enemyScript.DEF);
		if(enemyScript.HP <=0) {
			enemyScript.currentTileScript.removeOccupant(enemyScript.TM.team);
		}
		this.endTurn();
	}

	public void destroyPlayer(HexTile tileScript) {
		tileScript.occupant = null;//change the occupied status of the hex
		WorldManager.players.Remove(this);//remove from the player array in world manager
	}

	public void capture(Base b) {
		//if base is not already yours and you can capture bases..
		bool isAlreadyYourBase = WorldManager.blueScript.bases.Contains(b);
		if (canCapture && !isAlreadyYourBase) {
			player.transform.position = b.center;
			b.occupant = this.player;
			currentTileScript.occupant = null;
			b.deselect();
			currentTileScript = b;
			endTurn();
			b.changeSides(gameObject.transform.parent.tag, TM);
			b.hasBeenCaptured = true;
		}
	}

	void OnGUI() {
		if(isOn) {
			int space = 0;//has spacing of thirty
			if(canMove) {
				if(GUI.Button(new Rect(20,40,80,20), "Move")) {
					//Application.LoadLevel(1);
				}
				space +=30;
			}
			if(canAttack) {
				if(GUI.Button(new Rect(20,40 + space,80,20), "Attack")) {
					//Application.LoadLevel(2);
				}
				space+=30;
			}
			if(GUI.Button(new Rect(20,40 + space,80,20), "Cancel")) {
				//then cancel
			}
			space +=30;
			// Make a background box
			GUI.Box(new Rect(10,10,100,30 + space), "Actions");
		}
	}

	public void turnMenuOn() {
		isOn = true;
	}

	public void turnMenuOff() {
		isOn = false;
	}

	public void allMenuActionsOn() {
		turnMenuOn();
		canMove = true;
		canAttack = true;
		}

	public void endTurn() {
		turnOverTile.transform.position = currentTileScript.gameObject.transform.position;
		turnIsOver = true;
		turnOverTile.SetActive(true);
		turnMenuOff();
		WorldManager.setNormal();
	}

	public void disableTurnOverTile() {
		turnOverTile.SetActive(false);
	}

}