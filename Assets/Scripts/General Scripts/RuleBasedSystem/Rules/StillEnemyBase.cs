using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StillEnemyBase : Rule {
	DummyAI dai;
	RuleBasedSystem sys;

	public StillEnemyBase(DummyAI dai, RuleBasedSystem sys){
		this.dai = dai;
		this.sys = sys;
		this.ifClause = RuleBasedSystem.blueHasBases;
		sys.addInfo(new Datum(RuleBasedSystem.STILLENEMYBASE, WorldManager.blueScript.bases.Count));
		sys.addRule(this);
	}

	public override void action (List<Binding> bindings)
	{
		//if there is still an enemy base left, spawn another solider
		Debug.Log("StillEnemyBase Rule fired");
		//spawn soldier from the closest base
		//find closest base red base
		//takes in team manager
		//so find closest red base from the blue's team manager
		Base closest = WorldManager.blueScript.bases[0].closestEnemyBase(dai.TM);
		dai.createNewPlayer(closest, "BadSoldier");
	}
}
