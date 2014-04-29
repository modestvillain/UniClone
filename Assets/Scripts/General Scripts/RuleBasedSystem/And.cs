using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class And : Match {

		public Match match1;
		public Match match2;

		public And(){
		}
		public And(Match match1, Match match2){
			this.match1 = match1;
			this.match2 = match2;
		}
		
	public override bool  matches(Database db, List<Binding> bindings){
			//true is we match both sub-matches
			return match1.matches(db, bindings)&& match2.matches(db, bindings);
		}
		

}
