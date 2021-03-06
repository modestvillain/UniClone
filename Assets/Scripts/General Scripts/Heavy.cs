﻿using UnityEngine;
using System.Collections;

public class Heavy : Player {

	void Start () {
		cost = HeavyStats.COST;
		attackRange = HeavyStats.ATTACKRANGE;
		MOB = HeavyStats.MOBILITY;
		DEF = HeavyStats.DEFENSE;
		canCapture = HeavyStats.CANCAPTURE;
		canAttackAfterMove =HeavyStats.CANATTACKAFTERMOVE;
		repair = HeavyStats.REPAIR;
		HP = HeavyStats.HEALTH;
		DMG = HeavyStats.DAMAGE; //taking ground light variable for now
	}
	
	void OnEnable() {
		setup ();
		normalSprite = Resources.Load<Sprite>("Sprites/Heavy");
		gameObject.GetComponent<SpriteRenderer>().sprite = normalSprite;
	}
}
