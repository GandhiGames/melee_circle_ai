using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	public class Movement3DCommand : Command
	{

			
		private Rigidbody rigidbody;
			
		public Movement3DCommand (Transform character, Vector2 dir, Rigidbody rigidbody) : base (character, dir)
		{
			this.rigidbody = rigidbody;
		}
			
		public override void Execute ()
		{
			rigidbody.AddForce (dir);
		}
			

	}
}
