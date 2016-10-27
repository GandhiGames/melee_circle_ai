using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	public class PlayerInRangeReason : FSMReason
	{
		private Transform character;
		private NPC controller;
		private float range;

		private static readonly string SCRIPT_NAME = typeof(PlayerInRangeReason).Name;

		public PlayerInRangeReason (Transform character, float range, FSMStateID goToState)
		{
			Transition = FSMTransistion.ReachedObject;
			GoToState = goToState;

			this.character = character;
			this.range = range;

			controller = character.GetComponent<NPC> ();

		}

		public override bool ChangeState (Transform player)
		{
			if (CloseToObject (player.position, character.position, range)) { // Player in attack range.
				Debug.Log (SCRIPT_NAME + ": switching state to: " + GoToState);
				controller.SetTransistion (Transition);
				return true;
			}

			return false;
		}
	}
}
