using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StillEnemyBase : Rule {
	DummyAI dai;
	//AI dai;
	RuleBasedSystem sys;

	public StillEnemyBase(DummyAI dai, RuleBasedSystem sys){
	//public StillEnemyBase(AI dai, RuleBasedSystem sys){
		this.dai = dai;
		this.sys = sys;

		//DataGroupMatch dgm = new DataGroupMatch();
		//dgm.identifier = RuleBasedSystem.RED_PLAYERS;
		//dgm.children.Add(new Not(RuleBasedSystem.alreadyASolider));

		//create match for solider already there

		Match comp = new And(new Not(RuleBasedSystem.alreadyASolider), RuleBasedSystem.blueHasBases);
		//dgm.children.Add(comp);
		this.ifClause = comp;
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
		//Base closest = WorldManager.blueScript.bases[0].closestEnemyBase(dai.AI_TM);
		dai.createNewPlayer(closest, "BadSoldier");
	}
}
