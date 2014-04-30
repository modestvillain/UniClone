using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnRandom : Rule {

	DummyAI dai;
	//AI dai;
	RuleBasedSystem sys;

	public  SpawnRandom(DummyAI dai, RuleBasedSystem sys){
		//public StillEnemyBase(AI dai, RuleBasedSystem sys){
		this.dai = dai;
		this.sys = sys;


		this.ifClause = RuleBasedSystem.redHasBases;
		sys.addRule(this);
	}
	
	public override void action (List<Binding> bindings)
	{
		//if there is still an enemy base left, spawn another solider
		Debug.Log("SpawnRandom Rule fired");
		 string AERIAL = "BadAerial";
		string SOLDIER = "BadSoldier";
		 string HEAVY = "BadHeavy";
		 string[] types = new string[3];
		types[0] = AERIAL;
		types[1] = SOLDIER;
		types[2] = HEAVY;
		
		Base closest = WorldManager.blueScript.bases[0].closestEnemyBase(dai.TM);
		//Base closest = WorldManager.blueScript.bases[0].closestEnemyBase(dai.AI_TM);
		dai.createNewPlayer(closest, types[UnityEngine.Random.Range(0,3)]);
	}

}
