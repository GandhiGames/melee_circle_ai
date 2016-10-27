using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	public class Settings : MonoBehaviour
	{

		private static Settings _instance;
		public static Settings instance {
			get {
				if (!_instance) {
					_instance = FindObjectOfType<Settings> ();
				}

				return _instance;
			}
		}

		public int MaxAttackers = 2;
		public float AttackRate = 2;
		public float AttackRateFluctuationMax = 1f;
		public int RequestThresholdPerSec = 2;

		public float OutsideCircleRange = 5f;
		public float InsideCircleRange = 1.5f;

		public float SeperationRange = 2.5f;



	}
}
