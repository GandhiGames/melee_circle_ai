using UnityEngine;
using System.Collections;

namespace MeleeAI
{

	/// <summary>
	/// Attach to any melee weapon in the game. 
	/// Provides commands for when the base weapon collides with other objects. 
	/// </summary>
	[RequireComponent(typeof(AudioSource))]
	public class MeleeWeapon : Weapon
	{

		public AudioClip[] SwingSounds;
		public AudioClip[] HitSounds;
		public Transform ImpactAnimation;
			
		private static readonly string SCRIPT_NAME = typeof(MeleeWeapon).Name;

		private AudioSource audioSource;

		/// <summary>
		/// Checks prefab is correctly initialised (not null and with a tag name). If not alerts user.
		/// </summary>
		void Awake ()
		{
			if (!IsPrefabInitialised ()) {
				Debug.LogError (SCRIPT_NAME + ": damage prefab not set or damage prefab tag not set. " +
					"Damage will be applied to any object with same tag so make sure it is set correctly");
			}

			audioSource = GetComponent<AudioSource> ();
		}

		void OnEnable ()
		{						
			if (SwingSounds != null && SwingSounds.Length > 0) {
				audioSource.PlayOneShot (SwingSounds [(int)Random.Range (0, SwingSounds.Length)]);
			}
		}

		/// <summary>
		/// If collide with wall
		/// 	get BlockController script and apply damage based on DamageToCLock variable
		/// else if collide with an object with a tag to damage (either neutral or the tag belonging to the prefab)
		/// 	get the objects health script and apply damage based on Damage variable
		/// else if collide with projectile
		/// 	reverse the projectiles trajectory
		/// </summary>
		void OnTriggerEnter2D (Collider2D other)
		{
			if (IsDamageTag (other)) {
		
				if (HitSounds != null && HitSounds.Length > 0) {
					audioSource.PlayOneShot (HitSounds [(int)Random.Range (0, HitSounds.Length)]);
				}

				var heading = other.transform.position - transform.position;
				var distance = heading.magnitude;

				var dir = heading / distance;

				other.GetComponent<Rigidbody2D>().AddForce (dir * DamageForce);


				//play sound
				// Play animation
				if (ImpactAnimation) {
					PlayHitAnimation (other, dir);
				}
				
				ApplyDamage (other);
		
			} 

		}
		
		private void ApplyDamage (Collider2D other)
		{
			var health = other.GetComponent<EnemyHealth> ();
				
			if (health) {
				health.ApplyDamage (Damage);
			}
		}

		private void PlayHitAnimation (Collider2D other, Vector2 dir)
		{
			var splatter = ObjectPool.instance.GetObjectForType (ImpactAnimation.name, false);
			splatter.transform.position = transform.position;
			
			var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			
			splatter.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
			
			splatter.SetActive (true);

		}

		/// <summary>
		/// Adds weapon to pool (removing it from the scene). This can be called by an animations or by the weapons script.
		/// </summary>
		protected void Destroy ()
		{
			ObjectPool.instance.PoolObject (this.gameObject);
		}
			
			


	}
}
