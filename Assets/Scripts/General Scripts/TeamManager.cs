using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamManager : MonoBehaviour {

	public int CREDITS;
	public List<Player> team;
	public List<Base> bases;

	void Start() {
		team = new List<Player>();
		CREDITS = 500;
	}

	void Update() {
		if(bases.Count == WorldManager.numBases) {
			WorldManager.WINSTATE = true;
			WorldManager.WINNER = gameObject.tag;
		}
	}

	public bool inTeam(Player player) {
		return team.Contains(player);
	}

	public void removePlayersFromCapturedBases() {
		foreach(Base b in bases) {
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