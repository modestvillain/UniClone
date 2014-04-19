using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DummyAI :MonoBehaviour {
	TeamManager TM;
	private string AERIAL = "BadAerial";
	private string SOLDIER = "BadSoldier";

	void OnEnable() {
		TM = GameObject.FindGameObjectWithTag("RED").GetComponent<TeamManager>();
	}

	public void startTurn() {
		WorldManager.map.deselectAll();
		TM.addCredits();
		if(TM.bases.Count>0) {
			createNewPlayer(TM.bases[0],AERIAL);
			foreach(Player p in TM.team) {
				List<HexTile> possibleMoves = WorldManager.map.legalMoves(p);
				int rand = Random.Range(0, possibleMoves.Count);
				p.move(possibleMoves[rand].gameObject);
			}
		}
		endAITurn();
	}

	private void endAITurn() {
		GameObject.FindGameObjectWithTag("BLUE").GetComponent<TeamManager>().removePlayersFromCapturedBases();
		GameObject.FindGameObjectWithTag("RED").GetComponent<TeamManager>().removePlayersFromCapturedBases();
		WorldManager.beginPlayerTurn();
	}

	//creates player on top of base
	private void createNewPlayer(Base b, string playerType) {
		if(TM.creditsAreSufficient(playerType)) {
			b.createPlayerAndPositionOnBase(playerType);
		}
	}
}
