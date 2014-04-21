using UnityEngine;
using System.Collections;

public class SoldierStats: IStats{
	
	public static int COST = 150;
	public static int ATTACKRANGE = 1;
	public static int MOBILITY = 3;
	public static int DEFENSE = 6;
	public static bool CANCAPTURE = true;
	public static bool CANATTACKAFTERMOVE = true;
	public static int REPAIR = 1;
	public static int HEALTH = 15;
	public static int DAMAGE = 6;

	public int getCost(){
		return SoldierStats.COST;
	}
	
	public int getAttackRange(){
		return SoldierStats.ATTACKRANGE;
	}
	public int getMobility(){
		return SoldierStats.MOBILITY;
	}
	public int getDefense(){
		return SoldierStats.DEFENSE;
	}
	public bool getCanCapture(){
		return SoldierStats.CANCAPTURE;
	}
	public bool getCanAttackAfterMove(){
		return SoldierStats.CANATTACKAFTERMOVE;
	}
	public int getRepair(){
		return SoldierStats.REPAIR;
	}
	public int getStartHealth(){
		return SoldierStats.HEALTH;
	}
	public int getDamage(){
		return SoldierStats.DAMAGE;
	}
}
