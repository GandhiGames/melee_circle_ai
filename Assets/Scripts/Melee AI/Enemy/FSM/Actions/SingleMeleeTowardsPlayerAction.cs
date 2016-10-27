using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	public class SingleMeleeTowardsPlayerAction : FSMAction, ILimitedAttack
	{

		public bool AttackComplete { get { return attacked; } }
		private Transform character;
		private bool attacked;
			
		private MeleeAttack attack;

		public SingleMeleeTowardsPlayerAction (Transform character)
		{
			this.character = character;
			attack = character.GetComponent<MeleeAttack> ();
			
		}

		public override Command GetAction (Transform player)
		{
			if (OkToAct ()) {
				attacked = true;
								
				var heading = player.position - character.position;
				var distance = heading.magnitude;
				var direction = heading / distance; 

				return new ImmediateMeleeCommand (character, direction, attack);
			}
			
			return null;
		}
		
		protected override bool OkToAct ()
		{
			return !attacked;
		}
			
		public override void Enter ()
		{
			attacked = false;
		}

		public override void Exit ()
		{
		
		}
	}
}
