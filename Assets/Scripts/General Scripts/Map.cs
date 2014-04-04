using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Map : MonoBehaviour {

	public GameObject[,] tiles;
	public GameObject whiteHexTile;
	public WorldManager worldManager;
	public int modX=0;
	public int mapWidth;
	public int mapHeight;

	void Start() {
		mapWidth = 8;
		mapHeight = 8;
		int realWidth = mapWidth + (int)((mapHeight - 1) / 2);
		tiles = new GameObject[realWidth,mapHeight];
		this.createMap();
		this.worldManager = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>();
	}

	public void Update() {
		if(this.tileList.Count> 0){
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
		tiles[hextile.x,hextile.y]=newT;
	}

	public void createMap() {
		float widthAway = .5f;
		int off=-1;										/* FOR INITIALIZING FIRST/BOTTOM ROW */
		int count=0;

		for(int y = 0; y < mapHeight; y++){
			for(int x = 0; x < mapWidth; x++) {
				this.createHexTile(x, y, off, widthAway);
			}
			modX=count;
			if(off==1) {off=0; modX=++count;}
			else if(off==0) off=1;
			else off=1;
		}	
	}

	public void selectTile(){
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			
			if(hit)	{
				Debug.Log("object clicked: "+hit.collider.tag);
				if(hit.collider.tag == "hexTile"){
					HexTile hexScript = hit.collider.gameObject.GetComponent<HexTile>();
					//highlight it
					hexScript.highlight();
					//see if player is in it
					Debug.Log ("Player in it: " +  hexScript.isOccupied());
					//if occupied, turn into move mode
					if(this.worldManager.mode == WorldManager.MOVEMODE){
						//then move the player to that tile
						Player script = (Player)worldManager.player.GetComponent("Player");
						script.move(hit.collider.gameObject);
						worldManager.mode = WorldManager.NORMALMODE;
					}
					else{
						if(hexScript.isOccupied()){
							this.worldManager.mode = WorldManager.MOVEMODE;
						}
					}


					foreach(HexTile tile in tileList){
						if(tile.gameObject != hit.collider.gameObject){
							//change to normal
							tile.deselect();
						
						}
					}//foreach
				}//if
				
			}//if hit
			else{
				//Debug.Log ("Not hit");
			}
		}//if

		}//method
}//class
