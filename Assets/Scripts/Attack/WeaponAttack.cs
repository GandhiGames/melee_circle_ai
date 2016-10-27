using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	/// <summary>
	/// Provides character the ability to attack using a weapon (currently melee and projectile).
	/// Extended by MeleeAttack and ProjectileAttack.
	/// </summary>
	public abstract class WeaponAttack : MonoBehaviour
	{

		// Attack speed of current weapon.
		protected float attackSpeed;

		// Limits characters attack speed;
		protected float attackTime = 0f;

		/// <summary>
		/// Request attack.
		/// </summary>
		public abstract void Execute (Vector2 dir);
	}
}
