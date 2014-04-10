using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Base : HexTile {

	public Sprite blueBaseSprite;
	public Sprite blueBaseHighlightSprite;
	public Sprite redBaseSprite;
	public Sprite greyBaseSprite;
	public string side;
	public Texture2D aerialPic;
	public Texture2D soldierPic;
	public Texture2D checkMark;
	bool shouldShowStats = false;
	bool menuOn = false;
	IStats selectedStats = WorldManager.aerialStats;
	string create;
	public TeamManager team;
	
	void Start () {
		
	}

	void Update () {
		
	}

	void OnEnable() {
		blueBaseSprite = Resources.Load<Sprite>("Sprites/blueBase");
		blueBaseHighlightSprite = Resources.Load<Sprite>("Sprites/blueBaseHighLight");
		redBaseSprite = Resources.Load<Sprite>("Sprites/redbase");
		greyBaseSprite = Resources.Load<Sprite>("Sprites/greyBase");
		aerialPic = Resources.Load<Texture2D>("Textures/dragonTexture");
		soldierPic = Resources.Load<Texture2D>("Textures/soldierTexture");
		checkMark = Resources.Load<Texture2D>("Textures/checkMark");
	}

	public void highlight() {
		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		sr.sprite = this.highLightSprite;
	}
	
	public void deselect() {
		this.turnMenuOff();
	}

	public void baseSelected(){
		this.highlight();
		this.turnMenuOn();
	}
	
	public void setBase(int baseType) {
		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		switch(baseType) {
			case 0:
				team = GameObject.FindGameObjectWithTag("BLUE").GetComponent<TeamManager>();
				gameObject.transform.parent = team.transform;
				team.GetComponent<TeamManager>().bases.Add (this);
				normalSprite = blueBaseSprite;
				highLightSprite = blueBaseHighlightSprite;
				occupiedSprite = blueBaseSprite;
				greyOutSprite = blueBaseSprite;
				sr.sprite = normalSprite;
				this.side = "BLUE";
				break;
			case 1:
				gameObject.transform.parent = GameObject.FindGameObjectWithTag("RED").transform;
				normalSprite = redBaseSprite;
				highLightSprite = redBaseSprite;
				occupiedSprite = redBaseSprite;
				greyOutSprite = redBaseSprite;
				sr.sprite = normalSprite;
				this.side = "RED";
				break;
			default:
				normalSprite = greyBaseSprite;
				highLightSprite = greyBaseSprite;
				occupiedSprite = greyBaseSprite;
				greyOutSprite = greyBaseSprite;
				sr.sprite = normalSprite;
				this.side = null;
				break;
		}
		setWidthAndHeight();
	}


	void OnGUI(){
		if(menuOn){
			Rect windowRect = new Rect(20, 20, 120, 150);
			windowRect = GUILayout.Window(0, windowRect, DoMyWindow, "Menu");
		}
	}//method


	void showStats(IStats stats){
		string cost = "Cost : " + stats.getCost();
		GUILayout.Label(cost);
		string attackRange = "AttackRange : " + stats.getAttackRange();
		GUILayout.Label(attackRange);
		GUILayout.Label("Damage : " + stats.getDamage());
		GUILayout.Label("Defense : " + stats.getDefense());
		GUILayout.Label("Mobility : " + stats.getMobility());
		GUILayout.Label("Repair : " + stats.getRepair());
		GUILayout.Label("Can Capture Bases : " + stats.getCanCapture());
	}
	
	void DoMyWindow(int windowID) {

		/*int ah = this.aerialPic.height;
		int aw = this.aerialPic.width;
		int sh = this.soldierPic.height;
		int sw = this.soldierPic.width;
		int space = ah + sh;//has spacing of thirty
		int width = 20 + aw + sw;*/

		GUILayout.BeginVertical();
		GUILayout.BeginHorizontal();

		if(GUILayout.Button(this.aerialPic)){
			shouldShowStats = true;
			selectedStats = WorldManager.aerialStats;
			create = "Aerial";
		}
		else if(GUILayout.Button(this.soldierPic)){
			shouldShowStats = true;
			selectedStats = WorldManager.soldierStats;
			create = "Soldier";
		}
		GUILayout.EndHorizontal();

		if(shouldShowStats){
			this.showStats(selectedStats);

			GUILayout.BeginHorizontal();
			if(GUILayout.Button(this.checkMark)){
				this.createPlayerAndClose(create);
			}
			if(GUILayout.Button((Texture2D)Resources.Load("Textures/close"))){
				this.menuOn = false;
			}
			GUILayout.EndHorizontal();

		}
		GUILayout.EndVertical();
	}//method

	void createPlayerAndClose(string prefabName){
		GameObject player = WorldManager.instantiatePlayer(prefabName, this.side);
		WorldManager.positionPlayer(player, (HexTile)this );
		this.menuOn = false;
		WorldManager.getPlayerScript(player).endTurn();
	}

	public void turnMenuOn(){
		this.menuOn = true;
	}

	public void turnMenuOff(){
		this.menuOn = false;
	}

}//class
