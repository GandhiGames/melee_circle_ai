using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MeleeAI
{
	[RequireComponent (typeof(Collider))]
	[RequireComponent (typeof(FSM))]
	[RequireComponent (typeof(Rigidbody))]
	public class MeleeController3D : NPC
	{
		public AnimationClip[] AttackAnimations;

		public AnimationClip WalkAnimation;
		public AnimationClip FleeAnimation;

		public AnimationClip IdleAnimation;


		protected override void ConstructFSM ()
		{

			GetComponent<Animation>().Play (WalkAnimation.name);

			var outsideCircleRange = Settings.instance.OutsideCircleRange;
			var insideCircleRange = Settings.instance.InsideCircleRange;
			var seperationRange = Settings.instance.SeperationRange;
			
			// Move into danger zone
			var seekDangerZoneActions = new List<FSMAction> ();
			seekDangerZoneActions.Add (new SeekDistanceFromPlayer3D (transform, SeekSpeed, outsideCircleRange, GetComponent<Rigidbody>()));
			seekDangerZoneActions.Add (new AvoidNPCAction (transform, seperationRange, transform.tag));
			var dangerReasons = new List<FSMReason> ();
			dangerReasons.Add (new PermissionToAttackReason (transform, FSMStateID.BattleCircleAI_SeekToAttackZone));
			stateMachine.AddState (new State (FSMStateID.BattleCircleAI_SeekToDangerZone, seekDangerZoneActions, dangerReasons));

			// Avoid other characters and move into attack range.
			var seekAttackZoneActions = new List<FSMAction> ();
			seekAttackZoneActions.Add (new AvoidNPCAction (transform, 0.1f, transform.tag));
			seekAttackZoneActions.Add (new SeekDistanceFromPlayer3D (transform, SeekSpeed, insideCircleRange, GetComponent<Rigidbody>()));
			seekAttackZoneActions.Add (new InformMeleeAIOnExitAction (transform));
			var seekAttackZoneReasons = new List<FSMReason> ();
			//seekAttackZoneReasons.Add (new NoClearPathToPlayerReason (transform, ObstacleMask, collider2D, FSMStateID.BattleCircleAI_SeekToDangerZone));
			seekAttackZoneReasons.Add (new PlayerInRangeReason (transform, insideCircleRange, FSMStateID.MeleeAttack));
			stateMachine.AddState (new State (FSMStateID.BattleCircleAI_SeekToAttackZone, seekAttackZoneActions, seekAttackZoneReasons));
			
		}

	}
}
