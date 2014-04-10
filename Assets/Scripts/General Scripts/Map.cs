using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Map : MonoBehaviour {

	public HexTile[,] tiles;
	public List<HexTile> tileList;
	public GameObject whiteHexTile;
	public WorldManager worldManager;
	public Player player;
	public int modX=0;
	public int realWidth;
	public int mapWidth;
	public int mapHeight;
	public bool empty=true;
	public Base lastBaseSelected;

	void Start() {

		mapWidth = 8;
		mapHeight = 8;
		realWidth = mapWidth + (int)((mapHeight - 1) / 2);
		tiles = new HexTile[realWidth,mapHeight];
		this.createMap();
		this.worldManager = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>();
	}

	public void Update() {

		if(!empty){
			this.selectTile();
		}
	}

	/*
	 * Instantiates hex tile, places it on map, adds it to tiles and tileList
	 */
	public void createHexTile(float x, float y, int upDown, float widthAway) {

		GameObject newT = (GameObject) Instantiate(Resources.Load("Prefabs/whiteHexTile"));
		newT.transform.parent = this.transform;
		HexTile hextile = newT.GetComponent<HexTile>();
		hextile.map = this;

		Vector2 loc = new Vector2(1.5f*x,y);
		Vector2 pos = new Vector2();

		switch (upDown) {
			case 0:
				pos.x = loc.x + widthAway;
				pos.y = loc.y*1.3f;
				break;
			case 1:
				pos.x = loc.x + widthAway-.7f;
				pos.y = loc.y*1.3f;
				break;
			default:									/* COPY OF CASE 0 FOR INITIALIZATION, MINUS X MODIFICATION */
				pos.x = loc.x + widthAway;
				pos.y = loc.y*1.3f;
				break;
		}
		newT.transform.position = pos;

		hextile.position = pos;
		hextile.location = loc;
		hextile.x = modX;
		hextile.y = (int)y;
		hextile.setWidthAndHeight();
		hextile.center = new Vector2(pos.x + .1f, pos.y + .1f);

		tileList.Add(hextile);
		tiles[hextile.x,hextile.y]=hextile;
		modX++;
	}

	/*
	 * Instantiates base, places it on map, adds it to tiles and tileList
	 */
	public void createBase(float x, float y, int upDown, float widthAway, int baseType) {
		
		GameObject newBase = (GameObject) Instantiate(Resources.Load("Prefabs/Base"));
		Base baseScript = newBase.GetComponent<Base>();
		baseScript.map = this;
		
		Vector2 loc = new Vector2(1.5f*x,y);
		Vector2 pos = new Vector2();
		
		switch (upDown) {
		case 0:
			pos.x = loc.x + widthAway;
			pos.y = loc.y*1.3f;
			break;
		case 1:
			pos.x = loc.x + widthAway-.7f;
			pos.y = loc.y*1.3f;
			break;
		default:									/* COPY OF CASE 0 FOR INITIALIZATION, MINUS X MODIFICATION */
			pos.x = loc.x + widthAway;
			pos.y = loc.y*1.3f;
			break;
		}
		newBase.transform.position = pos;

		baseScript.setBase(baseType);
		baseScript.position = pos;
		baseScript.location = loc;
		baseScript.x = modX;
		baseScript.y = (int)y;
		baseScript.setWidthAndHeight();
		baseScript.center = new Vector2(pos.x + .1f, pos.y + .1f);
		
		tileList.Add(baseScript);
		tiles[baseScript.x,baseScript.y]=baseScript;
		modX++;
	}

	/*
	 * Creates hex grid, establishes coordinate system
	 */
	public void createMap() {

		float widthAway = .5f;
		int off=-1;										/* FOR INITIALIZING FIRST / BOTTOM ROW */
		int count=0;

		for(int y = 0; y < mapHeight; y++) {
			for(int x = 0; x < mapWidth; x++) {
				if(y==0 && x==0) {
					createBase(x, y, off, widthAway,0);
				}
				else if(y==mapHeight-1 && x==mapWidth-1) {
					createBase(x, y, off, widthAway,1);
				}
				else {
					createHexTile(x, y, off, widthAway);
				}
			}
			modX=count;
			if(off==1) {off=0; modX=++count;}
			else if(off==0) off=1;
			else off=1;
		}

		createAdjacencies();
		empty = false;
	}

	/*
	 * Using self-defined coordinate system, find neighboring tiles
	 * and create adjacency list for each individual hex tile
	 */
	public void createAdjacencies() {

		foreach(HexTile ht in tileList) {
			int xp=ht.x+1;
			int xn=ht.x-1;
			int yp=ht.y+1;
			int yn=ht.y-1;

			/* CLOCKWISE STARTING AT TILE TO RIGHT */

			if(xp<realWidth) {					// +1, 0
				if(tiles[xp,ht.y]!=null)	ht.neighbors.Add(tiles[xp,ht.y]);
			}
			if(yn>=0) {							//  0,-1
				if(tiles[ht.x,yn]!=null)	ht.neighbors.Add(tiles[ht.x,yn]);
			}
			if(xn>=0 && yn>=0) {				// -1,-1
				if(tiles[xn,yn]!=null)		ht.neighbors.Add(tiles[xn,yn]);
			}
			if(xn>=0) {							// -1, 0
				if(tiles[xn,ht.y]!=null)	ht.neighbors.Add(tiles[xn,ht.y]);
			}
			if(yp<mapHeight) {					//  0,+1
				if(tiles[ht.x,yp]!=null)	ht.neighbors.Add(tiles[ht.x,yp]);
			}
			if(xp<realWidth && yp<mapHeight) {	// +1,+1
				if(tiles[xp,yp]!=null)		ht.neighbors.Add(tiles[xp,yp]);
			}
		}
	}

	/*
	 * Consider legal tiles for a given player to move to
	 * @return	list of tiles a player can reach
	 */
	public List<HexTile> legalMoves(Player p) {

		List<HexTile> legal = new List<HexTile>(p.currentTileScript.neighbors);
		List<HexTile> temp = new List<HexTile>();

		for(int i=1; i<p.MOB; i++) {
			foreach(HexTile hex in legal) {
				foreach(HexTile ht in hex.neighbors) {
					if(!temp.Contains(ht)) {
						temp.Add (ht);
					}
				}
			}
			foreach(HexTile hex in temp) {
				legal.Add(hex);
			}
			temp = new List<HexTile>();
		}

		return legal;
	}

	public List<HexTile> legalAttacks(Player p) {
		
		List<HexTile> inRange = new List<HexTile>(p.currentTileScript.neighbors);
		List<HexTile> temp = new List<HexTile>();
		
		for(int i=1; i<p.attackRange; i++) {
			foreach(HexTile hex in inRange) {
				foreach(HexTile ht in hex.neighbors) {
					if(!temp.Contains(ht)) {
						temp.Add (ht);
					}
				}
			}
			foreach(HexTile hex in temp) {
				inRange.Add(hex);
			}
			temp = new List<HexTile>();
		}
		List<HexTile> legal = new List<HexTile>();
		foreach(HexTile hex in inRange) {
			if(hex.isOccupied() && p.team != ((Player)hex.occupant.GetComponent(hex.occupant.tag)).team)
				legal.Add(hex);
		}
		return legal;
	}

	/*
	 * Since the "player" can be one of many different kinds,
	 * this method pulls the script for the correct player type
	 */
	public void setPlayerScript(GameObject g) {

		if(g.tag == "Player")
			player = (Player)g.GetComponent("Player");
		else if(g.tag == "Soldier")
			player = (Player)g.GetComponent("Soldier");
	}

	/*
	 * Pretty self-explanatory, selects and highlights tiles under
	 * different conditions:
	 * 		1. If tile selected with player on it, consider legal actions
	 * 		2. If #1 was the case last time, the new selected tile should
	 * 		   indicate the new tile to move to, or the opponent to attack
	 * 		3. If nothing is special about the tile, just highlight the tile
	 * 		   and ignore history
	 */
	public void selectTile() {

		if (Input.GetMouseButtonDown(0)) {
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			
			if(hit)	{
				GameObject[] bases = GameObject.FindGameObjectsWithTag("Base");
				foreach(HexTile tile in tileList) {
					if(tile.gameObject != hit.collider.gameObject) {
						tile.deselect();
					}
				}
//				Debug.Log(hit.collider.gameObject.GetComponent<HexTile>().occupant.tag);

				//if you hit a tile...
				if(hit.collider.tag == "hexTile") {
					List<HexTile> legalTiles = legalMoves (player);
					HexTile hexScript = hit.collider.gameObject.GetComponent<HexTile>();
					if(WorldManager.MOVEMODE && hexScript.isOccupied() && hexScript.occupant.transform.parent.tag != player.transform.parent.tag) {
						WorldManager.setAttack();
					}
					hexScript.deselect();
					/* CHECK IF IN MOVE MODE, IF DESTINATION IS LEGAL, AND IF PLAYER IS ALREADY ON TILE*/
					if(WorldManager.MOVEMODE) {
						//if next selected tile is empty.
						if(legalTiles.Contains(hexScript) && player.currentTileScript!=hexScript) {
							if(!hexScript.isOccupied()){
								player.move(hit.collider.gameObject);
								hexScript.deselect ();
							}
						}
//						WorldManager.MODE = WorldManager.NORMALMODE;
						List<HexTile> attacks = legalAttacks(player);
						if (attacks.Count>0) {
							WorldManager.setAttack ();
							foreach(HexTile hex in attacks) {
								hex.highlightEnemy();
							}
						}
						else {
							WorldManager.setNormal();
							player.endTurn();
						}
						//if next selected tile contains an enemy..
					}
					else if(WorldManager.ATTACKMODE || WorldManager.MOVEMODE) {
						List<HexTile> attacks = legalAttacks(player);
						if(hexScript.isOccupied()) {
							if(hexScript.occupant.transform.parent.tag != player.transform.parent.tag && attacks.Contains(hexScript)){
								hexScript.deselect();
								Player enemyScript = WorldManager.getPlayerScript(hexScript.occupant);
								Debug.Log ("Attacking enemy, health is: " + enemyScript.HP );
								player.attack(enemyScript);
								Debug.Log ("Attacking enemy, health is NOW: " + enemyScript.HP );
							}
						}
						WorldManager.setNormal();
						player.endTurn();
					}
					else {
						WorldManager.setNormal();
						//if you selected your own memeber..
						if(hexScript.isOccupied() && hexScript.occupant.transform.parent.tag == "BLUE") {
							WorldManager.setMove();
							player = (Player)hexScript.occupant.GetComponent(hexScript.occupant.tag);
							//player.allMenuActionsOn();
							legalTiles = legalMoves (player);
							List<HexTile> attacks = legalAttacks(player);
							foreach(HexTile hex in tileList){
								hex.greyOut();
							}
							foreach(HexTile hex in legalTiles) {
								hex.deselect();
							}
							foreach(HexTile hex in attacks) {
								hex.highlightEnemy();
							}
							if(lastBaseSelected!=null){
								lastBaseSelected.SendMessage("deselect");
							}
							hexScript.highlight();
						}

						else if(!hexScript.isOccupied()) {
							hexScript.highlight();
						}

					}
				}
				else if(hit.collider.tag == "Base" && hit.collider.transform.parent.tag != "RED") {
					hit.collider.gameObject.GetComponent<Base>().baseSelected();
					this.lastBaseSelected = hit.collider.gameObject.GetComponent<Base>();
					player.isOn = false;// makes the menu turn off
				}

				if(this.player!=null && WorldManager.NORMALMODE){
					player.isOn = false;// makes the menu turn off
					if(hit.collider.tag != "Base"){

						if(lastBaseSelected!= null){
							lastBaseSelected.SendMessage("deselect");
						}

					}

				
				}

			}
		}//if
	}//method
}//class
