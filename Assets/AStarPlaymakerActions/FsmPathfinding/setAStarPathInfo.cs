using System;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Helpers;
using FsmPathfinding;
using Pathfinding;
using System.Linq;

namespace HutongGames.PlayMaker.Pathfinding
{
	[ActionCategory("A Star")]
	[Tooltip("few very technical things to set. Do not play with this unless you know what you're doing.")]
	public class setAStarPathInfo : FsmStateAction
	{
		[ActionSection("Input")]
		[ObjectType(typeof(FsmAstarPath))]
		[Tooltip("Leave this empty if you're using just one graph or if the graph is your active graph")]
		public FsmObject astarPath;
		
		[ActionSection("Astar Data")]
		[ObjectType(typeof(FsmAstarData))]
		[Tooltip("AstarData saves a lot of low level stuff, just like AstarPath. It also exists per graph.")]
		public FsmObject astarData;
		
		[ActionSection("Booleans")]
		[Tooltip ("Set true when scanning is being done.")]
		public FsmBool isScanning;
		
		[Tooltip ("Shows or hides graph inspectors. ")]
		public FsmBool showGraphs;
		
		[Tooltip ("The last area index which was used. ")]
		public FsmInt lastUniqueAreaIndex;
		
		[ActionSection("graphs...s")]
		[ObjectType(typeof(FsmNavGraphs))]
		[Tooltip ("All graphs this instance holds.This will be filled only after deserialization has completed. Set it at your own peril :D")]	
		public FsmObject graphs;	
		
		public FsmBool everyFrame;
		
		private AstarPath astarp;
		
		public override void Reset(){
		
			astarPath = new FsmObject { UseVariable = true };
			astarData = new FsmObject { UseVariable = true };
			isScanning = new FsmBool { UseVariable = true };
			showGraphs = new FsmBool { UseVariable = true };
			lastUniqueAreaIndex = new FsmInt { UseVariable = true };
			graphs = new FsmObject { UseVariable = true };		
		}
		
		public override void OnEnter() 
	  	{			
			DoStuff();
			
			if(!everyFrame.Value)
			{Finish();}
		}
		
		public void DoStuff()
		{
			if (!astarPath.IsNone && astarPath.Value != null)
			{ astarp = FsmConverter.GetAstarPath(astarPath); }
			else
			{ astarp = AstarPath.active; }
			
			if(!isScanning.IsNone)
			{ astarp.isScanning = isScanning.Value;	}		
			
			if(!showGraphs.IsNone)
			{ astarp.showGraphs = showGraphs.Value; }
			
			if(!lastUniqueAreaIndex.IsNone)
			{ astarp.lastUniqueAreaIndex = lastUniqueAreaIndex.Value; }	

			if (!astarData.IsNone)
			{ astarp.graphs = FsmConverter.GetNavGraphs(astarData.Value); }
			
			return;
		}
	  
		public override void OnUpdate() 
	  	{
			DoStuff();
		}
   	}
}