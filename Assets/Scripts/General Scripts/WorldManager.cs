using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {
	
	public Map map;
	public Player player;
	public Sprite playerSprite;

	// Use this for initialization
	void Start () {
		this.map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
		this.player = new Player(map.tileList, playerSprite);

		//player.SendMessage("crecreatePlayerInRandomLocation", map.tileList);
	}
	
	// Update is called once per frame
	void Update () {
			//map.Update();
	}




}
