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

}