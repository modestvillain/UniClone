using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamManager {

	public GameObject parent;
	public int CREDITS = 500;
	public List<Player> team = new List<Player>();
	public List<Base> bases = new List<Base>();

	public TeamManager(){

	}
	public TeamManager(GameObject parent) {
		this.parent = parent;
	}

//	void Start() {
//		team = new List<Player>();
//		CREDITS = 500;
//	}

//	void Update() {
//		if(WorldManager.redWon()) {
//			WorldManager.WINSTATE = true;
//			WorldManager.WINNER = "RED";
//		}
//		if(WorldManager.blueWon()) {
//			WorldManager.WINSTATE = true;
//			WorldManager.WINNER = "BLUE";
//		}
//	}

	public bool inTeam(Player player) {
		return team.Contains(player);
	}

	public void removePlayersFromCapturedBases() {
		foreach(Base b in bases) {
			if(b.isOccupied())
				b.removeCaptor(team);
		}
	}

	public void removeFromTeam(Player p) {
		team.Remove(p);
	}

	public void addCredits() {
		foreach(Base b in bases) {
			CREDITS += 100;
		}
	}

	public double totalHealth(){
		double total = 0;
		foreach(Player p in this.team){
			total += p.HP;
		}
		return total;
	}//method

	public double totalDamage(){
		double total = 0;
		foreach(Player p in this.team){
			total += p.DMG;
		}
		return total;
	}

	public double totalDefense(){
		double total = 0;
		foreach(Player p in this.team){
			total += p.DEF;
		}
		return total;
	}

	public double totalMinDistanceToDesiredBases(double weight){
		double total = 0;
		foreach(Player p in this.team){
			Base closest = p.closestEnemeyOrNuetralBase();
			if(closest!=null){
				int dist = closest.distanceFromBase(p.currentTileScript);
				if(p.canCapture){
					total += dist * weight;
				}
				else{
					total += dist;
					//non-capture distance to nuetral base
				}
			}

		}

		return total;
	}



	public double totalMinDistanceToEnemy(double weight){
		double total = 0;
		foreach(Player p in this.team){
			Player closest = p.closestEnemyPlayer();
			if(closest!=null){
				int dist = closest.distanceToAnotherPlayer(p);
				if(p.canCapture){
					total += 1/dist;
				}
				else{
					total += dist;
				}
			}
		}
		return 2*total;
	}


	public bool creditsAreSufficient(string name) {
		if((name=="Soldier" || name=="BadSoldier") && CREDITS >= SoldierStats.COST) {
			CREDITS -= SoldierStats.COST;
			return true;
		}

		else if((name=="Aerial" || name=="BadAerial") && CREDITS >= AerialStats.COST) {
			CREDITS -= AerialStats.COST;
			return true;
		}
		else if((name=="Heavy" || name=="BadHeavy") && CREDITS >= HeavyStats.COST) {
			CREDITS -= AerialStats.COST;
			return true;
		}
		return false;
	}


}