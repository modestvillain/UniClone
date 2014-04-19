//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//public class AI : MonoBehaviour {
//
//	// Use this for initialization
//	void Start () {
//
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
//
//	public tuple[] getMoveList(TeamManager myTeam,HexTile[] tileList, int credits){
//		//get Legal Moves
//		//get all friendly troops
//		List<Player> listOfFriendlies = myTeam.team;
//
//		//get list of legal destinations
//		for (int i=0; i<listOfFriendlies.Count; i++) {
//
//
//			}
//			//get list of legal attack targets
//
//		//select moves randomly
//		float rand = Random.Range (0.0f, 1.0f);
//
//		tuple actionList = new tuple();
//		return actionList;
//
//	}
//
//public class tuple{
//		private Player attacker;
//		private Player target;
//		private HexTile destination;
//
//		void Start(Player attckr, HexTile dest, Player targ){
//			attacker = attckr;
//			destination = dest;
//			target = targ;
//		}
//
//		public Player getAttacker(){
//			return attacker;
//		}
//
//		public Player getTarget(){
//			return target;
//		}
//
//		public HexTile getDestination(){
//			return destination;
//		}
//	}
//}
