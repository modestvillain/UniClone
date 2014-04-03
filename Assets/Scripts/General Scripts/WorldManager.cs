using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {
	
	public Map map;
	public Player player;
	public Sprite playerSprite;
	bool playerSet = false;

	// Use this for initialization
	void Start () {
		this.map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();


		//player.SendMessage("crecreatePlayerInRandomLocation", map.tileList);
	}
	
	// Update is called once per frame
	void Update () {
			//map.Update();
		this.spawnPlayer();
			
	}

	void spawnPlayer(){
		if(!this.playerSet && this.map.tileList.Count > 0){
			this.player = new Player(this.map.tileList, playerSprite);
			playerSet = true;
		}
	}




}
