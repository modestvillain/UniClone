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
		this.createRandomMap();
		this.worldManager = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>();
	}

	public void Update() {
		if(!empty) {
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
		hextile.center = new Vector2(pos.x + .1f, pos.y + .1f);

		tileList.Add(hextile);
		tiles[hextile.x,hextile.y]=hextile;
//		modX++;
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
		baseScript.center = new Vector2(pos.x + .1f, pos.y + .1f);
		
		tileList.Add(baseScript);
		tiles[baseScript.x,baseScript.y]=baseScript;
//		modX++;
	}

	public void createWaterTile(float x, float y, int upDown, float widthAway) {

		GameObject newT = (GameObject) Instantiate(Resources.Load("Prefabs/waterTile"));
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
		hextile.center = new Vector2(pos.x + .1f, pos.y + .1f);
		
		tileList.Add(hextile);
		tiles[hextile.x,hextile.y]=hextile;
//		modX++;
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
				modX++;
			}
			modX=count;
			if(off==1) {off=0; modX=++count;}
			else if(off==0) off=1;
			else off=1;
		}

		createAdjacencies();
		empty = false;
	}

	public void createRandomMap() {

		float widthAway = .5f;
		int off=-1;										/* FOR INITIALIZING FIRST / BOTTOM ROW */
		int count=0;
		modX=0;

		int basex = Random.Range(realWidth/4,3*realWidth/4);
		int basey = Random.Range(mapHeight/4,3*mapHeight/4);
		
		for(int y = 0; y < mapHeight; y++) {
			for(int x = 0; x < mapWidth; x++) {
				if(y==0 && x==0) {
					createBase(x, y, off, widthAway,0);
				}
				else if(y==mapHeight-1 && x==mapWidth-1) {
					createBase(x, y, off, widthAway,1);
				}
				else if(y==basey && x==basex) {
					createBase(x, y, off, widthAway,-1);
				}
				else {
					if(Random.Range(0.0f,10.0f)<8.0f)
						createHexTile(x, y, off, widthAway);
					else
						createWaterTile(x, y, off, widthAway);
				}
				modX++;
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
	public bool createAdjacencies() {

		bool hasAdj;
		bool connected = true;
		foreach(HexTile ht in tileList) {
			int xp=ht.x+1;
			int xn=ht.x-1;
			int yp=ht.y+1;
			int yn=ht.y-1;
			hasAdj = false;
			/* CLOCKWISE STARTING AT TILE TO RIGHT */

			if(xp<realWidth) {					// +1, 0
				if(tiles[xp,ht.y]!=null)	{ht.neighbors.Add(tiles[xp,ht.y]); hasAdj=true;}
			}
			if(yn>=0) {							//  0,-1
				if(tiles[ht.x,yn]!=null)	{ht.neighbors.Add(tiles[ht.x,yn]); hasAdj=true;}
			}
			if(xn>=0 && yn>=0) {				// -1,-1
				if(tiles[xn,yn]!=null)		{ht.neighbors.Add(tiles[xn,yn]); hasAdj=true;}
			}
			if(xn>=0) {							// -1, 0
				if(tiles[xn,ht.y]!=null)	{ht.neighbors.Add(tiles[xn,ht.y]); hasAdj=true;}
			}
			if(yp<mapHeight) {					//  0,+1
				if(tiles[ht.x,yp]!=null)	{ht.neighbors.Add(tiles[ht.x,yp]); hasAdj=true;}
			}
			if(xp<realWidth && yp<mapHeight) {	// +1,+1
				if(tiles[xp,yp]!=null)		{ht.neighbors.Add(tiles[xp,yp]); hasAdj=true;}
			}

			if(!hasAdj) connected = false;
		}

		return connected;
	}

	/*
	 * Consider legal tiles for a given player to move to
	 * @return	list of tiles a player can reach
	 */
	public List<HexTile> legalMoves(Player p) {

		if(p==null)											/* NULL CHECK, NEED TO RETURN NO LEGAL TILES */
			return new List<HexTile>();

		List<HexTile> legal = new List<HexTile>(p.currentTileScript.neighbors);
		List<HexTile> temp = new List<HexTile>();

		for(int i=1; i<p.MOB; i++) {
			foreach(HexTile hex in legal) {
				foreach(HexTile ht in hex.neighbors) {
					if(!ht.isOccupied() && !temp.Contains(ht) && !legal.Contains(ht)) {
						temp.Add (ht);
					}
				}
			}
			foreach(HexTile hex in temp) {
				legal.Add(hex);
			}
			temp = new List<HexTile>();
		}

		List<HexTile> copy = new List<HexTile>(legal);

		foreach(HexTile ht in copy) {						/* REMOVE TILES THAT SOMEONE ELSE IS ALREADY OCCUPYING */
			if(ht.isOccupied()) {
				legal.Remove(ht);
			}
		}

		if(p.tag != "Aerial" && p.tag != "BadAerial") {		/* UNLESS AERIAL TYPE, REMOVE WATER TILES */
			foreach(HexTile ht in copy) {
				if(ht.tag == "waterTile")
					legal.Remove(ht);
			}
		}

		return legal;
	}

	/*
	 * Attacks counterpart to legalMoves
	 */
	public List<HexTile> legalAttacks(Player p) {

		if(p==null)
			return new List<HexTile>();
		List<HexTile> inRange = new List<HexTile>(p.currentTileScript.neighbors);
		List<HexTile> temp = new List<HexTile>();
		
		for(int i=1; i<p.attackRange; i++) {
			foreach(HexTile hex in inRange) {
				foreach(HexTile ht in hex.neighbors) {
					if(!temp.Contains(ht) && !inRange.Contains(ht)) {
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
			if(hex.isOccupied() && p.TM != ((Player)hex.occupant.GetComponent(hex.occupant.tag)).TM)
				legal.Add(hex);
		}

		return legal;
	}

	/*
	 * Simple method to deselect all tiles
	 */
	public void deselectAll() {
		foreach(HexTile ht in tileList) {
			ht.deselect();
		}
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

			if(hit && WorldManager.PLAYERMODE == true)	{

				foreach(HexTile tile in tileList) {
					if(tile.gameObject != hit.collider.gameObject) {
						tile.deselect();
					}
				}
				//if you hit a tile...
				if(hit.collider.tag == "hexTile" || hit.collider.tag == "waterTile" || (hit.collider.tag == "Base" && hit.collider.gameObject.GetComponent<Base>().side != "RED"
				                                     && hit.collider.gameObject.GetComponent<Base>().isOccupied())) {
					List<HexTile> legalTiles = legalMoves (player);
					HexTile hexScript = hit.collider.gameObject.GetComponent<HexTile>();
					if(WorldManager.MOVEMODE && hexScript.isOccupied() && hexScript.occupant.transform.parent.tag != player.transform.parent.tag) {
						WorldManager.setAttack();
					}
					hexScript.deselect();

					/* CHECK IF IN MOVE MODE, IF DESTINATION IS LEGAL, AND IF PLAYER IS ALREADY ON TILE*/
					if(WorldManager.MOVEMODE && legalTiles.Contains(hexScript) && hexScript != player.currentTileScript) {

						if(!hexScript.isOccupied()) {					/* IF TILE VACANT, MOVE TO TILE */
							player.move(hit.collider.gameObject);
							hexScript.deselect();
							WorldManager.setNormal();
						}

						List<HexTile> attacks = legalAttacks(player);
							
						if (attacks.Count>0) {							/* HIGHLIGHT ATTACKABLE ENEMIES BEFORE/AFTER MOVE */
							WorldManager.setAttack();
							foreach(HexTile hex in attacks) {
								hex.highlightEnemy();
							}
						}
						else {
							WorldManager.setNormal();
							if(hexScript.isOccupied() && hexScript.occupant != player.gameObject) {		/* IF ATTACKING AVAILABLE, DECIDE BETWEEN */
								hexScript.highlight();													/* HIGHLIGHTING OCCUPANT */
							}
							else {
								player.endTurn();
							}
							player = null;
						}
					}
					else if(WorldManager.ATTACKMODE) {

						List<HexTile> attacks = legalAttacks(player);
						if(hexScript.isOccupied()) {
							if(hexScript.occupant.transform.parent.tag != player.transform.parent.tag && attacks.Contains(hexScript)) {
								hexScript.deselect();
								Player enemyScript = (Player)hexScript.occupant.GetComponent(hexScript.occupant.tag);
								Debug.Log ("Attacking enemy, health is: " + enemyScript.HP);
								player.attack(enemyScript);
								Debug.Log ("Attacking enemy, health is NOW: " + enemyScript.HP);
							}
						}

						WorldManager.setNormal();
						player.endTurn();
						player = null;
					}
					else {
						WorldManager.setNormal();
						player = null;
						//if you selected your own memeber..
						if(hexScript.isOccupied() && hexScript.occupant.transform.parent.tag == "BLUE") {

							WorldManager.setMove();
							player = (Player)hexScript.occupant.GetComponent(hexScript.occupant.tag);
							legalTiles = legalMoves (player);
							List<HexTile> attacks = legalAttacks(player);

							foreach(HexTile hex in tileList) {
								hex.greyOut();
							}
							foreach(HexTile hex in legalTiles) {
								hex.deselect();
							}
							foreach(HexTile hex in attacks) {
								hex.highlightEnemy();
							}
							if(lastBaseSelected!=null) {
								lastBaseSelected.SendMessage("deselect");
							}
						}

						hexScript.highlight();
					}
				}

				else if(hit.collider.tag == "Base") {
					Base script = hit.collider.gameObject.GetComponent<Base>();
					if(WorldManager.blueScript.bases.Contains(script)) {
						script.baseSelected();
						lastBaseSelected = script;
						if(player != null)
							player.isOn = false;				// makes the menu turn off
					}
					else {
						WorldManager.setNormal();
						script.highlight();
						List<HexTile> legalTiles = legalMoves (player);
						if(legalTiles.Contains(script)) {
							player.capture(script);
						}
					}
				}

				if(player!=null && WorldManager.NORMALMODE) {
					player.isOn = false;// makes the menu turn off
					if(hit.collider.tag != "Base") {
						if(lastBaseSelected!= null) {
							lastBaseSelected.SendMessage("deselect");
						}
					}
				}
			}
		}//if
	}//method
}//class
