using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DataGroupMatch : Match{
	
	public Identifier identifier;
	//public List<DatumMatch> children;
	public List<Match> children;

	public DataGroupMatch(){
		//this.children = new List<DatumMatch>();
		this.children = new List<Match>();
	}
	
	public override bool matchesItem(DataNode item, List<Binding> bindings){
		//is the item of the same type
		if(!(item is DataGroup)){
			return false;
		}
		
		//does the identifier match?
		if(identifier.isWildCard && identifier.number != item.identifier.number ){
			return false;
		}
	
		//is every child present
		foreach(DatumMatch child in this.children){
				//use the children of the item as if it were a database and call matches recursively
		Database pretend = new Database();
		pretend.dataItems = ((DataGroup)item).children;
		if(!child.matches(pretend, bindings)){
					return false;
				}

		}
		//we must have matched all children
		
		//Do we need to add to the bindings list?
		if(identifier.isWildCard){
			Binding b = new Binding(identifier, item);
			bindings.Add(b);
		}
		
		return true;
	}//method
}

