using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player:MonoBehaviour  {
	public GameObject player;
	public Sprite normalSprite;
	public HexTile currentTileScript;
	public ArrayList actionsList;
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

	//for menu
	public bool canMove = false;
	public bool canAttack = false;
	public bool isOn = false;

	public void move(GameObject hextile){

		//set the location equal to the corresponding tile's position
		//if checking the map reveals that the move is okay
		HexTile hexscript = hextile.GetComponent<HexTile>();
		this.player.transform.position = hexscript.center;
		//remove self from occupant of previous tile
		//put this one as occupied
		hexscript.occupant = this.player;

		this.currentTileScript.occupant = null;
		hexscript.deselect();
		this.currentTileScript = hexscript;
		this.endTurn();




	}//move

	//attack
	public void attack(Player enemyScript){
		if(this.DMG> enemyScript.DEF){
			enemyScript.HP -= this.DMG;
			if(enemyScript.HP <=0){
				Destroy(enemyScript.gameObject);
			}
		}
		else{
			this.HP -= enemyScript.DMG;
			if(this.HP <=0){
				Destroy(this.gameObject);
			}
		}

		this.endTurn();
	}

	void OnGUI(){
		
		if(isOn){
			int space = 0;//has spacing of thirty
			
			
			if(canMove){
				if(GUI.Button(new Rect(20,40,80,20), "Move")) {
					//Application.LoadLevel(1);
				}
				space +=30;
			}
			
			
			if(canAttack){
				if(GUI.Button(new Rect(20,40 + space,80,20), "Attack")) {
					//Application.LoadLevel(2);
				}
				space+=30;
			}
			
			if(GUI.Button(new Rect(20,40 + space,80,20), "Cancel")){
				//then cancel
				
			}
			space +=30;
			
			// Make a background box
			GUI.Box(new Rect(10,10,100,30 + space), "Actions");
		}
	}//method

	public void turnMenuOn(){
		this.isOn = true;
	}

	public void turnMenuOff(){
		this.isOn = false;
	}

	public void allMenuActionsOn(){
		this.turnMenuOn();
		this.canMove = true;
		this.canAttack = true;
		}

	public void endTurn(){
		this.turnIsOver = true;
		this.turnMenuOff();
		this.turnOverTile = (GameObject)Instantiate(Resources.Load("Prefabs/blackoutTile"));
		turnOverTile.transform.position = currentTileScript.gameObject.transform.position;
		WorldManager.MODE = WorldManager.NORMALMODE;
	}
}
