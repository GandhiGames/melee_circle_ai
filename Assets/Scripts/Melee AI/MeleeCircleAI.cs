using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MeleeAI
{
	public class MeleeCircleAI : MonoBehaviour
	{
        /// <summary>
        /// Maximum simultaneous attackers.
        /// </summary>
        public int MaxAttackers = 2;

        /// <summary>
        /// Base delay between attack requests.
        /// </summary>
		public float AttackRate = 2;

        /// <summary>
        /// Maximum fluctuation added to AttackRate: provides non-uniform requests to attack.
        /// </summary>
		public float AttackRateFluctuationMax = 1f;

		private int attackers;

		void Start ()
		{
			attackers = 0;
		}

        /// <summary>
        /// Mark attack as complete. Decrements attack counter, allowing another agent to receive
        /// permission to attack.
        /// </summary>
		public void CompletedAttack ()
		{
			attackers--;

            if(attackers < 0)
            {
                attackers = 0;
                Debug.LogWarning("More attacks marked as completed than were initiated.");
            }
		}

        /// <summary>
        /// Returns true if attacker has permission to begin attacking.
        /// Increments attack counter.
        /// </summary>
        /// <returns>true if permission to begin attacking.</returns>
		public bool HavePermissionToAttack ()
		{
			if(attackers >= MaxAttackers){
				return false;
			}

			attackers++;

			return true;
		}

        /// <summary>
        /// Returns random attacktime with offset defined by AttackRateFluctuationMax.
        /// </summary>
        /// <returns>Wait time until should request next attack.</returns>
        public float GetNextAttackWaitTime ()
		{
			var fluc = Random.Range (-AttackRateFluctuationMax, AttackRateFluctuationMax);

			return (AttackRate + fluc);
		}
	}
}
