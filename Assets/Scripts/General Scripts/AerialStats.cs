using UnityEngine;
using System.Collections;

public class AerialStats: IStats{

	public static int COST = 350;
	public static int ATTACKRANGE = 2;
	public static int MOBILITY = 3;
	public static int DEFENSE = 7;
	public static bool CANCAPTURE = false;
	public static bool CANATTACKAFTERMOVE = true;
	public static int REPAIR = 1;
	public static int HEALTH = 15;
	public static int DAMAGE = 6;

	public int getCost(){
		return AerialStats.COST;
	}

	public int getAttackRange(){
		return AerialStats.ATTACKRANGE;
	}
	public int getMobility(){
		return AerialStats.MOBILITY;
	}
	public int getDefense(){
		return AerialStats.DEFENSE;
	}
	public bool getCanCapture(){
		return AerialStats.CANCAPTURE;
	}
	public bool getCanAttackAfterMove(){
		return AerialStats.CANATTACKAFTERMOVE;
	}
	public int getRepair(){
		return AerialStats.REPAIR;
	}
	public int getStartHealth(){
		return AerialStats.HEALTH;
	}
	public int getDamage(){
		return AerialStats.DAMAGE;
	}



}//class
