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
	public string me;
	private bool created = false;

	void OnEnable() {
		TM = WorldManager.redScript;
		BLUE = WorldManager.blueScript;
		types[0] = AERIAL;
		types[1] = SOLDIER;
		types[2] = HEAVY;
	}

	public void startTurn() {
		WorldManager.map.deselectAll();
		TM.addCredits();
		created = false;
		if(TM.bases.Count>0 && BLUE.bases.Count>0) {
			if(UnityEngine.Random.Range(0,10)<9) {
				createNewPlayer(TM.bases[0],types[UnityEngine.Random.Range(0,3)]);
			}
			if(TM.team.Count > 0 & !created) {
				doActions (0);
			}
			else if(TM.team.Count > 0) {
				doActions (1);
			}
		}
		endAITurn();
	}

	public void doActions(int off) {

		State s = new State(WorldManager.map.tileList,WorldManager.blueScript,WorldManager.redScript);

		for(int i=0; i<s.red.team.Count-off; i++) {

			List<HexTile> possibleMoves = WorldManager.map.legalMoves(s.red.team[i]);
			HexTile opTile = null;
			double bestScore = -9999999;

			foreach(HexTile ht in possibleMoves) {
//				int newMin = (int)Math.Sqrt((int)Math.Pow(ht.x-BLUE.bases[0].x,2) + (int)Math.Pow(ht.y-BLUE.bases[0].y,2));
//				if(newMin < minDistance) {
//					opTile = ht;
//					minDistance = newMin;
//				}
				State newS = new State(s);
				newS.red.team[i].move(ht.gameObject);
				double score = newS.scoringFunction();

				if(score>bestScore) {
					bestScore = score;
					opTile = ht;
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
			List<HexTile> possibleAtts = WorldManager.map.legalAttacks(TM.team[i]);
			if(possibleAtts.Count>0) {
				GameObject p = possibleAtts[UnityEngine.Random.Range(0,possibleAtts.Count)].occupant;
				TM.team[i].attack((Player)p.GetComponent(p.tag));
			}
		}
	}

	private void endAITurn() {

		WorldManager.blueScript.removePlayersFromCapturedBases();
		WorldManager.redScript.removePlayersFromCapturedBases();
		WorldManager.beginPlayerTurn();
	}

	//creates player on top of base
	private void createNewPlayer(Base b, string playerType) {
		if(TM.creditsAreSufficient(playerType) && !b.isOccupied()) {
			b.createPlayerAndPositionOnBase(playerType);
			created = true;
		}
	}

	private class State {
		
		public List<HexTile> map;
		public TeamManager blue;
		public TeamManager red;
		
		public State(List<HexTile> oldmap, TeamManager oldblue, TeamManager oldred) {
			map = new List<HexTile>(oldmap);
			blue = copyTM(oldblue);
			red = copyTM(oldred);
		}
		
		public State(State s) {
			map = new List<HexTile>(s.map);
			blue = copyTM(s.blue);
			red = copyTM(s.red);
		}
		
		public TeamManager copyTM(TeamManager tm) {
			TeamManager TM = new TeamManager(tm.parent);
			TM.team = new List<Player>(tm.team);
			TM.CREDITS = tm.CREDITS;
			TM.bases = new List<Base>(tm.bases);
			return TM;
		}
		
		public double scoringFunction(){
			double score = 0;
			//total health of all players should be greater than the total health of theirs-- your total health minus theirs
			score += red.totalHealth() - blue.totalHealth();
			//money is good in the begining-- want enough money to buy something substantial, for now just have money
			score += red.CREDITS;
			// want a player that has high enough damage to take out the player's on their map (so want stronger players)-- your total damage minus theirs
			score += red.totalDamage() - blue.totalDamage();
			// better if total strength is better than theirs-- your total strength minus theirs, offense
			score += red.totalDefense() - blue.totalDefense();
			// better if you have more bases than they do -- your bases minus their bases
			score += red.bases.Count - blue.bases.Count;
			
			//other things
			//the proximity of individual troops
			return score;
		}//method
		
		
	}//state class
}
