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

	public bool inTeam(Player player) {
		foreach(Player p in team) {
			if(player==p)	return true;
		}
		return false;
	}

}