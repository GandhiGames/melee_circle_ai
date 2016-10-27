using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	public class SeekDistanceFromPlayer2D : SeekDistanceFromPlayer
	{

		private Rigidbody2D rigidbody;
		
		public SeekDistanceFromPlayer2D (Transform character, float seekSpeed, float distanceFromPlayer, Rigidbody2D rigidbody)
			: base (character, seekSpeed, distanceFromPlayer)
		{
			this.rigidbody = rigidbody;
		}
		
		public override Command GetAction (Transform player)
		{
			if (OkToAct ()) {
				
				Vector2 newPos = //Lenard_Jones (character.position, player.position, lenardJonesVector, distanceFromPlayer);

						(character.position - player.position).normalized * distanceFromPlayer + player.position;

				if (Vector2.Distance (newPos, character.position) > 0.1f) {
					return new Movement2DCommand (character, ((Vector3)newPos - character.position).normalized * seekSpeed, rigidbody);
				}

				
			}
			
			return null;
		}
	}
}
