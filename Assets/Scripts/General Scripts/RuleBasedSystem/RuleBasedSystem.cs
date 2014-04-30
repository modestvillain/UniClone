using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RuleBasedSystem  {
	Database database;
	List<Rule> rules;

	public static Identifier HEALTH ;
	public static Identifier WEALTH;
	public static Identifier NUMNEUTRALBASES;
	public static Identifier RED__BASE_COUNT;
	public static Identifier SOLDIER_CAN_GET_TO_NEUTRALBASE;
	public static Identifier BADSOLDIER;
	public static Identifier BADAERIAL;
	public static Identifier BADHEAVY;
	public static Identifier SOLDIER;
	public static Identifier AERIAL;
	public static Identifier HEAVY;
	public static Identifier STILLENEMYBASE;

	//some predefined matches
	public static Match redHasBases; 
	public static Match isANuetralBase; 
	public static Match alreadyASolider; 
	public static Match blueHasBases;

	public RuleBasedSystem(){
		RuleBasedSystem.HEALTH = new Identifier( true, "HEALTH");
		RuleBasedSystem.WEALTH = new Identifier( true, "WEALTH");
		RuleBasedSystem.NUMNEUTRALBASES = new Identifier( true, "NUMNEUTRALBASES");
		RuleBasedSystem.RED__BASE_COUNT = new Identifier( true, "RED__BASE_COUNT");
		RuleBasedSystem.SOLDIER_CAN_GET_TO_NEUTRALBASE = new Identifier( true, "SOLDIER_CAN_GET_TO_NEUTRALBASE");
		RuleBasedSystem.BADSOLDIER = new Identifier(true, "BADSOLDIER");
		RuleBasedSystem.BADAERIAL = new Identifier(true, "BADAERIAL");
		RuleBasedSystem.BADHEAVY = new Identifier(true, "BADHEAVY");
		RuleBasedSystem.SOLDIER = new Identifier(true, "SOLDIER");
		RuleBasedSystem.AERIAL = new Identifier(true, "AERIAL");
		RuleBasedSystem.HEAVY = new Identifier(true, "HEAVY");
		RuleBasedSystem.STILLENEMYBASE = new Identifier(true, "STILLENEMYBASE");
		//RuleBasedSystem.SPAWNRANDOM = new Identifier(true, "SPAWNRANDOM");

		//predfine some matches
		RuleBasedSystem.redHasBases = new DatumMatch(RuleBasedSystem.RED__BASE_COUNT, 1, 5);// red has at least one base
		RuleBasedSystem.blueHasBases = new DatumMatch(RuleBasedSystem.STILLENEMYBASE, 1, 5);// blue has at least one base
		RuleBasedSystem.isANuetralBase = new DatumMatch(RuleBasedSystem.NUMNEUTRALBASES,1, 4);//nuetral has at least one base
		RuleBasedSystem.alreadyASolider = new Match(RuleBasedSystem.BADSOLDIER);

		this.database = new Database();
		this.rules = new List<Rule>();
	}

	public void ruleBasedIteration(){
		//check each rule in turn
		foreach(Rule r in rules){
			//create the empty set of bindings
			List<Binding> bindings = new List<Binding>();

			//check for triggering
			if(r.ifClause.matches(database, bindings)){
				Debug.Log ("there is a match");
				//fire the rule
				r.action(bindings);
				//exit
				return;
			}
		}//for each
		//if we get here, no match, so create fall back action or do nothing
	}//method

	public void addRule(Match match){
		//order you add rules determines priority!!
		this.rules.Add(new Rule(match));
	}
	public void addRule(Rule rule){
		//order you add rules determines priority!!
		this.rules.Add(rule);
	}

	public void addInfo(DataNode dn){
		this.database.add(dn);
	}

	public string printDatabase(){
		return this.database.print();
	}

	public void addPlayersOnThisTeamToDataBase(DummyAI ai){
	//public void addPlayersOnThisTeamToDataBase(AI ai){
		//create relavant info for database
		//go through players on red team, ask what type they are
		//DataGroup
		//Identifier is redTEam, with nested with the players
		Identifier redPlayers = new Identifier(false, "RED_PLAYERS");
		DataGroup dg = new DataGroup();
		dg.identifier = redPlayers;



		foreach(Player p in ai.TM.team){
	//	foreach(Player p in ai.AI_TM.team){
			Debug.Log ("Player added to database");
			if(p is BadSoldier){
				dg.addToChildren(new DataNode(RuleBasedSystem.BADSOLDIER));
			}
			else if(p is BadAerial){
				dg.addToChildren(new DataNode(RuleBasedSystem.BADAERIAL));
			}
			else{
					dg.addToChildren(new DataNode(RuleBasedSystem.BADHEAVY));
			}
		}
		this.addInfo(dg);
	}//method

	public void addPlayersOnEnemyTeamToDataBase(){
		//public void addPlayersOnThisTeamToDataBase(AI ai){
		//create relavant info for database
		//go through players on red team, ask what type they are
		//DataGroup
		//Identifier is redTEam, with nested with the players
		Identifier bluePlayers = new Identifier(false, "BLUE_PLAYERS");
		DataGroup dg = new DataGroup();
		dg.identifier = bluePlayers;
		
		
		
		foreach(Player p in WorldManager.blueScript.team){
			//	foreach(Player p in ai.AI_TM.team){
			Debug.Log ("Player added to database");
			if(p is Soldier){
				dg.addToChildren(new DataNode(RuleBasedSystem.SOLDIER));
			}
			else if(p is Aerial){
				dg.addToChildren(new DataNode(RuleBasedSystem.AERIAL));
			}
			else{
				dg.addToChildren(new DataNode(RuleBasedSystem.HEAVY));
			}
		}
		this.addInfo(dg);
	}//method
}//class

