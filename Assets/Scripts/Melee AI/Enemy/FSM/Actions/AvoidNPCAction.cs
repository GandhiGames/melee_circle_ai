using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MeleeAI
{
	public class AvoidNPCAction : FSMAction
	{
		
		private Transform character;
		private Rigidbody2D rigidbody;
		private float avoidanceRange;
				
		private float forceMultiplier;
		private string avoidTag;
		//Increases the strength of the force - lower numbers equals a stronger force
		private static readonly float MAG_OFFSET = 0.02f;
		
		public AvoidNPCAction (Transform character, float avoidanceRange, string avoidTag, float forceMultiplier = 2f)
		{
			this.character = character;
			this.avoidanceRange = avoidanceRange;
			this.forceMultiplier = forceMultiplier;
			this.avoidTag = avoidTag;

			rigidbody = character.GetComponent<Rigidbody2D> ();
		}
				
		private List<GameObject> GetEntitiesInSight (string tagName)
		{
			var retVals = new List<GameObject> ();
			
			var entities = GameObject.FindGameObjectsWithTag (tagName);
			
			foreach (var obj in entities) {			

					
				float to = (obj.transform.position - character.transform.position).sqrMagnitude;
					
				if (!obj.transform.Equals (character.transform) && to <= (avoidanceRange * avoidanceRange)) {
					retVals.Add (obj);
				}
								
				
			}
			
			return retVals;
		}
				
		private Vector2 GetForce ()
		{
			
			var entities = GetEntitiesInSight (avoidTag);
						
		
			if (entities.Count == 0) {
				return Vector2.zero;
			}
						
			
			var steeringForce = Vector2.zero;
			foreach (var obj in entities) {
				Vector2 toAgent = character.transform.position - obj.transform.position;
				steeringForce += toAgent.normalized / (toAgent.magnitude * MAG_OFFSET);
			}
			
			
			return steeringForce;
		}
		
		public override Command GetAction (Transform player)
		{
			if (OkToAct ()) {
				
				var force = GetForce ();
								
				if (force == Vector2.zero) {
					return null;
				}
				
				return new Movement2DCommand (character, force * forceMultiplier, rigidbody);	
			}
			
			return null;
		}
		
		protected override bool OkToAct ()
		{
			return true;
		}

		public override void Enter ()
		{

		}

		public override void Exit ()
		{

		}
	}
}
