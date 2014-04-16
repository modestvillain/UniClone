﻿using UnityEngine;
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
		return team.Contains(player);
	}

	public void removePlayersFromCapturedBases(){
			foreach(Base b in bases){
				b.removeCaptor(team);
			}
	}

	public void removeFromTeam(Player p){
		team.Remove(p);
	}
}