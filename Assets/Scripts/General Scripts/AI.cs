using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public tuple[] getMoveList(Team myTeam,HexTile[] tileList, int credits){
		//get Legal Moves
			//get all friendly troops
		Player[] listOfFriendlies = myTeam.getTroops ();

			//get list of legal destinations
		for (int i=0; i<listOfFriendlies.Length; i++) {


			}
			//get list of legal attack targets

		//select moves randomly
		float rand = Random.Range (0.0f, 1.0f);

		actionList = new tuple[3];
		return actionList;

	}

public class tuple{
		private Player attacker;
		private Player target;
		private HexTile destination;

		void start(Player attckr, HexTile dest, Player targ){
			attacker = attckr;
			destination = dest;
			target = targ;
		}

		public Player getAttacker(){
			return attacker;
		}

		public Player getTarget(){
			return target;
		}

		public HexTile getDestination(){
			return destination;
		}
	}
}
