using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataGroup : DataNode
{
	
		//No-leaf nodes correspond to data groups in the data and have the following form
		//DataGroups in the database hold additional data nodes, so overall the database becomes a tree of information
		public List<DataNode> children;// the children of a data group can be any data node: either another data group or a Datum
		public DataGroup ()
		{
				this.children = new List<DataNode> ();
		}

		public override string print ()
		{
				string st = " <DataGroup> ";
				st += this.identifier.name;

				foreach (DataNode dn in this.children) {
						st += dn.print ();
				}
				st += " </DataGroup> ";
				return st;
		}

		public void addToChildren (DataNode dn)
		{
				this.children.Add (dn);
		}

}
