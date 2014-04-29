using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Match {
	public Identifier identifier;
	public Match(){

	}
	public Match(Identifier identifier){
		this.identifier = identifier;
	}

	public virtual bool matches(Database database, List<Binding> bindings){
		//when part of the IfClause matches a "dont' care" value (wild card) it is added to the bindings
		//foreach item in the database
		foreach(DataNode item in database.dataItems){
			//we've matched if we match any item
			//if matchesItem(item, bindings)
			if(matchesItem(item, bindings)){
				return true;
			}//if
		}
		return false;
	}

	public virtual bool matchesItem(DataNode item, List<Binding> bindings){
		//is the item of the same type?
		if(!(item is DataNode)){
			return false;
		}
		
		
		//does the identifier match?
		if(identifier.isWildCard && identifier.number != item.identifier.number ){
			return false;
		}
		
		

		//Do we need to add to the bindings list?
		if(identifier.isWildCard){
			Binding b = new Binding(identifier, item);
			bindings.Add(b);
		}
		return true;
		

		
	}//method




}//class

