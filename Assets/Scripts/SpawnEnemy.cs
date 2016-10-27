using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	public class SpawnEnemy : MonoBehaviour
	{

		public Transform Enemy;
	
		public void Spawn ()
		{
			var enemy = ObjectPool.instance.GetObjectForType (Enemy.name, true);
			
			if (enemy) {
				enemy.transform.position = new Vector2 (Random.Range (1, 20), Random.Range (1, 20));
				enemy.SetActive (true);
			}
		}
	}
}
