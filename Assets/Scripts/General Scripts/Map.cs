using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public static  class Map {
	public static List<HexTile> tileList = new List<HexTile>();




	public static void selectTile(){
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
							tile.SendMessage("deselect");
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
