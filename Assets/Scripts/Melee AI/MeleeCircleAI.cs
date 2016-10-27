using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MeleeAI
{
	public class MeleeCircleAI : MonoBehaviour
	{				
		public int MaxAttackers = 2;
		public float AttackRate = 2;
		public float AttackRateFluctuationMax = 1f;

		private int attackers;


		void Start ()
		{
			attackers = 0;
		}


		public void CompletedAttack ()
		{
			attackers--;
		}

		public bool HavePermissionToAttack ()
		{
			if(attackers >= MaxAttackers){
				return false;
			}

			attackers++;

			return true;
		}
				
		public float GetNextAttackWaitTime ()
		{
			var fluc = Random.Range (-AttackRateFluctuationMax, AttackRateFluctuationMax);

			return (AttackRate + fluc);
		}
			
	
	}
}
