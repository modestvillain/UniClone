using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DummyAI :MonoBehaviour {
	public TeamManager TM;
	public TeamManager BLUE;
	public string AERIAL = "BadAerial";
	public string SOLDIER = "BadSoldier";
	public string HEAVY = "BadHeavy";
	public string[] types = new string[3];
	public string me;
	public bool created = false;
	public static bool createdModified;

	void Start() {
		TM = WorldManager.redScript;
		BLUE = WorldManager.blueScript;
		types[0] = AERIAL;
		types[1] = SOLDIER;
		types[2] = HEAVY;

		DummyAI.createdModified = false;
	}

	public void startTurn() {

		//==========================Test Database stuff==========================
		RuleBasedSystem sys = new RuleBasedSystem();
		sys.addPlayersOnThisTeamToDataBase(this);

		new SoldierCanGetToNuetralBase(sys, this);
		new StillEnemyBase(this, sys);
		//rules are in order of highest priority
		//sys.ruleBasedIteration();
		Debug.Log ("In this database:" + sys.printDatabase());

		//============================end db===================================
		WorldManager.map.deselectAll();
		TM.addCredits();
		created = false;
		if(TM.bases.Count>0 && BLUE.bases.Count>0) {
			if(UnityEngine.Random.Range(0,10)<9) {
				//createNewPlayer(TM.bases[0],types[UnityEngine.Random.Range(0,3)]);
				sys.ruleBasedIteration();
			
			}
			if(TM.team.Count > 0 & !created) {
				doActions (0);
			}
			else if(TM.team.Count > 0) {
				doActions (1);
			}
		}
		WorldManager.endAITurn();
	}

	public void doActions(int off) {


		for(int i=0; i<TM.team.Count-off; i++) {

			List<HexTile> possibleMoves = WorldManager.map.legalMoves(TM.team[i]);
			HexTile opTile = null;
			double bestScore = -9999999;
			double weightForCloseToBases = .60;//if you can't capture bases
			double weightForCloseToEnemy = .70;//if you can capture bases
			double weightForNumBases = 2;//if you can capture bases

			foreach(HexTile ht in possibleMoves) {
//				int newMin = (int)Math.Sqrt((int)Math.Pow(ht.x-BLUE.bases[0].x,2) + (int)Math.Pow(ht.y-BLUE.bases[0].y,2));
//				if(newMin < minDistance) {
//					opTile = ht;
//					minDistance = newMin;
//				}
				State newS = new State(WorldManager.map.tileList, BLUE, TM);
				newS.red.team[i].move(ht.gameObject);
				double score = newS.scoringFunction(weightForCloseToBases, weightForCloseToEnemy, weightForNumBases);
				//Debug.Log("score: " + score);
				if(score>bestScore) {
					bestScore = score;
					opTile = ht;
				}
			}
			//Debug.Log ("best score" + bestScore);
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

	public void endAITurn() {
		TM.removePlayersFromCapturedBases();
		BLUE.removePlayersFromCapturedBases();
		WorldManager.beginPlayerTurn();
	}

	//creates player on top of base
<<<<<<< HEAD
	public void createNewPlayer(Base b, string playerType) {
		if(TM.creditsAreSufficient(playerType) && !b.isOccupied()) {
=======
	private void createNewPlayer(Base b, string playerType) {
		if(TM.creditsAreSufficient(playerType) && !b.isOccupied() && TM.team.Count < 5) {
>>>>>>> 83f9442333ef496bc7056bd4df45c568ccd27b61
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
		
		public double scoringFunction(double weightForCloseToBases, double weightForCloseToEnemy, double weightForNumBases){
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
			score += 2*(red.bases.Count - blue.bases.Count);

			// how close troop is to base how close they are minus how close you are to bases
			score+= blue.totalMinDistanceToDesiredBases(weightForCloseToBases) - red.totalMinDistanceToDesiredBases(weightForCloseToBases);
			
			//make weight of close to other enemy players more for those who can't capture bases
			score -=  red.totalMinDistanceToEnemy(weightForCloseToEnemy);
			return score;


		}//method
		
		
	}//state class



}
