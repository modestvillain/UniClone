using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI : MonoBehaviour {

	/***
	 * LEFT TODO
	 * getcompletemovelist needs to work based on team
	 * create getmostexpensive respawn function
	 * tweak permutation tree to deal with spawns
	 * 
	 * *****/

	public TeamManager TM;
	public int leafLevel=1;
	public static AIMap map;

	public void virtualSpawn(AITM tm, string playerType) {

		if(tm.creditsAreSufficient(playerType) && !tm.bases[0].isOccupied()) {
			tm.bases[0].createPlayerAndPositionOnBase(playerType);
		}
	}

	public void spawnPlayer(TeamManager tm, string playerType) {

		if(tm.creditsAreSufficient(playerType) && !tm.bases[0].isOccupied()) {
			tm.bases[0].createPlayerAndPositionOnBase(playerType);
		}
	}

	public State getSuccessorState(State oldState, Action action) {
		
		State state = new State(oldState);

		AIPlayer attacker = state.getPlayer(action.attacker);
		AIHexTile hex = state.getHex(action.destination);
		AIPlayer victim = state.getPlayer(action.target);

		if(attacker != null) {
			if(hex != null)
				attacker.move(hex);
			if(victim != null)
				attacker.attack(victim);
		}

		return state;
	}

	public State getResultantState(State oldState, List<Action> actions) {
		
		State state = new State(oldState);

		foreach(Action action in actions) {

			AIPlayer attacker = state.getPlayer(action.attacker);
			AIHexTile hex = state.getHex(action.destination);
			AIPlayer victim = state.getPlayer(action.target);

			if(attacker != null) {
				if(hex != null)
					attacker.move(hex);
				if(victim != null)
					attacker.attack(victim);
			}
		}
	
		if(actions[0].respawnType != null) {
			virtualSpawn(state.TM, actions[0].respawnType);
		}
		
		return state;
	}

	public string getMostExpensive(int credits) {
		/* JILL PUT YO SHIT HERE */
		/* JILL PUT YO SHIT HERE */
		/* JILL PUT YO SHIT HERE */
		/* JILL PUT YO SHIT HERE */
		/* JILL PUT YO SHIT HERE */
		return "BadSoldier";
	}

	public void startTurn(TeamManager myTeam, TeamManager enemyTeam, Map map) {

		AIMap m = new AIMap(map);
		AITM TM = new AITM(myTeam, m);
		AITM enemyTM = new AITM(enemyTeam, m);
		TM.enemy = enemyTM;
		enemyTM.enemy = TM;
		List<Action> vActions = getBestMove(TM, enemyTM, m);
		List<Action> actions = new List<Action>();

		for(int i=1; i<vActions.Count; i++) {

			Action action = vActions[i];
			Player attacker=null;
			HexTile hex=null;
			Player victim=null;

			if(action.attacker != null && myTeam.team.Count>0)
				attacker = myTeam.team[action.attacker.id];
			if(action.destination != null)
				hex = map.tiles[action.destination.x, action.destination.y];
			if(action.target != null && enemyTeam.team.Count>0)
				victim = enemyTeam.team[action.target.id];
			if(hex != null) {
				if(hex.gameObject.tag=="Base") {
					attacker.capture((Base)hex);
				}
				else {
					attacker.move(hex.gameObject);
				}
			}
			if(victim != null)
				attacker.attack(victim);
		}
		if(vActions[0].respawnType != null) {
			spawnPlayer(myTeam, vActions[0].respawnType);
		}
	}

	public List<Action> getBestMove(AITM myTeam, AITM enemyTeam, AIMap map) {

		State state = new State (myTeam, enemyTeam, map);

		List<List<Action>> totalMoves=getCompleteMoveList (myTeam, enemyTeam, map);

		double[] values=new double[totalMoves.Count];

		for(int i=0; i<totalMoves.Count; i++){

			int credits=state.TM.CREDITS;
			State successor = getResultantState(state,totalMoves[i]);
			Node node = new Node(successor);
			//values[i]=getNodeValue(node, successor.level, true);
			values[i]=node.state.scoringFunction(.6,.7,2);
		}

		int bestId = 0;
		double bestValue = -99999999999;
		for (int i=0; i<totalMoves.Count; i++) {
			if(values[i]>=bestValue){
				bestValue=values[i];
				bestId=i;
			}
		}

		Action respawnTarget = new Action ();
		
		if(totalMoves.Count==0)													/* INITIALIZER */
			totalMoves.Add(new List<Action>());

		totalMoves[bestId].Insert (0, respawnTarget);

		if(myTeam.team.Count<3) {
			respawnTarget.respawnType = getMostExpensive(myTeam.CREDITS);
		}

		return totalMoves[bestId];
	}

private double getNodeValue(Node node, int level, bool max) {

		int newLevel=level;

		AITM myTeam = node.state.TM;
		AITM enemyTeam = node.state.enemyTM;
		AIMap map = node.state.map;

		if (!max) {
			//enemy increments level count
			newLevel=level++;
		}

		if (level == leafLevel) {
			//get value using scoring function

			if(max){
			//get list of all possible moves
			List<List<Action>> totalMoves=getCompleteMoveList (myTeam, enemyTeam, map);

			double[] values=new double[totalMoves.Count];
			for(int i=0; i<totalMoves.Count; i++){
				//construct successor state node
				int credits=myTeam.CREDITS;
				State successor=getResultantState (node.state,totalMoves[i]);
				values[i]=node.state.scoringFunction(.6,.7,2);														/* PLACEHOLDER FOR REAL WEIGHTS*/
				}
				double nodeValue=-999999999;
				for(int i=0; i<totalMoves.Count; i++){
					if(values[i]>nodeValue){
						nodeValue=values[i];
					}
			 	}
				return nodeValue;
			}
			else{
				//min
				//get list of all possible moves
				List<List<Action>> totalMoves=getCompleteMoveList (myTeam, enemyTeam, map);
				double[] values=new double[totalMoves.Count];
				for(int i=0; i<totalMoves.Count; i++){
					//construct successor state node
					int credits=node.state.TM.CREDITS;
					State successor=getResultantState (node.state,totalMoves[i]);
					values[i]=node.state.scoringFunction(.6,.7,2);													/* PLACEHOLDER FOR REAL WEIGHTS*/
				}
				double nodeValue=999999999;
				for(int i=0; i<totalMoves.Count; i++){
					if(values[i]<nodeValue){
						nodeValue=values[i];
					}
				}
				return nodeValue;
			}
		}

		else {

			if(max) {
				//recurse
				//get list of all possible moves

				List<List<Action>> totalMoves = getCompleteMoveList(myTeam, enemyTeam, map);
				double[] values = new double[totalMoves.Count];

				for(int i=0; i<totalMoves.Count; i++){
					//construct successor state node
					
					int credits=node.state.TM.CREDITS;
					State successorState = getResultantState(node.state,totalMoves[i]);
					Node successor = new Node(successorState);
					bool newMax;
					if(max==true){
						newMax = false;
					}
					else{
						newMax = true;
					}
					values[i] = getNodeValue (successor,newLevel,newMax);				
               }

				//max value
				double nodeValue=-999999999;
				for(int i=0; i<totalMoves.Count; i++){
					if(values[i]>nodeValue){
						nodeValue=values[i];
					}
				}
				return nodeValue;

			}
			else{
				//choose mininum value
				//total moves needs to work based on team
				List<List<Action>> totalMoves=getCompleteMoveList (enemyTeam, myTeam, map);
				double[] values=new double[totalMoves.Count];
				for(int i=0; i<totalMoves.Count; i++){
					
					int credits=node.state.enemyTM.CREDITS;
					State successorState = getResultantState (node.state,totalMoves[i]);
					Node successor = new Node(successorState);
					bool newMax;
					if(max==true){
						newMax=false;
					}
					else{
						newMax=true;
					}
					values[i]=getNodeValue (successor,newLevel,newMax);
				}

				double nodeValue=99999999;
				for(int i=0; i<totalMoves.Count; i++){
					if(values[i]<nodeValue){
						nodeValue=values[i];
					}
				}
				return nodeValue;
			}

		}
	}
	
	private List<List<Action>> getCompleteMoveList(AITM myTeam, AITM enemyTeam, AIMap m){

		StateNode root = buildPermutationTree (myTeam, enemyTeam, m);

		List<List<Action>> totalActions = traverseTree (root);

		return totalActions;
	
	}

	private List<Action> getLegalActions(State state, AIPlayer attacker){

		List<AIHexTile> actionTiles=state.legalMoves(attacker);
		List<Action> actionList=new List<Action>();
		AIHexTile h = attacker.hex;

		for (int i=0; i<actionTiles.Count; i++) {
			Action action = new Action();
			action.attacker = attacker;
			action.destination=actionTiles[i];
			attacker.move(actionTiles[i]);
			List<AIPlayer> legal = state.legalAttacks(attacker);
			if(legal.Count>0) {
				foreach(AIPlayer p in legal) {
					Action a = new Action(action);
					a.target = p;
					actionList.Add (a);
				}
			}
			else {
				actionList.Add (action);
			}
		}

		attacker.move(h);
		return actionList;
	}

	private List<List<Action>> traverseTree(StateNode snode) {

		List<List<Action>> totalList = new List<List<Action>> ();

		if (snode.children != null) {

			List<TroopNode> troopChilds = snode.children;
			for (int i=0; i<troopChilds.Count; i++) {

				AIPlayer attacker = troopChilds[i].actingTroop;
				List<Action> actions = getLegalActions (snode.state, attacker);
				List<StateNode> successors = troopChilds[i].children;

				for (int j=0; j<successors.Count; j++) {
					
					List<List<Action>> lists = traverseTree (successors [j]);
					List<Action> actionList = new List<Action>();
					totalList.Add(actionList);
					Action newAction = new Action (actions[j]);
					actionList.Add(newAction);

					for (int k=0; k<lists.Count; k++) {
						lists [k].Insert (0, newAction);
						totalList.Add (lists [k]);
					}
				}
			}
		}

		return totalList;
	}

	private StateNode buildPermutationTree(AITM myTeam, AITM enemyTeam, AIMap m) {

		State state = new State(myTeam, enemyTeam, m);
		StateNode root = new StateNode(state);
		root.unactedTroops = state.TM.team;
		createChildrenTrooperNodes (root);

		return root;
	}

	private void createChildrenTrooperNodes(StateNode snode) {

		State state = snode.state;
		List<AIPlayer> troops = snode.unactedTroops;
		List<TroopNode> children = new List<TroopNode>(); 

		if(troops.Count > 0) {

			for (int i=0; i<troops.Count; i++) {

				TroopNode newTroopnode = new TroopNode(state);
				newTroopnode.actingTroop = newTroopnode.state.TM.team[i];
				List<AIPlayer> newUnactedTroops = new List<AIPlayer>(snode.unactedTroops);
				newUnactedTroops.Remove (troops[i]);
				newTroopnode.unactedTroops = newUnactedTroops;
				createChildrenStateNodes(newTroopnode);
				children.Add(newTroopnode);
			}

			snode.children = children;
		}
	}

	private void createChildrenStateNodes(TroopNode troop) {

		State s = new State(troop.state);
		AIPlayer copyTroop = s.getPlayer(troop.actingTroop);
		List<Action> actionList=getLegalActions(s, copyTroop);
		List<StateNode> children = new List<StateNode> ();

		for (int i=0; i<actionList.Count; i++) {

			StateNode snode=new StateNode(getSuccessorState(s,actionList[i]));
			snode.unactedTroops = new List<AIPlayer>(troop.unactedTroops);
			createChildrenTrooperNodes(snode);
			children.Add (snode);
		}

		troop.children = children;
	}


	private class Node {

		public double value;
		public State state;
	
		public Node(State s) {
			state = s;
		}
	}


	public class StateNode {

		public State state; //change later to actual state holder
		public List<TroopNode> children;
		public List<AIPlayer> unactedTroops;

		public StateNode(State s) {
			state= new State(s);
		}
	}

	public class TroopNode {

		public State state;
		public AIPlayer actingTroop;
		public List<StateNode> children;
		public List<AIPlayer> unactedTroops;
		 
		public TroopNode(State s) {
			state = s;
		}
	}

	public class Action {
		public AIPlayer attacker;
		public AIPlayer target;
		public AIHexTile destination;
		public string respawnType;

		public Action() {

		}

		public Action(Action a) {
			attacker = a.attacker;
			target = a.target;
			destination = a.destination;
			respawnType = a.respawnType;
		}

		public Action(AIPlayer attckr, AIHexTile dest, AIPlayer targ, string respawn) {
			attacker = attckr;
			destination = dest;
			target = targ;
			respawnType = respawn;
		}
	}

	public class State {
		
		public AIMap map;
		public AITM TM;
		public AITM enemyTM;
		public int level=0;

		public State(AITM TM, AITM enemyTM, AIMap map) {
			this.map = new AIMap(map);
			this.TM = new AITM(TM, map);
			this.enemyTM = new AITM(enemyTM, map);
		}
		
		public State(State s) {
			this.map = new AIMap(s.map);
			this.TM = new AITM(s.TM, map);
			this.enemyTM = new AITM(s.enemyTM, map);
		}
		
		public List<AIHexTile> legalMoves(AIPlayer p) {
			p = getPlayer(p);
			List<AIHexTile> legal = map.legalMoves(p);
			return legal;
		}

		public List<AIPlayer> legalAttacks(AIPlayer p) {
			p = getPlayer(p);
			List<AIPlayer> legal = map.legalAttacks(p);
			return legal;
		}

		public AIHexTile getHex(AIHexTile hex) {
			foreach(AIHexTile ht in map.tileList) {
				if(ht.x==hex.x && ht.y==hex.y)
					return ht;
			}
			return null;
		}

		public AIPlayer getPlayer(AIPlayer p) {
			//Debug.Log(p.id);
			if(TM.team.Count==0 || p==null)	return null;
			if(p.side==TM.side) {
				return TM.team[p.id];
			}
			else {
				return enemyTM.team[p.id];
			}
		}
		
		public double scoringFunction(double weightForCloseToBases, double weightForCloseToEnemy, double weightForNumBases){
			double score = 0;
			//total health of all players should be greater than the total health of theirs-- your total health minus theirs
			score += TM.totalHealth() - enemyTM.totalHealth();
			//money is good in the begining-- want enough money to buy something substantial, for now just have money
			score += TM.CREDITS;
			// want a player that has high enough damage to take out the player's on their map (so want stronger players)-- your total damage minus theirs
			score += TM.totalDamage() - enemyTM.totalDamage();
			// better if total strength is better than theirs-- your total strength minus theirs, offense
			score += TM.totalDefense() - enemyTM.totalDefense();
			// better if you have more bases than they do -- your bases minus their bases
			score += 2*(TM.bases.Count - enemyTM.bases.Count);
			
			// how close troop is to base how close they are minus how close you are to bases
			score+= enemyTM.totalMinDistanceToDesiredBases(weightForCloseToBases) - TM.totalMinDistanceToDesiredBases(weightForCloseToBases);
			
			//make weight of close to other enemy players more for those who can't capture bases
			score += enemyTM.totalMinDistanceToEnemy(weightForCloseToEnemy) - TM.totalMinDistanceToEnemy(weightForCloseToEnemy);
			return score;
		}
	}

	public class AIHexTile {
		public int x;
		public int y;
		public AIPlayer p;
		public string type;
		public AIPlayer occupant;
		public List<AIHexTile> neighbors = new List<AIHexTile>();

		public AIHexTile() {}

		public AIHexTile(int x, int y) {
			this.x = x;
			this.y = y;
		}
	}

	public class AIBase : AIHexTile {
		public string side;
		public AITM TM;
		public bool hasBeenCaptured=false;

		public AIBase() {}

		public AIBase(int x, int y) {
			this.x = x;
			this.y = y;
		}

		public void createPlayerAndPositionOnBase(string type) {
			AIPlayer p = new AIPlayer(type, TM);
			this.occupant = p;
			p.hex = this;
		}

		public bool isOccupied() {
			return occupant != null;
		}

		public int distanceFromBase(AIHexTile ht) {
			return (int)Mathf.Sqrt((int)Mathf.Pow(ht.x - this.x,2) + (int)Mathf.Pow(ht.y - this.y,2));
		}
	}

	public class AIPlayer {
		public AIHexTile hex;
		public AITM TM;
		public int id;
		public int HP;//health
		public int DMG;//damage - attack strength
		public int DEF;//defense
		public int MOB;//mobility
		public int cost;
		public int attackRange;
		public bool canCapture;
		public string type;
		public string side;

		public AIPlayer() {}

		public AIPlayer(AIPlayer p, AITM tm) {

			this.HP = p.HP;
			this.DMG = p.DMG;
			this.DEF = p.DEF;
			this.MOB = p.MOB;
			this.cost = p.cost;
			this.attackRange = p.attackRange;
			this.canCapture = p.canCapture;
			this.type = p.type;

			if(p.hex != null) {
				this.hex = tm.map.tiles[p.hex.x, p.hex.y];
			}
			this.side = p.side;
			this.type = p.type;
			TM = tm;
		}

		public AIPlayer(Player p, AITM tm) {

			this.HP = p.HP;
			this.DMG = p.DMG;
			this.DEF = p.DEF;
			this.MOB = p.MOB;
			this.cost = p.cost;
			this.attackRange = p.attackRange;
			this.canCapture = p.canCapture;
			this.type = p.type;

			if(p.currentTileScript != null) {
				hex = tm.map.tiles[p.currentTileScript.x, p.currentTileScript.y];
			}
			TM = tm;
		}

		public AIPlayer(string type, AITM tm) {

			this.id = tm.maxID;
			tm.maxID++;
			this.type = type;
			this.side = TM.side;
			TM = tm;

			if(type=="BadAerial" || type=="Aerial") {
				this.cost = AerialStats.COST;
				this.attackRange = AerialStats.ATTACKRANGE;
				this.MOB = AerialStats.MOBILITY;
				this.DEF = AerialStats.DEFENSE;
				this.canCapture = AerialStats.CANCAPTURE;
				this.HP = AerialStats.HEALTH;
				this.DMG = AerialStats.DAMAGE;
			}
			else if(type=="BadSoldier" || type=="Soldier") {
				this.cost = SoldierStats.COST;
				this.attackRange = SoldierStats.ATTACKRANGE;
				this.MOB = SoldierStats.MOBILITY;
				this.DEF = SoldierStats.DEFENSE;
				this.canCapture = SoldierStats.CANCAPTURE;
				this.HP = SoldierStats.HEALTH;
				this.DMG = SoldierStats.DAMAGE;
			}
			else if(type=="BadHeavy" || type=="Heavy") {
				this.cost = HeavyStats.COST;
				this.attackRange = HeavyStats.ATTACKRANGE;
				this.MOB = HeavyStats.MOBILITY;
				this.DEF = HeavyStats.DEFENSE;
				this.canCapture = HeavyStats.CANCAPTURE;
				this.HP = HeavyStats.HEALTH;
				this.DMG = HeavyStats.DAMAGE;
			}
		}

		public void move(AIHexTile dest) {

			hex.occupant = null;
			dest.occupant = this;
			this.hex = dest;
		}

		public void attack(AIPlayer p) {

			p.HP -= (int)(this.DMG*((float)this.DMG/p.DEF));
			if(p.HP <=0) {
				p.hex = null;
				p.TM.team.Remove(p);
			}
		}

		public void capture(AIBase b) {

			bool isAlreadyYourBase;
			if(b.side != null)		isAlreadyYourBase = b.side == this.side;
			else 					isAlreadyYourBase = false;
			move(b);
			if (canCapture && !isAlreadyYourBase) {
				b.TM.bases.Remove(b);
				b.side = this.side;
				TM.bases.Add(b);
				b.hasBeenCaptured = true;
			}
		}

		public AIBase closestEnemyBase() {
			return closestBase(TM.enemy);
		}

		public AIBase closestNeutralBase() {
			return closestBase(null);
		}

		public AIBase closestBase(AITM tm) {

			AIBase closestBase;
			if(tm==null)
				closestBase = TM.map.getNeutral();
			else {
				int distance = 999999;
				closestBase = null;
				foreach(AIBase b in tm.bases) {
					//get distance to it from tile player is on
					int newDist = b.distanceFromBase(this.hex);
					if(newDist < distance) {
						distance = newDist;
						closestBase = b;
					}
				}
			}
			return closestBase;
		}

		public AIBase closestEnemyOrNeutralBase() {
			AIBase neutral = closestNeutralBase();
			AIBase enemy = closestEnemyBase();
			if(neutral == null && enemy == null) {
				return null;
			}
			else if(neutral == null && enemy!=null) {
				return enemy;
			}
			else if(enemy == null && neutral!= null) {
				return neutral;
			}
			else if(neutral.distanceFromBase(this.hex) < enemy.distanceFromBase(this.hex)) {
				return neutral;
			}
			else{
				return enemy;
			}
		}

		public AIPlayer closestEnemyPlayer(AITM enemy) {
			int distance = 999999;
			AIPlayer closest = null;
			foreach(AIPlayer ep in enemy.team) {
				int newDist = distanceToAnotherPlayer(ep);
				if(newDist < distance){
					distance = newDist;
					closest = ep;
				}
			}

			return closest;
		}

		public int distanceToAnotherPlayer(AIPlayer p) {
			return (int)Mathf.Sqrt((int)Mathf.Pow(p.hex.x - this.hex.x,2) + (int)Mathf.Pow(p.hex.y - this.hex.y,2));
		}
	}

	public class AITM {

		public AITM enemy;
		public List<AIPlayer> team = new List<AIPlayer>();
		public List<AIBase> bases = new List<AIBase>();
		public AIMap map;
		public int CREDITS;
		public string side;
		public int maxID;

		public AITM() {}

		public AITM(TeamManager tm, AIMap map) {

			this.map = map;
			this.CREDITS = tm.CREDITS;
			this.side = tm.parent.tag;

			int id=0;
			foreach(Player p in tm.team) {
				map.tiles[p.currentTileScript.x,p.currentTileScript.y].occupant = new AIPlayer(p, this);
				map.tiles[p.currentTileScript.x,p.currentTileScript.y].occupant.hex = map.tiles[p.currentTileScript.x,p.currentTileScript.y];
				this.team.Add(map.tiles[p.currentTileScript.x,p.currentTileScript.y].occupant);
				map.tiles[p.currentTileScript.x,p.currentTileScript.y].occupant.side = this.side;
				map.tiles[p.currentTileScript.x,p.currentTileScript.y].occupant.id = id;
				id++;
			}
			maxID=id;
			foreach(AIBase b in map.bases) {
				if(b.side == this.side) {
					bases.Add(b);
					b.TM = this;
				}
			}
		}

		public AITM(AITM tm, AIMap map) {
			this.maxID = tm.maxID;
			this.map = map;
			this.CREDITS = tm.CREDITS;
			this.side = tm.side;

			foreach(AIPlayer p in tm.team) {
				map.tiles[p.hex.x,p.hex.y].occupant = new AIPlayer(p, this);
				map.tiles[p.hex.x,p.hex.y].occupant.hex = map.tiles[p.hex.x,p.hex.y];
				team.Add(map.tiles[p.hex.x,p.hex.y].occupant);
				map.tiles[p.hex.x,p.hex.y].occupant.side = this.side;
				map.tiles[p.hex.x,p.hex.y].occupant.id = p.id;
			}
			foreach(AIBase b in map.bases) {
				if(b.side == side) {
					bases.Add(b);
					b.TM = this;
				}
			}
			enemy = tm.enemy;									//might be an issue, might need to completely copy enemy
		}

		public double totalHealth() {
			double total = 0;
			foreach(AIPlayer p in this.team){
				total += p.HP;
			}
			return total;
		}//method

		public double totalDamage() {
			double total = 0;
			foreach(AIPlayer p in this.team) {
				total += p.DMG;
			}
			return total;
		}

		public double totalDefense() {
			double total = 0;
			foreach(AIPlayer p in this.team) {
				total += p.DEF;
			}
			return total;
		}

		public double totalMinDistanceToDesiredBases(double weight) {
			double total = 0;
			foreach(AIPlayer p in this.team) {
				AIBase closest = p.closestEnemyOrNeutralBase();
				if(closest!=null){
					int dist = closest.distanceFromBase(p.hex);
					if(p.canCapture) {
						total += dist;
					}
					else {
						total += dist * weight;
					}
				}

			}

			return total;
		}

		public double totalMinDistanceToEnemy(double weight) {
			double total = 0;
			foreach(AIPlayer p in this.team) {
				AIPlayer closest = p.closestEnemyPlayer(enemy);
				if(closest!=null){
					int dist = closest.distanceToAnotherPlayer(p);
					if(p.canCapture) {
						total += dist * weight;
					}
					else{
						total += dist;
					}
				}
			}
			return total;
		}


		public bool creditsAreSufficient(string name) {
			if((name=="Soldier" || name=="BadSoldier") && CREDITS >= SoldierStats.COST) {
				CREDITS -= SoldierStats.COST;
				return true;
			}

			else if((name=="Aerial" || name=="BadAerial") && CREDITS >= AerialStats.COST) {
				CREDITS -= AerialStats.COST;
				return true;
			}
			else if((name=="Heavy" || name=="BadHeavy") && CREDITS >= HeavyStats.COST) {
				CREDITS -= AerialStats.COST;
				return true;
			}
			return false;
		}

		public void addCredits() {
			foreach(AIBase b in bases) {
				CREDITS += 100;
			}
		}
	}

	public class AIMap {

		public List<AIHexTile> tileList = new List<AIHexTile>();
		public AIHexTile[,] tiles;
		public List<AIBase> bases = new List<AIBase>();
		public int width;
		public int height;

		public AIMap() {}

		public AIMap(Map map) {
			width = map.realWidth;
			height = map.mapHeight;
			tiles = new AIHexTile[width,height];
			for(int i=0; i<width; i++) {
				for(int j=0; j<height;j++) {
					if(map.tiles[i,j] != null) {
						if(map.tiles[i,j].gameObject.tag=="Base") {
							tiles[i,j] = new AIBase(i, j);
							bases.Add((AIBase)tiles[i,j]);
						}						
						else {
							tiles[i,j] = new AIHexTile(i, j);
						}
						tiles[i,j].type = map.tiles[i,j].gameObject.tag;
						tileList.Add(tiles[i,j]);
					}
				}
			}
			createAdjacencies();
		}

		public AIMap(AIMap map) {
			this.width = map.width;
			this.height = map.height;
			tiles = new AIHexTile[width,height];
			for(int i=0; i<width; i++) {
				for(int j=0; j<height;j++) {
					if(map.tiles[i,j] != null) {
						if(map.tiles[i,j].type=="Base") {
							tiles[i,j] = new AIBase(i, j);
							bases.Add((AIBase)tiles[i,j]);
						}
						else {
							tiles[i,j] = new AIHexTile(i, j);
						}
						tiles[i,j].type = map.tiles[i,j].type;
						tileList.Add(tiles[i,j]);
					}
				}
			}
			createAdjacencies();
		}

		public void createAdjacencies() {
			foreach(AIHexTile ht in tileList) {
				int xp=ht.x+1;
				int xn=ht.x-1;
				int yp=ht.y+1;
				int yn=ht.y-1;

				/* CLOCKWISE STARTING AT TILE TO RIGHT */

				if(xp<width) {					// +1, 0
					if(tiles[xp,ht.y]!=null)	ht.neighbors.Add(tiles[xp,ht.y]);
				}
				if(yn>=0) {							//  0,-1
					if(tiles[ht.x,yn]!=null)	ht.neighbors.Add(tiles[ht.x,yn]);
				}
				if(xn>=0 && yn>=0) {				// -1,-1
					if(tiles[xn,yn]!=null)		ht.neighbors.Add(tiles[xn,yn]);
				}
				if(xn>=0) {							// -1, 0
					if(tiles[xn,ht.y]!=null)	ht.neighbors.Add(tiles[xn,ht.y]);
				}
				if(yp<height) {					//  0,+1
					if(tiles[ht.x,yp]!=null)	ht.neighbors.Add(tiles[ht.x,yp]);
				}
				if(xp<width && yp<height) {	// +1,+1
					if(tiles[xp,yp]!=null)		ht.neighbors.Add(tiles[xp,yp]);
				}
			}
		}

		public List<AIHexTile> legalMoves(AIPlayer p) {

			if(p==null)	{										/* NULL CHECK, NEED TO RETURN NO LEGAL TILES */
				return new List<AIHexTile>();
			}
			List<AIHexTile> legal = new List<AIHexTile>(p.hex.neighbors);
			List<AIHexTile> temp = new List<AIHexTile>();
			
			foreach(AIHexTile ht in p.hex.neighbors) {
				if((ht.type=="waterTile" && p.type!="Aerial" && p.type!="BadAerial") || ht.occupant != null)
					legal.Remove(ht);
			}
			//Debug.Log(legal.Count + " legal tiles for " + p.id);
			
			for(int i=1; i<p.MOB; i++) {
				foreach(AIHexTile h in legal) {
					foreach(AIHexTile ht in h.neighbors) {
						if((ht.occupant == null && !temp.Contains(ht) && !legal.Contains(ht)) && (ht.type != "waterTile" || p.type=="Aerial" || p.type=="BadAerial"))
							temp.Add (ht);
					}
				}
				foreach(AIHexTile ht in temp) {
					legal.Add(ht);
				}
				temp = new List<AIHexTile>();
			}

			return legal;
		}

		public List<AIPlayer> legalAttacks(AIPlayer p) {
			
			if(p==null)
				return new List<AIPlayer>();
			List<AIHexTile> inRange = new List<AIHexTile>(p.hex.neighbors);
			List<AIHexTile> temp = new List<AIHexTile>();
			List<AIPlayer> players = new List<AIPlayer>();
			
			for(int i=1; i<p.attackRange; i++) {
				foreach(AIHexTile h in inRange) {
					foreach(AIHexTile ht in h.neighbors) {
						if(!temp.Contains(ht) && !inRange.Contains(ht) && ht.occupant != null && ht.occupant.side != p.side)
							temp.Add (ht);
					}
				}
				foreach(AIHexTile hex in temp) {
					inRange.Add(hex);
					players.Add(hex.occupant);
				}
				temp = new List<AIHexTile>();
			}

			return players;
		}

		public AIBase getNeutral() {
			foreach(AIBase b in bases) {
				if(b.side == null)
					return b;
			}
			return null;
		}
	}
}
