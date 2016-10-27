using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	/// <summary>
	/// Base abstract command. All commands (e.g. movement, attack) should inherit form this class.
	/// By recording a characters commands they can be played in reverse (i.e. rewound).
	/// </summary>
	public abstract class Command
	{
		protected Transform character;
		protected Vector2 dir;
		public Command (Transform character, Vector2 dir = default(Vector2))
		{
			this.character = character;
			this.dir = dir;
		}
		public abstract void Execute ();

	}
}
