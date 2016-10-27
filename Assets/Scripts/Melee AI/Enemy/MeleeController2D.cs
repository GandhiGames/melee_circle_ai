using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MeleeAI
{
	/// <summary>
	/// Attach to any character who you wish to have the following behaviour:
	///		Follows a path to players location (when not near player)
	///		Digs (when an obstacle is in the way)
	///		Seeks and attacks player (when close to player)
	///		Flees from player (when near death)
	///		Is in an idle state (when successfully tun from player)
	///
	/// This state machine creates a melee character, who hunts down the player from anywhere on the map.
	/// </summary>
	[RequireComponent (typeof(MeleeAttack))]
	[RequireComponent (typeof(Collider2D))]
	[RequireComponent (typeof(Rigidbody2D))]
	public class MeleeController2D : NPC
	{				
		public LayerMask ObstacleMask;

		/// <summary>
		/// Construct a state mahcine to provide the behaviour outlined in the class comment.
		/// Individual states are created and transitions added to them. These transitions are
		/// used in the states Reason method.
		/// Once all states and transitions have been created the states are added to the state machine,
		/// which will handle transitions.
		/// </summary>
		protected override void ConstructFSM ()
		{
			var outsideCircleRange = Settings.instance.OutsideCircleRange;
			var insideCircleRange = Settings.instance.InsideCircleRange;
			var seperationRange = Settings.instance.SeperationRange;
						
			// Move into danger zone
			var seekDangerZoneActions = new List<FSMAction> ();
			seekDangerZoneActions.Add (new SeekDistanceFromPlayer2D (transform, SeekSpeed, outsideCircleRange, GetComponent<Rigidbody2D>()));
			seekDangerZoneActions.Add (new AvoidNPCAction (transform, seperationRange, transform.tag));
			var dangerReasons = new List<FSMReason> ();
			dangerReasons.Add (new PermissionToAttackReason (transform, FSMStateID.BattleCircleAI_SeekToAttackZone));
			stateMachine.AddState (new State (FSMStateID.BattleCircleAI_SeekToDangerZone, seekDangerZoneActions, dangerReasons));
						
			// Avoid other characters and move into attack range.
			var seekAttackZoneActions = new List<FSMAction> ();
			seekAttackZoneActions.Add (new AvoidNPCAction (transform, seperationRange, transform.tag));
			seekAttackZoneActions.Add (new SeekDistanceFromPlayer2D (transform, SeekSpeed, insideCircleRange, GetComponent<Rigidbody2D>()));
			seekAttackZoneActions.Add (new InformMeleeAIOnExitAction (transform));
			var seekAttackZoneReasons = new List<FSMReason> ();
			seekAttackZoneReasons.Add (new NoClearPathToPlayerReason (transform, ObstacleMask, GetComponent<Collider2D>(), FSMStateID.BattleCircleAI_SeekToDangerZone));
			seekAttackZoneReasons.Add (new PlayerInRangeReason (transform, insideCircleRange, FSMStateID.MeleeAttack));
			stateMachine.AddState (new State (FSMStateID.BattleCircleAI_SeekToAttackZone, seekAttackZoneActions, seekAttackZoneReasons));
						
			var meleeAttackActions = new List<FSMAction> ();
			var singleAttack = new SingleMeleeTowardsPlayerAction (transform);
			meleeAttackActions.Add (singleAttack);
			seekAttackZoneActions.Add (new InformMeleeAIOnExitAction (transform));
			var meleeAttackReasons = new List<FSMReason> ();
			meleeAttackReasons.Add (new AttackCompleteReason (transform, singleAttack, 
													FSMStateID.BattleCircleAI_SeekToDangerZone));
			stateMachine.AddState (new State (FSMStateID.MeleeAttack, meleeAttackActions, meleeAttackReasons)); 


		}

	}
}
