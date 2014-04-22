using UnityEngine;
using System.Collections;

public class Soldier : Player {
	
	void Start () {
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

	void OnEnable() {
		setup();
		normalSprite = Resources.Load<Sprite>("Sprites/soldier");
//		normalSprite = Resources.Load<Sprite>("Sprites/knight");
		gameObject.GetComponent<SpriteRenderer>().sprite = normalSprite;
	}
}
