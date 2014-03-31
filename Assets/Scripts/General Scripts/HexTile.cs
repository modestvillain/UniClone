using UnityEngine;
using System.Collections;

public class HexTile:MonoBehaviour {

	public Vector2 location;
	public Sprite normalSprite;
	public Sprite highLightSprite;




	// Use this for initialization
	void Start () {

		Map.tileList.Add(this);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void highlight(){
		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		sr.sprite = highLightSprite;
	}

	void deselect(){
		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		sr.sprite = normalSprite;
	}
}
