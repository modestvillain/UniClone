using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wait : MonoBehaviour {

	public IEnumerator waitASec() {
		yield return new WaitForSeconds (5);
	}
}