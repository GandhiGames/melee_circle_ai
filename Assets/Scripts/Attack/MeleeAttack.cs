using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MeleeAI
{
	/// <summary>
	/// Behaviour specific to attacking with a melee weapon. 
	/// </summary>
	public class MeleeAttack : WeaponAttack
	{
		// Weapon prefab. Set in editor.
		public GameObject WeaponPrefab;

		private static readonly string SCRIPT_NAME = typeof(MeleeAttack).Name;
		// Instantiated weapon.
		private GameObject melee;

		private Vector2? heading;
		private float range;
		private Animator animator;

		/// <summary>
		/// Initialises attack speed (in base class) and attack range of melee weapon. 
		/// </summary>
		void Start ()
		{
			InitWeapon ();
			var meleeDetails = WeaponPrefab.transform.GetComponent<MeleeWeapon> ();
			
			if (meleeDetails) {
				range = meleeDetails.Range;
			}

			animator = GetComponent<Animator> ();
		}

		/// <summary>
		/// Checks if WeaponPrefab is present and sets attack speed else raises error.
		/// </summary>
		protected void InitWeapon ()
		{
			if (!WeaponPrefab) {
				Debug.LogError (SCRIPT_NAME + ": weapon prefab not set");
			} else {
				var weaponDetails = WeaponPrefab.transform.GetComponent<Weapon> ();
				
				if (!weaponDetails) {
					Debug.LogError (SCRIPT_NAME + ": weapon prefab does not have weapon script");
				} else {
					attackSpeed = weaponDetails.AttackSpeed;
				}
			}
		}
		
		/// <summary>
		/// Updates attacktime and ensures any instantiated melee objects position stays relative to the controlling character.
		/// </summary>
		void Update ()
		{
			attackTime += Time.deltaTime;

			if (melee && heading.HasValue) {
				melee.transform.position = (Vector2)transform.position + heading.Value;
				melee.transform.transform.up = heading.Value;
			}
		}

		/// <summary>
		/// Instantiates melee weapon using object pool based on weapon prefab in base class. 
		/// Weapons range is limited based on the melee weapons range.
		/// Weapons attack ainimation started. It is the animations responsibility to remove the weapon when attack has finished.
		/// </summary>
		private void Instantiate (Vector2 dir)
		{
			dir.Normalize ();
			melee = ObjectPool.instance.GetObjectForType (WeaponPrefab.name, false);
			melee.transform.position = transform.position;
		
				
			//Limit the melee weapons range
			heading = new Vector2 (Mathf.Clamp (dir.x, -range, range), Mathf.Clamp (dir.y, -range, range));

			melee.SetActive (true);

		}

		/// <summary>
		/// Request attack. Called when player presses attack button or by enemy AI. 
		/// </summary>
		public override void Execute (Vector2 dir)
		{
			if (dir != Vector2.zero && attackTime >= attackSpeed) {
				attackTime = 0f;
				animator.SetTrigger ("Attacked");
				Instantiate (dir);

			}
		}

		public void ImmediateExecute (Vector2 dir)
		{
			animator.SetTrigger ("Attacked");
			Instantiate (dir);
		}
	}
}
