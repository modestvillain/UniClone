using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DummyAI :MonoBehaviour {
	private TeamManager TM;
	private TeamManager BLUE;
	private string AERIAL = "BadAerial";
	private string SOLDIER = "BadSoldier";
	private string HEAVY = "BadHeavy";
	private string[] types = new string[3];
	private bool created = false;

	void OnEnable() {
		TM = GameObject.FindGameObjectWithTag("RED").GetComponent<TeamManager>();
		BLUE = GameObject.FindGameObjectWithTag("BLUE").GetComponent<TeamManager>();
		types[0] = AERIAL;
		types[1] = SOLDIER;
		types[2] = HEAVY;
	}

	public void startTurn() {
		WorldManager.map.deselectAll();
		TM.addCredits();
		created = false;
		if(TM.bases.Count>0 && BLUE.bases.Count>0) {
			if(UnityEngine.Random.Range(0,10)<6) {
				createNewPlayer(TM.bases[0],types[UnityEngine.Random.Range(0,3)]);
			}
			if(TM.team.Count > 0 & !created) {
				for(int i=0; i<TM.team.Count; i++) {
					List<HexTile> possibleMoves = WorldManager.map.legalMoves(TM.team[i]);
					HexTile opTile = null;
					int minDistance = 100;
					foreach(HexTile ht in possibleMoves) {
						int newMin = (int)Math.Sqrt((int)Math.Pow(ht.x-BLUE.bases[0].x,2) + (int)Math.Pow(ht.y-BLUE.bases[0].y,2));
						if(newMin < minDistance) {
							opTile = ht;
							minDistance = newMin;
						}
					}
					if(opTile != null) {
						if(opTile.gameObject.tag == "Base") {
							TM.team[i].capture((Base)opTile);
							if(BLUE.bases.Count==0) {
								break;
							}
						}
						else {
							TM.team[i].move(opTile.gameObject);
						}
					}
				}
			}
			else if(TM.team.Count > 0) {
				for(int i=0; i<TM.team.Count-1; i++) {
					List<HexTile> possibleMoves = WorldManager.map.legalMoves(TM.team[i]);
					HexTile opTile = null;
					int minDistance = 100;
					foreach(HexTile ht in possibleMoves) {
						int newMin = (int)Math.Sqrt((int)Math.Pow(ht.x-BLUE.bases[0].x,2) + (int)Math.Pow(ht.y-BLUE.bases[0].y,2));
						if(newMin < minDistance) {
							opTile = ht;
							minDistance = newMin;
						}
					}
					if(opTile != null)
						TM.team[i].move(opTile.gameObject);
				}
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
		if(TM.creditsAreSufficient(playerType) && !b.isOccupied()) {
			b.createPlayerAndPositionOnBase(playerType);
			created = true;
		}
	}
}
