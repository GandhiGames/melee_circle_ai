using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	[RequireComponent (typeof(Animator), typeof(Rigidbody2D))]
	public class WalkAnimationController : MonoBehaviour
	{
		private Rigidbody2D rigidBody2D;
		private Animator animator;

		void Awake()
		{
			rigidBody2D = GetComponent<Rigidbody2D> ();
			animator = GetComponent<Animator> ();
		}

		// Update is called once per frame
		void Update ()
		{
			var stateInfo = animator.GetCurrentAnimatorStateInfo (0);
			
			if (stateInfo.fullPathHash == Animator.StringToHash ("Base Layer.Walk")) {
				animator.speed = (rigidBody2D.velocity.magnitude * 0.3f);
			} else {
				animator.speed = 1;
			} 
		
		}
	}
}
