using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	public class NoClearPathToPlayerReason : FSMReason
	{
		private static readonly string SCRIPT_NAME = typeof(NoClearPathToPlayerReason).Name;
	
		private NPC controller;
		private Transform character;
		private LayerMask obstacleMask;
		private Vector2 colliderSize;

	
		public NoClearPathToPlayerReason (Transform character, LayerMask obstacleMask, Collider2D collider, FSMStateID goToState)
		{
			Transition = FSMTransistion.SawPlayer;
			GoToState = goToState;
		
			this.character = character;
			this.obstacleMask = obstacleMask;
			controller = character.GetComponent<NPC> ();

			var scaleX = character.localScale.x;
			var scaleY = character.localScale.x;

			if (collider is BoxCollider2D) {
				var col = (BoxCollider2D)collider;
				colliderSize = new Vector2 (col.size.x * scaleX, col.size.y * scaleY);
			} else if (collider is CircleCollider2D) {
				var col = (CircleCollider2D)collider;
				colliderSize = new Vector2 (col.radius * scaleX, col.radius * scaleY);
			}

		}

		public override bool ChangeState (Transform player)
		{
			var heading = player.position - character.position;
			var distance = heading.magnitude;
			var dir = heading / distance;

			Vector2 pos = new Vector2 (character.position.x + (dir.x * (colliderSize.x * 0.7f)), 
			                           character.position.y + (dir.y * (colliderSize.y * 0.5f)));

			if (LayerInPath (player.position, pos, obstacleMask)) {
				Debug.Log (SCRIPT_NAME + ": switching state to: " + GoToState);
				controller.SetTransistion (Transition);
				return true;
			}
			
			return false;
		}
	

	}
}
