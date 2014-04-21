using UnityEngine;
using System.Collections;

public class HeavyStats : IStats {

	public static int COST = 400;
	public static int ATTACKRANGE = 1;
	public static int MOBILITY = 2;
	public static int DEFENSE = 12;
	public static bool CANCAPTURE = false;
	public static bool CANATTACKAFTERMOVE = true;
	public static int REPAIR = 1;
	public static int HEALTH = 15;
	public static int DAMAGE = 9;

	public int getCost(){
		return HeavyStats.COST;
	}
	
	public int getAttackRange(){
		return HeavyStats.ATTACKRANGE;
	}
	public int getMobility(){
		return HeavyStats.MOBILITY;
	}
	public int getDefense(){
		return HeavyStats.DEFENSE;
	}
	public bool getCanCapture(){
		return HeavyStats.CANCAPTURE;
	}
	public bool getCanAttackAfterMove(){
		return HeavyStats.CANATTACKAFTERMOVE;
	}
	public int getRepair(){
		return HeavyStats.REPAIR;
	}
	public int getStartHealth(){
		return HeavyStats.HEALTH;
	}
	public int getDamage(){
		return HeavyStats.DAMAGE;
	}
}
