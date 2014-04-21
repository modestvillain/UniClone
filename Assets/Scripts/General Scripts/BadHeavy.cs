using UnityEngine;
using System.Collections;

public class BadHeavy : Player {
	
	void Start () {
		cost = HeavyStats.COST;
		attackRange = HeavyStats.ATTACKRANGE;
		MOB = HeavyStats.MOBILITY;
		DEF = HeavyStats.DEFENSE;
		canCapture = HeavyStats.CANCAPTURE;
		canAttackAfterMove =HeavyStats.CANATTACKAFTERMOVE;
		repair = HeavyStats.REPAIR;
		HP = HeavyStats.HEALTH;
		DMG = HeavyStats.DAMAGE;
	}
	
	void OnEnable() {
		setup ();
		normalSprite = Resources.Load<Sprite>("Sprites/ifrit");
		gameObject.GetComponent<SpriteRenderer>().sprite = normalSprite;
	}
}
