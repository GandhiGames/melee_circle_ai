using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MeleeAI
{
	public class MeleeCircleAI : MonoBehaviour
	{				
		private int currentRequestCount = 0;
		private int previousRequestCount = 0;
		private float requestTime = 0f;
		private List<Transform> attackers;

		void Start ()
		{
			attackers = new List<Transform> ();
		}
				
		public float WaitTime {
			get {
				if (previousRequestCount < Settings.instance.RequestThresholdPerSec) {
					return 0f;
				}
								
				return GetNextAttackWaitTime ();
			}
		}
				
		void Update ()
		{
			requestTime += Time.deltaTime;
		}


		public bool HavePermissionToAttack (Transform attacker)
		{
			if (requestTime <= 1f) {
				currentRequestCount++;
			} else {
				previousRequestCount = currentRequestCount;
				requestTime = 0f;
			}

			if (attackers.Contains (attacker)) {
				return false;
			}

			if (attackers.Count >= Settings.instance.MaxAttackers) {
				return false;
			}

			attackers.Add (attacker);

			return true;

		}
				
		private float GetNextAttackWaitTime ()
		{
			var fluc = Random.Range (0, Settings.instance.AttackRateFluctuationMax);
						
			if ((int)Random.Range (0, 2) == 0) {
				return (Settings.instance.AttackRate - fluc);
			} else {
				return (Settings.instance.AttackRate + fluc);
			}
		}
			
		public void CompletedAttack (Transform character)
		{
			attackers.Remove (character);
		}
	}
}
