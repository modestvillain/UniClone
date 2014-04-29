using UnityEngine;
using System.Collections;

public class Identifier  {

	public int number;
	public bool isWildCard;
	public string name;

	public static int count = 0;

	public Identifier(){
		count++;
	}
	public Identifier(bool isWildCard, string name){
		this.number = count;
		this.isWildCard = isWildCard;
		this.name = name;
		count++;
		
	}
}
