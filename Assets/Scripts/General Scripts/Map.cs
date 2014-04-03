using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Map : MonoBehaviour {

	public List<HexTile> tileList = new List<HexTile>();
	public GameObject whiteHexTile;


	void Start(){
		this.createMap();

	}

	public void Update(){
		if(this.tileList.Count> 0){
			this.selectTile();
		}


	}

	public void createHexTile(float i, float j, int upDown, float widthAway){
		GameObject newT = (GameObject) Instantiate(whiteHexTile);
		HexTile hextile = newT.GetComponent<HexTile>();
		this.tileList.Add(hextile);

		hextile.map = this;
		Vector2 loc = new Vector2(i,j);
		Vector2 pos = new Vector2();
		switch(upDown){
		case 0:
			pos.x = loc.x + widthAway;
			pos.y = loc.y;
			break;
		case 1:
			pos.x = loc.x + widthAway -.3f;
			pos.y = loc.y +.3f;
			break;
		case -1:
			pos.x = loc.x + widthAway- .7f;
			pos.y = loc.y - .3f;
			break;
		default:
			pos.x = loc.x + widthAway ;
			pos.y = loc.y;
			break;
		}
		hextile.position = pos;
		newT.transform.position = pos;
		hextile.location = loc;
		hextile.center = new Vector2(pos.x + .1f, pos.y + .1f);
		//hextile.setWidthAndHeight();
	}

	public void createMap(){


		float widthAway = 0;
		float start = -10;

		//for(float j = 1.255f; j< 3; j+= 1.255f){
		float j = 0;
			for(float i = start; i < 10; i++){
				this.createHexTile(i+ 1, j + 1, 1, widthAway);
				this.createHexTile(i, j, 0, widthAway);
			    this.createHexTile(i, j - 1, -1, widthAway);

				widthAway+=.5f;
				
			}//for
		//start+= widthAway/2;
		//}//for



		
	}//method

	public void selectTile(){
		if (Input.GetMouseButtonDown(0))
			
		{
			
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			
			
			
			if(hit)
				
			{
				
				Debug.Log("object clicked: "+hit.collider.tag);
				if(hit.collider.tag == "hexTile"){
					//highlight it
					hit.collider.gameObject.SendMessage("highlight");

					foreach(HexTile tile in tileList){
						if(tile.gameObject != hit.collider.gameObject){
							//change to normal
							tile.deselect();
						
						}
					}//foreach
				}//if
				
			}//if hit
			else{
				Debug.Log ("Not hit");
			}
		}//if

		}//method
}//class
