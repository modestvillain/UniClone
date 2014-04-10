using UnityEngine;
using System.Collections;

public class Aerial : Player {


	// Use this for initialization
	void Start () {
		this.cost = AerialStats.COST;
		this.attackRange = AerialStats.ATTACKRANGE;
		this.MOB = AerialStats.MOBILITY;
		this.DEF = AerialStats.DEFENSE;
		this.canCapture = AerialStats.CANCAPTURE;
		this.canAttackAfterMove =AerialStats.CANATTACKAFTERMOVE;
		this.repair = AerialStats.REPAIR;
		this.HP = AerialStats.HEALTH;
		this.DMG = AerialStats.DAMAGE; //taking ground light variable for now
	}

	void OnEnable() {
		setup();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
