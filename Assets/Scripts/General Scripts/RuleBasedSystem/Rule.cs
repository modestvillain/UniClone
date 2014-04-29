using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Rule  {

	public Match ifClause;
	//if clause consists of a set of data items, in a format similar to those in the database, joined by 
	//boolean operators. They need to be able to match the database, so use a general data structure as the base
	//class of elements in an If-clause


	public Rule(){

	}
	public Rule(Match ifClause){
		this.ifClause = ifClause;
	}

	public virtual void action(List<Binding> bindings){
		Debug.Log ("Action Fired");
		//can perform any action required, including changind database contents
		//It takes a list of bindings which is filled with the items in the database that match any
		//wild cards in the ifClause
		foreach(Binding b in bindings){
			Debug.Log(b.print());
			// need to get it to create the player solider

		}

		Debug.Log ("end action fired");


	//	DummyAI d = new DummyAI();
	//	d.createNewPlayer(base, "BadSoldier");


	}



}//class
