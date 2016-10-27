using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	public class EnemyHealth : MonoBehaviour
	{
		public int Health = 10;
		private int currentHealth;
		public GameObject[] OnDeadSprites;
		public GameObject OnDeadAnimation;
		
		// Use this for initialization
		void OnEnable ()
		{
			currentHealth = Health;
		}
	
		public void ApplyDamage (int damage)
		{
			currentHealth -= damage;
			
			if (currentHealth <= 0) {
				OnDead ();
			}
		}
		
		private void OnDead ()
		{
			InitObject (OnDeadAnimation.name);
			InitObject (OnDeadSprites [Random.Range (0, OnDeadSprites.Length)].name);
			
			ObjectPool.instance.PoolObject (gameObject);
		}
		
		private void InitObject (string objName)
		{
			var deadAnim = ObjectPool.instance.GetObjectForType (objName, false);
			deadAnim.transform.position = transform.position;
			deadAnim.SetActive (true);
		}
		
		
	}
}
