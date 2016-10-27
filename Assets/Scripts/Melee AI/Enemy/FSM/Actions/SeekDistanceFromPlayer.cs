using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	public abstract class SeekDistanceFromPlayer : FSMAction
	{
		
		protected Transform character;
		protected float seekSpeed;
		protected float distanceFromPlayer;
				

		protected Vector4 lenardJonesVector = new Vector4 (2, 2, 1, 2); 

			
		public SeekDistanceFromPlayer (Transform character, float seekSpeed, float distanceFromPlayer)
		{
			this.character = character;
			this.seekSpeed = seekSpeed;
			this.distanceFromPlayer = distanceFromPlayer;

		}

			
		protected Vector2 Lenard_Jones (Vector2 pos, Vector2 target, Vector4 lenard_jones, float s)
		{
			var r = pos - target;
			var u = r.normalized;
			
			var A = lenard_jones.x;
			var B = lenard_jones.y;
			var n = lenard_jones.z;
			var m = lenard_jones.w;
			
			var d = r.magnitude / s;
			var U = -A / Mathf.Pow (d, n) + B / Mathf.Pow (d, m);

			return new Vector2 (u.x * U, u.y * U);
		}
		
		protected override bool OkToAct ()
		{
			return true; // Vector2.Distance (character.position, player.position) >= distanceFromPlayer;
		}
		
		public override void Enter ()
		{

		}

		public override void Exit ()
		{

		}	
	}
}
