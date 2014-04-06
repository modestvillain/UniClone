using UnityEngine;
using System.Collections;

public class Aerial : Player {

	// Use this for initialization
	void Start () {
		this.cost = 350;
		this.attackRange = 1;
		this.MOB = 3;
		this.DEF = 7;
		this.canCapture = false;
		this.canAttackAfterMove = true;
		this.repair = 2;
		this.HP = 10;
		this.DMG = 6; //taking ground light variable for now

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
