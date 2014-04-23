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
	public string type;

	public GUIText healthDisplay;
	

	void Start() {

	}

	void Update(){

	
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
		enemyScript.HP -= (int)(this.DMG*((float)this.DMG/enemyScript.DEF));
		if(enemyScript.HP <=0) {
			enemyScript.currentTileScript.removeOccupant(enemyScript.TM.team);
			enemyScript.disableTurnOverTile();
		}
		if(TM.parent.tag=="BLUE") {
			this.endTurn();
		}
	}

	public void destroyPlayer(HexTile tileScript) {
		tileScript.occupant = null;//change the occupied status of the hex
		WorldManager.players.Remove(this);//remove from the player array in world manager
	}

	public void capture(Base b) {
		//if base is not already yours and you can capture bases..
		bool isAlreadyYourBase;
		if(b.TM.parent != null)	isAlreadyYourBase = b.TM.parent.tag == TM.parent.tag;
		else 					isAlreadyYourBase = false;
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
		else {
			move(b.gameObject);
		}
	}

	void OnGUI() {

		Vector2 point = new Vector2(currentTileScript.center.x, currentTileScript.center.y);
		GUIStyle style = new GUIStyle();
		style.fontSize = 20;
		style.onNormal.textColor = Color.cyan;
		style.onActive.textColor = Color.cyan;
		style.onFocused.textColor = Color.cyan;
		style.onHover.textColor = Color.cyan;
		style.normal.textColor = Color.green;
		GUI.Label (new Rect (Camera.main.WorldToScreenPoint(point).x + 2, Screen.height - Camera.main.WorldToScreenPoint(point).y  ,100,50), this.HP.ToString(), style);


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

	public string getSide(){
		return gameObject.transform.parent.tag;
	}

	public TeamManager getEnemy(){
		TeamManager enemy;
		if(this.getSide() == "RED"){
			enemy = WorldManager.blueScript;
		}
		else{
			enemy = WorldManager.redScript;
		}
		return enemy;
	}

	public Player closestEnemyPlayer(){
		TeamManager enemy = this.getEnemy();
			int distance = 999999;
			Player closest = null;
			foreach(Player ep in enemy.team){
				int newDist = this.distanceToAnotherPlayer(ep);
				if(newDist < distance){
					distance = newDist;
					closest = ep;
				}
			}
			//return least distance hextile
			return closest;
		}


	public Base closestEnemeyBase(){
		return this.closestBase(this.getEnemy());
	}

	public Base closestNuetralBase(){
		return this.closestBase(WorldManager.nuetralScript);
	}

	public Base closestBase(TeamManager tm){
		//go through all enemy bases after determining who is the enemy
		int distance = 999999;
		Base closestBase = null;//tm.bases[0];
		foreach(Base b in tm.bases){
			//get distance to it from tile player is on
			int newDist = b.distanceFromBase(this.currentTileScript);
			if(newDist < distance){
				distance = newDist;
				closestBase = b;
			}
		}
		//return least distance hextile
		return closestBase;
	}

	public Base closestEnemeyOrNuetralBase(){
		Base nuetral = this.closestNuetralBase();
		Base enemy = this.closestEnemeyBase();
		if(nuetral == null && enemy == null){
			return null;
		}
		else if(nuetral == null && enemy!=null){
			return enemy;
		}
		else if(enemy == null && nuetral!= null){
			return nuetral;
		}
		else if(nuetral.distanceFromBase(this.currentTileScript) < enemy.distanceFromBase(this.currentTileScript)){
			return nuetral;
		}
		else{
			return enemy;
		}
	}

	public int distanceToAnotherPlayer(Player p){
		return (int)Mathf.Sqrt((int)Mathf.Pow(p.currentTileScript.x - this.currentTileScript.x,2) + (int)Mathf.Pow(p.currentTileScript.y - this.currentTileScript.y,2));
	}

}//class
