using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MeleeAI
{
	public class State : FSMState
	{

		private List<FSMAction> actions;
		private List<FSMReason> reasons;
		private int currentAction = 0;

		public State (FSMStateID stateid, FSMAction action, List<FSMReason> reasons)
		{
			var actionList = new List<FSMAction> ();
			actionList.Add (action);
			
			this.stateID = stateid;
			this.actions = actionList;
			this.reasons = reasons;
			
			foreach (var reason in reasons) {
				AddTransition (reason.Transition, reason.GoToState);
			}
						
		}
				
		public State (FSMStateID stateid, List<FSMAction> actions, List<FSMReason> reasons)
		{
			this.stateID = stateid;
			this.actions = actions;
			this.reasons = reasons;
			
			foreach (var reason in reasons) {
				AddTransition (reason.Transition, reason.GoToState);
			}
			
		}

		public override void Enter ()
		{
			currentAction = 0;

			foreach (var action in actions) {
				action.Enter ();
			}

			foreach (var reason in reasons) {
				reason.Enter ();
			}
						
		}

		public override void Exit ()
		{
			foreach (var action in actions) {
				action.Exit ();
			}

			foreach (var reason in reasons) {
				reason.Exit ();
			}
		}

		public override void Reason (Transform player)
		{
			foreach (var reason in reasons) {
				if (reason.ChangeState (player)) {
					break;
				}
			}
		}

		public override void Act (Transform player)
		{
			foreach(var action in actions){
				var command = action.GetAction (player);

				if (command != null) {
					command.Execute ();
				}
			}
					
			/*var action = actions [currentAction];
			currentAction = (currentAction + 1) % actions.Count;
					
			var command = action.GetAction (player);

			if (command != null) {
				command.Execute ();
			}*/
						
						
		}

		protected override bool OkToAct ()
		{
			return true;
		}

	}
}
