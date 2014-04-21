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
	public Texture2D heavyPic;
	public Texture2D checkMark;
	public TeamManager TM;
	public bool hasBeenCaptured = false;
	private bool shouldShowStats = false;
	private bool menuOn = false;
	private IStats selectedStats = WorldManager.aerialStats;
	private string create;
	
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
		heavyPic = Resources.Load<Texture2D>("Textures/heavyTexture");
		checkMark = Resources.Load<Texture2D>("Textures/checkMark");
		greyTile = (GameObject)Instantiate(Resources.Load("Prefabs/greyTile"));
		greyTile.transform.parent = this.transform;
		greyTile.SetActive(false);
		turnOverTile = (GameObject)Instantiate(Resources.Load("Prefabs/blackoutTile"));
		turnOverTile.transform.parent = this.transform;
		turnOverTile.SetActive(false);
	}

	public void highlight() {
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		sr.sprite = highLightSprite;
	}
	
	public void deselect() {
		turnMenuOff();
	}

	public void baseSelected() {
		highlight();
		turnMenuOn();
	}
	
	public void setBase(int baseType) {
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		switch(baseType) {
			case 0:
				TM = GameObject.FindGameObjectWithTag("BLUE").GetComponent<TeamManager>();
				gameObject.transform.parent = TM.transform;
				TM.GetComponent<TeamManager>().bases.Add (this);
				normalSprite = blueBaseSprite;
				highLightSprite = blueBaseHighlightSprite;
				occupiedSprite = blueBaseSprite;
				greyOutSprite = blueBaseSprite;
				sr.sprite = normalSprite;
				side = "BLUE";
				break;
			case 1:
				TM = GameObject.FindGameObjectWithTag("RED").GetComponent<TeamManager>();
				gameObject.transform.parent = TM.transform;
				TM.GetComponent<TeamManager>().bases.Add (this);
				normalSprite = redBaseSprite;
				highLightSprite = redBaseSprite;
				occupiedSprite = redBaseSprite;
				greyOutSprite = redBaseSprite;
				sr.sprite = normalSprite;
				side = "RED";
				break;
			default:
				normalSprite = greyBaseSprite;
				highLightSprite = greyBaseSprite;
				occupiedSprite = greyBaseSprite;
				greyOutSprite = greyBaseSprite;
				sr.sprite = normalSprite;
				side = null;
				break;
		}
		setWidthAndHeight();
		WorldManager.numBases++;
	}


	void OnGUI() {
		if(menuOn) {
			Rect windowRect = new Rect(20, 20, 120, 150);
			windowRect = GUILayout.Window(0, windowRect, DoMyWindow, "Menu");
		}
	}


	void showStats(IStats stats) {
		string cost = "Cost : " + stats.getCost();
		GUILayout.Label(cost);
		string attackRange = "AttackRange : " + stats.getAttackRange();
		GUILayout.Label(attackRange);
		GUILayout.Label("Damage : " + stats.getDamage());
		GUILayout.Label("Defense : " + stats.getDefense());
		GUILayout.Label("Mobility : " + stats.getMobility());
		GUILayout.Label("Can Capture Bases : " + stats.getCanCapture());
		GUILayout.Label("YOUR CREDITS : " + TM.CREDITS);
	}
	
	void DoMyWindow(int windowID) {

		/*int ah = aerialPic.height;
		int aw = aerialPic.width;
		int sh = soldierPic.height;
		int sw = soldierPic.width;
		int space = ah + sh;//has spacing of thirty
		int width = 20 + aw + sw;*/

		GUILayout.BeginVertical();
		GUILayout.BeginHorizontal();

		if(GUILayout.Button(aerialPic)) {
			shouldShowStats = true;
			selectedStats = WorldManager.aerialStats;
			create = "Aerial";
		}
		else if(GUILayout.Button(soldierPic)) {
			shouldShowStats = true;
			selectedStats = WorldManager.soldierStats;
			create = "Soldier";
		}
		else if(GUILayout.Button(heavyPic)) {
			shouldShowStats = true;
			selectedStats = WorldManager.heavyStats;
			create = "Heavy";
		}
		GUILayout.EndHorizontal();
		if(shouldShowStats) {
			showStats(selectedStats);

			GUILayout.BeginHorizontal();
			if(GUILayout.Button(checkMark)) {
				createPlayerAndClose(create);
			}
			if(GUILayout.Button((Texture2D)Resources.Load("Textures/close"))) {
				menuOn = false;
			}
			GUILayout.EndHorizontal();
		}
		GUILayout.EndVertical();
	}


	public void createPlayerAndPositionOnBase(string prefabName) {
		GameObject player = WorldManager.instantiatePlayer(prefabName, side);
		WorldManager.positionPlayer(player, (HexTile)this);
		WorldManager.setNormal();
	}

	void createPlayerAndClose(string prefabName) {
		if(TM.creditsAreSufficient(prefabName)) {
			GameObject player = WorldManager.instantiatePlayer(prefabName, side);
			Player playerScript = (Player)player.GetComponent(player.tag);
			WorldManager.positionPlayer(player, (HexTile)this );
			menuOn = false;
			playerScript.endTurn();
		}
	}

	public void turnMenuOn() {
		menuOn = true;
	}

	public void turnMenuOff() {
		menuOn = false;
	}

	public void changeSides(string newside, TeamManager newtm) {
		//if now on red team..
		if(side == "RED") {
			TM.bases.Remove(this);
			changeSprite("BLUE");
			newtm.bases.Add(this);
			side = "BLUE";
		}
		//if now on blue team..
		else if(side == "BLUE") {
			TM.bases.Remove(this);
			changeSprite("RED");
			side = "RED";
			newtm.bases.Add(this);//add to team manager list
		}
		// assume now on nuetral team..
		else{
			newtm.bases.Add(this);
			side = newside;
			changeSprite(newside);
			}	
		gameObject.transform.parent = newtm.gameObject.transform;
		TM = newtm;
	}

	public void changeSprite(string color) {
		if(color.Equals("BLUE")) {
			gameObject.GetComponent<SpriteRenderer>().sprite = blueBaseSprite;
			normalSprite = blueBaseSprite;
			highLightSprite = blueBaseHighlightSprite;
			greyOutSprite = blueBaseSprite;
			occupiedSprite = blueBaseSprite;
		}
		else{
			gameObject.GetComponent<SpriteRenderer>().sprite = redBaseSprite;
			normalSprite = redBaseSprite;
			greyOutSprite = redBaseSprite;
			highLightSprite = redBaseSprite;
			occupiedSprite = blueBaseSprite;
		}
	}

	public void removeCaptor(List<Player> players) {
		if(hasBeenCaptured) {
			Player script = occupant.GetComponent<Player>();
			players.Remove(script);
			script.disableTurnOverTile();
			Destroy(occupant);
			occupant = null;
			hasBeenCaptured = false;
		}
	}
	

}