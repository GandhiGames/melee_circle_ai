using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	public class InformMeleeAIOnExitAction : FSMAction
	{
		private Transform character;
		private MeleeCircleAI meleeAI;

		public InformMeleeAIOnExitAction (Transform character)
		{
			this.character = character;
		}

		public override Command GetAction (Transform player)
		{
			if (!meleeAI) {
				meleeAI = player.GetComponent<MeleeCircleAI> ();
			}
			
			return null;
		}

		protected override bool OkToAct ()
		{
			return true;
		}

		public override void Enter ()
		{

		}
		
		public override void Exit ()
		{
			if (!meleeAI) {
				var player = GameObject.FindGameObjectWithTag ("Player");
			
				if (player) {
					meleeAI = player.GetComponent<MeleeCircleAI> ();
				}
			}
			
			meleeAI.CompletedAttack ();
		}
	}
}
