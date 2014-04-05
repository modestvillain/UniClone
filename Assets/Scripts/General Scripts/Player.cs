using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player:MonoBehaviour  {
	public GameObject player;
	public Sprite normalSprite;
	public HexTile currentTileScript;
	public int numMoveTiles;
	public int HP;
	public int DMG;
	public int DEF;
	public int MOB=2;

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
