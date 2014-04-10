using UnityEngine;
using System.Collections;

public interface IStats {

	int getCost();
	int getAttackRange();
	int getMobility();
	int getDefense();
	bool getCanCapture();
	bool getCanAttackAfterMove();
	int getRepair();
	int getStartHealth();
	int getDamage();


}
