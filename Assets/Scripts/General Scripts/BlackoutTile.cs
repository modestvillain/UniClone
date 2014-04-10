using UnityEngine;
using System.Collections;

public class BlackoutTile : MonoBehaviour {

	// Use this for initialization
	void OnEnable() {
		gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/blackoutTile");
	}
}
