using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Not : Match {
	
		public Match match;
		public Not(Match match){
			this.match = match;
		}
	public override bool matches(Database db, List<Binding> bindings){
			//true if we don't match submatch
			//note we pass in new bindings list, because we're not interested in anything found;
			//we're making sure there are no matches
		return ! this.match.matches(db, new List<Binding>());
		}
		
	}//pclass

