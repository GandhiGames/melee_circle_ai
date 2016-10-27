using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	public abstract class SeekDistanceFromPlayer : FSMAction
	{
		protected Transform character;
		protected float seekSpeed;
		protected float distanceFromPlayer;

		public SeekDistanceFromPlayer (Transform character, float seekSpeed, float distanceFromPlayer)
		{
			this.character = character;
			this.seekSpeed = seekSpeed;
			this.distanceFromPlayer = distanceFromPlayer;

		}
			
		protected override bool OkToAct ()
		{
			return true; 
		}
		
		public override void Enter ()
		{

		}

		public override void Exit ()
		{

		}	
	}
}
