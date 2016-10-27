using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	public class LookAtPlayer : MonoBehaviour
	{
		public Transform player;
		
		private Animator animator;
		private bool spawning = true;
		
		// Use this for initialization
		void Start ()
		{
			if (!player) {
				player = GameObject.FindGameObjectWithTag ("Player").transform;
			}
			
			animator = GetComponent<Animator> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (!spawning || !IsSpawning ()) {
				Vector3 dir = player.position - transform.position;
				float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
			}
		}
		
		private bool IsSpawning ()
		{
			if (!animator)
				return false;
			
			var stateInfo = animator.GetCurrentAnimatorStateInfo (0);
			
			spawning = stateInfo.nameHash == Animator.StringToHash ("Base Layer.Spawn");
			
			return spawning;
		}
	}
}
