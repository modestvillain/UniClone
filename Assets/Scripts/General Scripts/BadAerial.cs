using UnityEngine;
using System.Collections;

public class BadAerial : Aerial {

	void OnEnable() {
		setup ();
		normalSprite = Resources.Load<Sprite>("Sprites/badAerial");
		gameObject.GetComponent<SpriteRenderer>().sprite = normalSprite;
	}
}