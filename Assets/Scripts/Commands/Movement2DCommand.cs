using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	/// <summary>
	/// Movement command.
	/// </summary>
	public class Movement2DCommand : Command
	{				
		private Rigidbody2D rigidbody2D;

		public Movement2DCommand (Transform character, Vector2 dir, Rigidbody2D rigidbody) : base (character, dir)
		{
			rigidbody2D = rigidbody;
		}

		public override void Execute ()
		{
			rigidbody2D.AddForce (dir);
		}

	}
}
