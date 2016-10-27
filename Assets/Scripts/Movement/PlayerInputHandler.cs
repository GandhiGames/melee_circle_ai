using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	[RequireComponent (typeof(Rigidbody2D))]
	[RequireComponent (typeof(MeleeAttack))]
	public class PlayerInputHandler : MonoBehaviour
	{
		public float MoveSpeed = 12f;
		private MeleeAttack meleeAttack;
		private Rigidbody2D rigidBody2D;

		void Awake ()
		{
			meleeAttack = GetComponent<MeleeAttack> ();
			rigidBody2D = GetComponent<Rigidbody2D> ();
			
		}

		void Update ()
		{
			var attackCommand = HandleAttack ();

			if (attackCommand != null) {
				attackCommand.Execute ();
			}

			var movementCommand = HandleMovement ();

			if (movementCommand != null) {
				movementCommand.Execute ();
			}
		}

		public Command HandleAttack ()
		{			
			Vector2? heading = null;
			
			
			if (Input.GetMouseButton (0)) {
				Vector2 cursorInWorldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition); 
				heading = cursorInWorldPos - (Vector2)transform.position;

			} else if (Input.GetJoystickNames ().Length > 0) {
				heading = new Vector2 (Input.GetAxisRaw ("RightStickHorizontal"),
				                       Input.GetAxisRaw ("RightStickVertical"));
				
			} 
			
			if (heading.HasValue) {
				return new MeleeCommand (transform, heading.Value, meleeAttack);
			}
			
			
			return null;
			
		}
		
		public Command HandleMovement ()
		{
			Vector2 move = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

			//if (move.magnitude > 0.1f) {
			return new Movement2DCommand (transform, move * MoveSpeed, rigidBody2D);
			//} 
			
			
			
			//return null;
		}
	}
}
