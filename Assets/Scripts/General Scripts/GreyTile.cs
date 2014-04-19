using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GreyTile : HexTile {

	public void highlight() {
		transform.parent.GetComponent<HexTile>().highlight();
	}
}
