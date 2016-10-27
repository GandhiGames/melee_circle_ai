using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	/// <summary>
	/// Perform melee attack
	/// </summary>
	public class MeleeCommand : Command
	{
				
		private MeleeAttack attack;

		public MeleeCommand (Transform character, Vector2 dir, MeleeAttack attack) : base (character, dir)
		{
			this.attack = attack;
		}
			

		public override void Execute ()
		{
			attack.Execute (dir);
		}

	}
}
