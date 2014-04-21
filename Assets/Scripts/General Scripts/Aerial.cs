using UnityEngine;
using System.Collections;

public class Aerial : Player {
	
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
		setup ();
//		if(this.tag == "Aerial") {
			normalSprite = Resources.Load<Sprite>("Sprites/dragon");
//			normalSprite = Resources.Load<Sprite>("Sprites/Cute Dragon");
//		}
//		else{
//			normalSprite = Resources.Load<Sprite>("Sprites/badAerial");
//		}
		gameObject.GetComponent<SpriteRenderer>().sprite = normalSprite;
	}
}
