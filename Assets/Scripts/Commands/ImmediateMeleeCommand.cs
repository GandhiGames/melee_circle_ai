using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	public class ImmediateMeleeCommand : Command
	{

		private MeleeAttack attack;
		
		public ImmediateMeleeCommand (Transform character, Vector2 dir, MeleeAttack attack) : base (character, dir)
		{
			this.attack = attack;
		}
		
		
		public override void Execute ()
		{
			attack.ImmediateExecute (dir);
		}
	}
}
