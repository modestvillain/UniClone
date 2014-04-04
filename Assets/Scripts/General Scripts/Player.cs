using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player:MonoBehaviour  {
	public GameObject player;
	public Sprite normalSprite;
	int numMoveTiles = 1;
	public HexTile currentTileScript;
	//GameObject Map;








	public void move(GameObject hextile){

		//set the location equal to the corresponding tile's position
		//if checking the map reveals that the move is okay{
		HexTile hexscript = hextile.GetComponent<HexTile>();
		this.player.transform.position = hexscript.center;
		//remove self from occupant of previous tile
		//put this one as occupied
		hexscript.occupant = this.player;
		this.currentTileScript.occupant = null;
		hexscript.deselect();
		this.currentTileScript = hexscript;
		//}

	}//move
}
