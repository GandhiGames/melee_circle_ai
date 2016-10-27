using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	/// <summary>
	/// Requires on the states action to be of type SingleMeleeAction or SingleProjectileAction
	/// </summary>
	public class AttackCompleteReason : FSMReason
	{

		private NPC controller;
		private ILimitedAttack attack;

		
		private static readonly string SCRIPT_NAME = typeof(AttackCompleteReason).Name;

		public AttackCompleteReason (Transform character, ILimitedAttack attack, FSMStateID goToState)
		{
			Transition = FSMTransistion.AttackComplete;
			GoToState = goToState;
				
			this.attack = attack;
						
			controller = character.GetComponent<NPC> ();
			
		}

		public override bool ChangeState (Transform player)
		{
			if (attack.AttackComplete) {
				Debug.Log (SCRIPT_NAME + ": switching state to: " + GoToState);
								
				controller.SetTransistion (Transition);
				return true;
			}
			
			return false;
		}

		public override void Exit ()
		{

		}

				
				
	}
}
