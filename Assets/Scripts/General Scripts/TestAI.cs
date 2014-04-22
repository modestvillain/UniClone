//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//public class AI : MonoBehaviour {
//
//	private int leafLevel=1;
//	private Map map;
//	// Use this for initialization
//	void Start () {
//		map = GameObject.FindGameObjectWithTag ("Map").GetComponent<Map>();
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
//
//	private void spawnPlayer(TeamManager TM, string playerType) {
//		if(TM.creditsAreSufficient(playerType)) {
//			TM.bases[0].createPlayerAndPositionOnBase(playerType);
//		}
//	}
//
//	public List<HexTile> getSuccessorState(State oldState, List<tuple> actions, string playerType) {
//
//		State state = new State(oldState);
//
//		foreach(tuple action in actions) {
//
//			Player attacker = state.red.team[actions[0]];
//			Hextile hex = state.map.tileList[actions[1]];
//			Player victim = state.blue[actions[2]];
//
//			if(hex != NULL)
//				attacker.move(hex);
//			if(victim != NULL)
//				attacker.attack(victim);
//		}
//		if(playerType != NULL) {
//			spawnPlayer(state.red, playerType);
//		}
//
//		return newState;
//	}
//
//	public tuple[] getBestMove(TeamManager myTeam,HexTile[] tileList, int credits) {
//
//		List<List<HexTile>> allMoves = new List<List<HexTile>>();
//		foreach(Player p in TM.team) {
//			allMoves.Add(map.legalMoves(p));
//		}
//
//		//get Legal Moves
//
//		//get all possible moves
//		foreach(List<HexTile> list in allMoves) {
//			foreach(Hextile move in list) {
//
//			}
//		}
//		//construct all successor states from each move into nodes
//		//loop through each node and use the getNodeValue() method
//
//		//return the move with the highest value
//
//		return null;
//	}
//
//private double getNodeValue(Node node, int level, boolean max){
//		int newLevel=level;
//		if (max == false) {
//			//enemy increments level count
//			newLevel=level++;
//		}
//
//		if (level == leafLevel) {
//			//get value using scoring function
//
//			//get list of all possible moves
//			List<List<tuple>> totalMoves=getCompleteMoveList (myTeam, tileList, credits);
//			double[] values=new double[totalMoves.size()];
//			for(int i=0; i<totalMoves.size(); i++){
//				//construct successor state node
//				//TODO
//				Node successor=new Node();
//				values=getScore(successor);
//			}
//		} 
//		else {
//			//recurse
//			//get list of all possible moves
//			List<List<tuple>> totalMoves=getCompleteMoveList (myTeam, tileList, credits);
//			double[] values=new double[totalMoves.size()];
//			for(int i=0; i<totalMoves.size(); i++){
//				//construct successor state node
//				//TODO
//				Node successor=new Node();
//				boolean newMax;
//				if(max==true){
//					newMax=false;
//				}
//				else{
//					newMax=true;
//				}
//				values[i]=getNodeValue (successor,newLevel,newMax);
//			}
//			if(max){
//				//choose largest value and return
//				//TODO
//
//			}
//			else{
//				//choose mininum value
//				//TODO
//			}
//
//		}
//
//	}
//
//	private List<List<tuple>> getCompleteMoveList(TeamManager team, HexTile[] tileList, int credits){
//		//get all friendly soldiers and loop through them
//
//		List<tuple> oneSingleMove=new List<tuple>();
//		List<Player> listOfFriendlies = myTeam.team;
//		int numSoldiers = listOfFriendlies.size ();
//
//		//need nested for loops where the number of nests changes
//		//dynamically depending on numSoldiers.
//
//		//loop through all moves for player1
//			//loop through all moves for player 2
//				//...
//				//loop through all moves for player 3
//					//List<tuple>.add(solder 1 move i)
//					//List<tuple>.add(soldier 2 mve j)
//					//...
//					//List<tuple>.add(soldier n move m)
//					//totalMoves.add(List<tuple>)
//
//
//		//BEST WAY IS TO JUST USE PERMUTATION TREE
//
//		for (int i=0; i<listOfFriendlies.size(); i++) {
//			//get all tiles legal to move to
//			Player soldier=listOfFriendlies.get (i);
//			List<HexTile> soldiersTiles=map.legalMoves (soldier);
//			for(int j=0; j<soldiersTiles.size(); j++){
//				//get all enemies attackable from soldier and tile
//				HexTile chosenTile=soldiersTiles.get (i);
//				List<HexTile> attackableEnemies=map.legalAttacks(soldier,chosenTile);
//				for(int k=0; k<attackableEnemies.size(); k++){
//					HexTile chosenEnemyTile=attackableEnemies.GetType (i);
//					Player chosenEnemy=chosenEnemyTile.getPlayer();
//					tuple order=new tuple(soldier,chosenTile,chosenEnemy);
//				}
//			}
//		}
//	}
//
//private void buildPermutationTree(string state, TeamManager myTeam){
//		StateNode s = new StateNode (state);
//
//		//for every friendly troop
//		List<Player> listOfFriendlies = myTeam.team;
//
//		//get list of friendlies, construct troopNodes from them
//		//add them as children to state node
//
//
//}
//
//private void createChildrenTrooperNodes(StateNode snode){
//	//take in StateNode, construct TroopNode children
//
//		string State = snode.getState ();
//
//		// get Troops to control
//		List<Player> troops = snode.getUnactedTroops();
//
//		//create children list
//		List<TroopNode> children = new List<TroopNode> (); 
//
//		if(troops.count !=0){
//
//		//iterate through each trooper, creater a trooper node
//		for (int i=0; i<troops.count; i++) {
//			Node newTroopnode=new TroopNode();
//				newTroopnode.setState(State);
//				newTroopnode.setActingTroop(troops[i]);
//				List<Player> newUnactedTroops=snode.getUnactedTroops();
//				newUnactedTroops.Remove (troops[i]);
//				newTroopnode.setUnactedTroops(newUnactedTroops);
//				createChildrenStateNodes(newTroopnode);
//				children.Add(newTroopnode);
//			}
//		snode.setChildren(children);
//	}
//	
//}
//
//private void createChildrenStateNodes(TroopNode troop){
//	//take in TroopNode, construct StateNode children
//
//		//get state
//		StateNode = troop.getState ();
//
//		//get acting trooper
//		Player actingTrooper = troop.getActingTroop ();
//
//		//getAvailable actions list
//	//****	//List<Actions> actionList=get action list somehow
//
//		//create children list
//		List<StateNode> children = new List<StateNode> ();
//
//		for (int i=0; i<actionsList.count; i++) {
//			//get successor state
//			//create new statenode:
//			StateNode snode=new StateNode();
//			snode.setUnactedTroops(troop.getUnactedTroops());
//			snode.setState(successor);
//			createChildrenTrooperNodes(snode);
//			children.Add (snode);
//		}
//		troop.setChildren (children);
//
//
//}
//
//
//private class Node{
//		private double value;
//
//		void start(){
//
//		}
//
//		public void setValue(double val){
//			value = val;
//		}
//
//		public double getValue(){
//			return value;
//		}
//
//
//	}
//
//
//public class StateNode{
//		private string State; //change later to actual state holder
//		private List<TroopNode> children;
//		private List<Player> unactedTroops;
//
//		public void setStateNode(StateNode s){
//
//			State=s;
//		}
//
//		public void setChildren(List<TroopNode> list){
//			children = list;
//		}
//
//		public void setUnactedTroops(List<Player> unacted){
//			unactedTroops = unacted;
//		}
//
//		public List<TroopNode> getChildren(){
//			return children;
//		}
//
//		public string getState(){
//			return State;
//		}
//	}
//
//public class TroopNode{
//		private string State;
//		private Player actingTroop;
//		private List<StateNode> children;
//		private List<Player> unactedTroops;
//		 
//		public TroopNode(){
//		
//		}
//
//		public string getState(){
//			return State;
//		}
//
//		public void setState(string state){
//			State = state;
//		}
//
//		public void setUnactedTroops(List<Player> unacted){
//			unactedTroops = unacted;
//		}
//
//		public List<Player> getUnactedTroops(){
//			return unactedTroops;
//		}
//
//		public void setChildren(List<StateNode> childs){
//			children = childs;
//		}
//
//		public void setActingTroop(Player p){
//			actingTroop = p;
//		}
//
//		public Player getActingTroop(){
//			return actingTroop;
//		}
//	}
//
//public class tuple {
//		private int attacker;
//		private int target;
//		private int destination;
//
//		public tuple(Player attckr, HexTile dest, Player targ){
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
//
//public class State {
//
//	List<HexTile> map;
//	TeamManager blue;
//	TeamManager red;
//
//	public State(List<HexTile> oldmap, TeamManager oldblue, TeamManager oldred) {
//		map = new List<HexTile>(oldmap);
//		blue = copyTM(oldblue);
//		red = copyTM(oldred);
//	}
//
//	public State(State s) {
//		map = new List<HexTile>(s.map);
//		blue = copyTM(s.blue);
//		red = copyTM(s.red);
//	}
//
//	public TeamManager copyTM(TeamManager tm) {
//		TeamManager TM = new TeamManager();
//		TM.team = new List<Player>(tm.team);
//		TM.CREDITS = tm.CREDITS;
//		TM.bases = new List<Bases>(tm.bases);
//	}
//
//	public double scoringFunction(){
//		double score = 0;
//		//total health of all players should be greater than the total health of theirs-- your total health minus theirs
//		score += red.totalHealth() - blue.totalHealth();
//		//money is good in the begining-- want enough money to buy something substantial, for now just have money
//		score += red.CREDITS;
//		// want a player that has high enough damage to take out the player's on their map (so want stronger players)-- your total damage minus theirs
//		score += red.totalDamage() - blue.totalDamage();
//		// better if total strength is better than theirs-- your total strength minus theirs, offense
//		score += red.totalDefense() - blue.totalDefense();
//		// better if you have more bases than they do -- your bases minus their bases
//		score += red.bases.Count - blue.bases.Count;
//
//		//other things
//		//the proximity of individual troops
//		return score;
//	}//method
//
//
//}//state class
//}//class