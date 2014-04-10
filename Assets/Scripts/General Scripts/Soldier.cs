using UnityEngine;
using System.Collections;

public class Soldier : Player {


	// Use this for initialization
	void Start () {
		setup();
		this.cost = SoldierStats.COST;
		this.attackRange = SoldierStats.ATTACKRANGE;
		this.MOB = SoldierStats.MOBILITY;
		this.DEF = SoldierStats.DEFENSE;
		this.canCapture = SoldierStats.CANCAPTURE;
		this.canAttackAfterMove = SoldierStats.CANATTACKAFTERMOVE;
		this.repair = SoldierStats.REPAIR;
		this.HP = SoldierStats.HEALTH;
		this.DMG = SoldierStats.DAMAGE; //taking ground light variable for now
		this.actionsList = new ArrayList();


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
