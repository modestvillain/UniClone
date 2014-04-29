using UnityEngine;
using System.Collections;

public class Binding  {

	public Identifier identifier;
	public DataNode item;
	public Binding(){

	}
	public Binding(Identifier identifier, DataNode item){
		this.identifier = identifier;
		this.item = item;
	}

	public string print(){
		return  identifier.name + " DataNode: " + this.item.print();
	}
}
