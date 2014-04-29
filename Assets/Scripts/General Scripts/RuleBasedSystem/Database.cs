using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Database {
	public List<DataNode> dataItems;
	//The database can simply be a list or array of data items, represented by the DataNode class
	//DataGroups in the database hold additional data nodes, so overall the database becomes a tree of information
	public Database(){
		dataItems = new List<DataNode>();
	}


	public void remove(){

	}

	public void add(DataNode dn){
		this.dataItems.Add(dn);
	}

	public string print(){
		string st = " <DataBase>" + "\n";
		foreach(DataNode dn in dataItems){
			st +=  dn.print ();
		}
		st +=   "</DataBase> ";
		return st;
	}

}//class
