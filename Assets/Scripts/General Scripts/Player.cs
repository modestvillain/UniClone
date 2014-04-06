using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player:MonoBehaviour  {
	public GameObject player;
	public Sprite normalSprite;
	public HexTile currentTileScript;
<<<<<<< HEAD

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
=======
	public int numMoveTiles;
	public int HP;
	public int DMG;
	public int DEF;
	public int MOB=2;
>>>>>>> 671d5be4b7ad0a6287596a3d2d5471975ac63912

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


	}//move
}
