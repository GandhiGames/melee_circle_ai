using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	[RequireComponent (typeof(Animator))]
	public class WalkAnimationController : MonoBehaviour
	{

		private Animator animator;
		// Use this for initialization
		void Start ()
		{
			animator = GetComponent<Animator> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
			var stateInfo = animator.GetCurrentAnimatorStateInfo (0);
			
			if (stateInfo.nameHash == Animator.StringToHash ("Base Layer.Walk")) {
				animator.speed = (GetComponent<Rigidbody2D>().velocity.magnitude * 0.3f);
			} else {
				animator.speed = 1;
			} 
		
		}
	}
}
