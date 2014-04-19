using UnityEngine;
using System.Collections;

public class BadSoldier : Soldier {

	void OnEnable() {
		setup ();
		normalSprite = Resources.Load<Sprite>("Sprites/knight");
		gameObject.GetComponent<SpriteRenderer>().sprite = normalSprite;
	}
}
