using System;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Helpers;
using FsmPathfinding;
using Pathfinding;
using System.Linq;

namespace HutongGames.PlayMaker.Pathfinding
{
	[ActionCategory("A Star")]
	[Tooltip("Set various properties of the path. Set properties does not work due to the wrappers.")]
	public class setPathInfo : FsmStateAction
	{
		[ActionSection("Input")]
		[ObjectType(typeof(FsmPath))]
		[Tooltip("Alternatively use a path directly. Overwrites everything else as a path, if set.")]
		public FsmObject InputPath;

		[ActionSection("Floats")]
		[Tooltip("The duration of this path in ms. ")]
		public FsmFloat duration;
		
		[Tooltip("Scale of the heuristic values. ")]
		public FsmFloat heuristicScale;
		
		[ActionSection("Integers")]
		[Tooltip("Which graph tags are traversable. ")]
		public FsmInt enabledTags;
		
		[Tooltip("Radius for the unit searching for the path. Not used by any built-in pathfinders. These common name variables are put here because it is a lot faster to access fields than, for example make use of a lookup table (e.g dictionary). Or having to cast to another path type for acess. ")]
		public FsmInt radius;
		
		[Tooltip("ID of this path. ")]
		public FsmInt pathID;
		
		[Tooltip("Number of nodes this path has searched. ")]
		public FsmInt searchedNodes;
		
		[Tooltip("ID of this path. ")]
		public FsmInt searchIterations;
		
		[Tooltip("Speed of the character. ")]
		public FsmInt speed;
		
		[Tooltip("Turning radius of the character. ")]
		public FsmInt turnRadius;
		
		
		[ActionSection("Bools")]
		[Tooltip("True if the path is currently recycled (i.e in the path pool). ")	]
		public FsmBool recycled;
		
		[ActionSection("NNConstraints")]
		[ObjectType(typeof(FsmNNConstraint))]
		[Tooltip("Constraint for how to search for nodes. ")]	
		public FsmObject nnConstraint;

		[ActionSection("Nodes...s")]
		[ObjectType(typeof(FsmNodes))]
		[Tooltip("Constraint for how to search for nodes. ")]	
		public FsmObject nodes;

		[ActionSection("NodeRunData")]
		[ObjectType(typeof(FsmNodeRunData))]
		[Tooltip("Constraint for how to search for nodes. ")]	
		public FsmObject runData;
		
		public FsmBool everyFrame;
		
		private FsmPath goo = new FsmPath();	

		public override void Reset() 
		{
			duration = new FsmFloat {UseVariable = true};
			heuristicScale = new FsmFloat {UseVariable = true};			
			enabledTags = new FsmInt {UseVariable = true};
			radius = new FsmInt {UseVariable = true};
			pathID = new FsmInt {UseVariable = true};
			searchedNodes = new FsmInt {UseVariable = true};
			searchIterations = new FsmInt {UseVariable = true};
			speed = new FsmInt {UseVariable = true};
			turnRadius = new FsmInt {UseVariable = true};
			recycled = new FsmBool{UseVariable = true};
			nnConstraint = new FsmObject{UseVariable = true};
			nodes = new FsmObject{UseVariable = true};
			runData = new FsmObject{UseVariable = true};			
		}
 
		public override void OnEnter() 
	  	{
			var moo = InputPath.Value as FsmPath;
			
			if(moo.Value == null) 
			{
				Finish(); 
				return;
			}
			
			DoStuff();
			
			if(!everyFrame.Value)
			{ Finish(); }			
		}
		
		public void DoStuff(){
			var doo = FsmConverter.GetPath(InputPath);
			
			doo.duration = duration.Value;
			doo.heuristicScale = heuristicScale.Value;			
			doo.enabledTags = enabledTags.Value;			
			doo.radius = radius.Value;
			doo.pathID = (ushort)pathID.Value;
			doo.searchedNodes = searchedNodes.Value;
			doo.searchIterations = searchIterations.Value;
			doo.speed = speed.Value;
			doo.turnRadius = turnRadius.Value;
			doo.recycled = recycled.Value;			
			nnConstraint.Value = FsmConverter.SetNNConstraint(doo.nnConstraint);
			nodes.Value = FsmConverter.SetNodes(doo.path);
			runData.Value = FsmConverter.SetNodeRunData(doo.runData);			
		}
	 
		public override void OnUpdate() 
	  	{ DoStuff(); }
   	}
}