using UnityEngine;
using System.Collections;

public class DataNode{
	public Identifier identifier;
	//if no value, then this thing is a boolean

	public DataNode(){
		
	}
	public DataNode(Identifier identifier){
		this.identifier = identifier;
	}

	public virtual string print(){

		return "( " + identifier.name + " )";
			

	}

}//class
