using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	/// <summary>
	/// Abstract base class for any weapons in the game (sublasses are to be attached directly to weapons/projectiles).
	/// Provides access to weapons capabilities, such as damage, range, and force. Provides helper methods used in subclasses.
	/// </summary>
	public abstract class Weapon : MonoBehaviour
	{
		public int Damage = 5;

		// The push back force applied with the weapon hits another entity.
		public int DamageForce = 200;

		// The range of the weapons attack.
		public float Range = 0.5f;
			
		// How quickly the character can attack with the weapon.
		public float AttackSpeed = 0.6f;

		// Any entity with the same tag as this prefab will be damaged by the weapon.
		public Transform PrefabWithDamageTag;


		/// <summary>
		/// Returns true if prefab present and prefabs tag is set. Checked during initialisation of sub-classes.
		/// </summary>
		protected bool IsPrefabInitialised ()
		{
			if (!PrefabWithDamageTag || PrefabWithDamageTag.transform.tag.Equals ("")) {
				return false;
			}

			return true;
		}
				


		/// <summary>
		/// Returns true if other tag is equal to the prefabs tag or a neutral tag. 
		/// The neutral tag is used when an entity can be damaged by anyone.
		/// </summary>
		protected bool IsDamageTag (Collider2D other)
		{
			return other.tag == PrefabWithDamageTag.transform.tag;
		}



		
	}
}
