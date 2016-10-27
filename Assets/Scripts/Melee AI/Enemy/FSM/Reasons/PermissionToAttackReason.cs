using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	public class PermissionToAttackReason : FSMReason
	{
		
		private NPC controller;
		private Transform character;
		private MeleeCircleAI circleAI;
		private float currentWaitTime = 0f;
		private float nextWaitTime;
				
		private static readonly string SCRIPT_NAME = typeof(PermissionToAttackReason).Name;
		
		public PermissionToAttackReason (Transform character, FSMStateID goToState)
		{
			Transition = FSMTransistion.PermissionToAttackGranted;
			GoToState = goToState;
			this.character = character;
						
			controller = character.GetComponent<NPC> ();
						
		}

		public override bool ChangeState (Transform player)
		{
				
			currentWaitTime += Time.deltaTime;
				
			if (!circleAI) {
				circleAI = player.GetComponent<MeleeCircleAI> ();
			}
				
			if (OkToAct () && circleAI.HavePermissionToAttack (character)) { 
				Debug.Log (SCRIPT_NAME + ": switching state to: " + GoToState);
				controller.SetTransistion (Transition);
				return true;
			}
			
			return false;
		}

		public override void Enter ()
		{
			currentWaitTime = 0f;
						
			if (circleAI) {
				nextWaitTime = circleAI.WaitTime;
			} else {
				nextWaitTime = 0f;
			}
						
		}
				
		private bool OkToAct ()
		{
				
			if (currentWaitTime >= nextWaitTime) {						
				nextWaitTime = circleAI.WaitTime;
				return true;
			}
			 
			return false;
		}

	}
}
