using UnityEngine;
using System.Collections;

public class ActionsMenu : MonoBehaviour {
	public bool canMove = false;
	public bool canAttack = false;
	public bool isOn = false;

	//void OnGUI () {
	void OnGUI(){

		if(isOn){
		int space = 0;//has spacing of thirty


		if(canMove){
			if(GUI.Button(new Rect(20,40,80,20), "Move")) {
				//Application.LoadLevel(1);
			}
			space +=30;
		}

		
		if(canAttack){
			if(GUI.Button(new Rect(20,40 + space,80,20), "Attack")) {
				//Application.LoadLevel(2);
			}
			space+=30;
		}

		if(GUI.Button(new Rect(20,40 + space,80,20), "Cancel")){
			//then cancel
			
		}
		space +=30;

		// Make a background box
		GUI.Box(new Rect(10,10,100,30 + space), "Actions");
		}
	}//method
}//class
