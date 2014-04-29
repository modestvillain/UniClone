using UnityEngine;
using System.Collections;

public class Datum : DataNode {
	//leaves in the tree contain actual values 
	//single item in the database, contains identifier and value
	//either holds a value or a set of Datum objects
	public int value;
	public Datum(){
	}
	public Datum(Identifier identifier, int value){
		this.identifier = identifier;
		this.value = value;
	}

	public override string print()
	{

	   return "( " + this.identifier.name + " , " + this.value +" )";

	}



}
