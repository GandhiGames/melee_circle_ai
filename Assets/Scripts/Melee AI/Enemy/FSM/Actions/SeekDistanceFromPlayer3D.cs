using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	public class SeekDistanceFromPlayer3D : SeekDistanceFromPlayer
	{
		private Rigidbody rigidbody;

		public SeekDistanceFromPlayer3D (Transform character, float seekSpeed, float distanceFromPlayer, Rigidbody rigidbody)
				: base (character, seekSpeed, distanceFromPlayer)
		{
			this.rigidbody = rigidbody;
		}

		public override Command GetAction (Transform player)
		{
			if (OkToAct ()) {
				
				var newPos = Lenard_Jones (character.position, player.position, lenardJonesVector, distanceFromPlayer);
				
				return new Movement3DCommand (character, newPos.normalized * seekSpeed, rigidbody);
				
			}
			
			return null;
		}
	}
}
