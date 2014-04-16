using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DummyAI :MonoBehaviour {
	TeamManager TM;
	private string AERIAL = "badAerial";
	// Use this for initialization
	void Start () {
		//this.TM = GameObject.FindGameObjectWithTag("RED").GetComponent<TeamManager>();
	}

	void OnEnable(){
		this.TM = GameObject.FindGameObjectWithTag("RED").GetComponent<TeamManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startTurn(){
		foreach(Player p in TM.team){
			List<HexTile> possibleMoves = WorldManager.map.legalMoves(p);
			int rand = Random.Range(0, possibleMoves.Count);
			p.move(possibleMoves[rand].gameObject);
		}
		this.endAITurn();

		this.createNewPlayer(TM.bases[0], this.AERIAL);
	}

	private void endAITurn(){
		WorldManager.PLAYERMODE = true;
		GameObject.FindGameObjectWithTag("BLUE").GetComponent<TeamManager>().removePlayersFromCapturedBases();
		GameObject.FindGameObjectWithTag("RED").GetComponent<TeamManager>().removePlayersFromCapturedBases();
		WorldManager.beginPlayerTurn();
	}

	//creates player on top of base
	private void createNewPlayer(Base b, string playerType){
		b.createPlayerAndPositionOnBase(playerType);
	}
	

}//class
