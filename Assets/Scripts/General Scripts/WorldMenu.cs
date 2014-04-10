using UnityEngine;
using System.Collections;

public class WorldMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void makeMenu(){

		if(GUILayout.Button ("EndTurn")){
			WorldManager.PLAYERMODE = false;
		}

		if(GUILayout.Button ("BeginTurn")){
			WorldManager.PLAYERMODE = true;
			gameObject.SendMessage("removeTurnOverTiles");
		}
	}
}
