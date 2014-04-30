using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DatumMatch : Match {

		//public Identifier identifier;
		public int minValue;
		public int maxValue;
		public DatumMatch(){
		}
		public DatumMatch(Identifier identifier, int minValue, int maxValue){
			this.identifier = identifier;
			this.minValue = minValue;
			this.maxValue = maxValue;
		}
		
		public override bool matchesItem(DataNode item, List<Binding> bindings){
			//tries each individual item in the database against a matchesItem method
			//the matchesItem method should check a specific data node for matching.
			//the whole match succeeds if any item in the database matches
			
			//4-byte number as an identifier and reserves the first bit to indicate if the identifier is a wild card
			//the isWildcard method just checks for this bit
			
			//the bindings list has been given an appendBinding method that adds an identifier which is always
			//a wild card and the database item it was matched to 
			//we could have it be a list of pair templates and append a new identifier , item pair.
			
			//is the item of the same type?
			if(!(item is Datum)){
				return false;
			}
			
			
			//does the identifier match?
			if(identifier.isWildCard && identifier.number != item.identifier.number ){
					return false;
			}
			
			
			//Does the value fit?
		if((minValue <= (int)((Datum)item).value) && ((int)((Datum)item).value <= maxValue) ){
				//Do we need to add to the bindings list?
				if(identifier.isWildCard){
					Binding b = new Binding(identifier, item);
					bindings.Add(b);
				}
				return true;
		}
		else{
			return false;
		}

		}//method
		
	}//class

