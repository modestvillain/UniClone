using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamManager : MonoBehaviour {

	public int CREDITS;
	public List<Player> team;
	public List<Base> bases;

	void Start() {
		team = new List<Player>();
	}

	void Update(){
		if(bases.Count == WorldManager.numBases){
			WorldManager.WINSTATE = true;
			WorldManager.WINNER = gameObject.tag;
		}
	}

	public bool inTeam(Player player) {
		foreach(Player p in team) {
			if(player==p)	return true;
		}
		return false;
	}

	public void removePlayersFromCapturedBases(){
			foreach(Base b in bases){
				b.removeCaptor(team);
			}
	}
}