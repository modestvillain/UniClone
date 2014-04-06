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

	public void createHexTile(float x, float y, int upDown, float widthAway) {

		GameObject newT = (GameObject) Instantiate(whiteHexTile);
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

	public void createMap() {

		empty = false;
		float widthAway = .5f;
		int off=-1;										/* FOR INITIALIZING FIRST/BOTTOM ROW */
		int count=0;

		for(int y = 0; y < mapHeight; y++) {
			for(int x = 0; x < mapWidth; x++) {
				this.createHexTile(x, y, off, widthAway);
			}
			modX=count;
			if(off==1) {off=0; modX=++count;}
			else if(off==0) off=1;
			else off=1;
		}

		createAdjacencies();
	}

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

	public void setPlayerScript(GameObject g) {
		if(g.tag == "Player")
			player = (Player)g.GetComponent("Player");
		else if(g.tag == "Soldier")
			player = (Player)g.GetComponent("Soldier");

	}

	public void selectTile() {

		if (Input.GetMouseButtonDown(0)) {
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			
			if(hit)	{
				if(hit.collider.tag == "hexTile") {
					foreach(HexTile tile in tileList) {
						if(tile.gameObject != hit.collider.gameObject) {
							//change to normal
							tile.deselect();
						}
					}
//					Player player = (Player)worldManager.player.GetComponent("Player");
					List<HexTile> legalTiles = legalMoves (player);
					HexTile hexScript = hit.collider.gameObject.GetComponent<HexTile>();
					hexScript.highlight();

					/* CHECK IF IN MOVE MODE, IF DESTINATION IS LEGAL, AND IF PLAYER IS ALREADY ON TILE*/
					if(this.worldManager.mode == WorldManager.MOVEMODE && legalTiles.Contains(hexScript) && player.currentTileScript!=hexScript
					   && !hexScript.isOccupied()) {
						//move occupant to that tile
						player.move(hit.collider.gameObject);//SendMessage("move", hit.collider.gameObject);
						worldManager.mode = WorldManager.NORMALMODE;
					}

					else {
						worldManager.mode = WorldManager.NORMALMODE;
						if(hexScript.isOccupied() && hexScript.occupant.tag == "Player") {
							worldManager.mode = WorldManager.MOVEMODE;
							setPlayerScript(hexScript.occupant);
							legalTiles = legalMoves (player);
							foreach(HexTile hex in legalTiles) {
								hex.highlight();
							}
						}
					}
				}
			}
		}//if
	}//method
}//class
