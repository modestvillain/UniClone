using UnityEngine;
using System.Collections;

public class Soldier : Player {

	// Use this for initialization
	void Start () {
		this.cost = 100;
		this.attackRange = 1;
		this.MOB = 11;
		this.DEF = 5;
		this.canCapture = true;
		this.canAttackAfterMove = true;
		this.repair = 1;
		this.HP = 10;
		this.DMG = 6; //taking ground light variable for now

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
