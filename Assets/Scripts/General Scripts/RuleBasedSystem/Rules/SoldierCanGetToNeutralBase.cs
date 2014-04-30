using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoldierCanGetToNuetralBase : Rule{
	DummyAI ai;
	//AI ai;
	RuleBasedSystem sys;


	public SoldierCanGetToNuetralBase(RuleBasedSystem sys, DummyAI ai){
	//public SoldierCanGetToNuetralBase(RuleBasedSystem sys, AI ai){
		this.sys = sys;
		//need to add and you didn't already spawn a solider

		//create relavant info for database
		sys.addInfo(new Datum(RuleBasedSystem.NUMNEUTRALBASES, WorldManager.nuetralScript.bases.Count));//how many nuetral bases there are
		sys.addInfo(new Datum(RuleBasedSystem.RED__BASE_COUNT, WorldManager.redScript.bases.Count));// how many bases red has

		if(WorldManager.nuetralScript.bases.Count > 0){
			bool contains = WorldManager.map.legalMovesModified(WorldManager.redScript.bases[0], 4, "Soldier").Contains(WorldManager.nuetralScript.bases[0]);
			int c;
			if(contains){
				c = 1;
			}
			else{
				c = 0;
			}
			sys.addInfo(new Datum(RuleBasedSystem.SOLDIER_CAN_GET_TO_NEUTRALBASE, c));
		}

		Match CAN_GET_TO_NEUTRALBASE = new DatumMatch(RuleBasedSystem.SOLDIER_CAN_GET_TO_NEUTRALBASE, 1, 1);
		Not notAlreadyASolider = new Not(RuleBasedSystem.alreadyASolider);
		And a = new And(RuleBasedSystem.redHasBases, RuleBasedSystem.isANuetralBase);
		And b = new And(a, CAN_GET_TO_NEUTRALBASE);
		And c2 = new And(b, notAlreadyASolider);
		this.ifClause = c2;

		//put rule in database
		sys.addRule(this);
		this.ai = ai;
	

	}

	public override void action(List<Binding> bindings){
		Debug.Log("SoldierCanGetToNuetralBase Rule fired");
		ai.createNewPlayer(WorldManager.redScript.bases[0], "BadSoldier");
	}


}
