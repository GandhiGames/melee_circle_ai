using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MeleeAI
{
	/// <summary>
	/// Base class for any character that will use the Finite-state machine. 
	/// Provides functionality to switch between states and call the current states update method.
	/// </summary>
	[RequireComponent (typeof(FSM))]
	public abstract class NPC : MonoBehaviour
	{
		// Common characteristics of all enemies.
		public float SeekSpeed = 20f;
					
		public bool IsSpawning = true;

		// Returns the previous state. Useful for states such as dig where
		// you would want the character to return to their previous state when they have finished digging.
		public FSMStateID PreviousState {
			get {
				return stateMachine.PreviousStateID;
			}
		}

		public string TargetTag;

		protected FSM stateMachine;

		// Every state is passed the players transform through their Reason and Act methods.
		protected Transform player;

				
		/// <summary>
		/// Using the object pool, a character is added and removed from the scene. WHen they are re-added to a
		/// scene it is important that the FSM is reset.
		/// </summary>
		void OnEnable ()
		{
			//if (stateMachine) {
			//	stateMachine.Reset ();
			//} else {

			Init ();
			//}
		}

		/// <summary>
		/// Initialises the statemachine and rewind (if not already).  
		/// Sets supportRewind to true if a rewind component is found and enabled.
		/// Calls ConstructFSM method, which setups a character specific state machine.
		/// </summary>
		void Init ()
		{
			if (stateMachine)
				stateMachine.ClearStates ();
			else
				stateMachine = GetComponent<FSM> ();
			ConstructFSM ();	
			
		}

		/// <summary>
		/// Returns true if the player is initilised and in close proximity (set by SeekDistance)
		/// and either:
		/// 	Rewind is supported and is not currently eecuting (i.e. rewinding the scene)
		///		Rewind is not supported.
		/// </summary>
		private bool OkToAct ()
		{
			return player != null && !IsSpawning;
		}

		/// <summary>
		/// Instantiates player transform and then if it is ok to act, the current states
		/// reason and act methods are called. Further Information on these methods can be found in
		/// the FSMState class.
		/// </summary>
		void FixedUpdate ()
		{

			if (!player) {
				InstantiatePlayerTransform ();
			}
						
			if (OkToAct ()) {
				stateMachine.CurrentState.Reason (player);
				stateMachine.CurrentState.Act (player);
			}

		}

		private void InstantiatePlayerTransform ()
		{
			var playerObj = GameObject.FindGameObjectWithTag (TargetTag);

			if (playerObj) {
				player = playerObj.transform;
			}
		}

		/// <summary>
		/// This performs a transistion from one state to another and is invoked in an individual states Reason method.
		/// </summary>
		public void SetTransistion (FSMTransistion tran, Transform extraData = default(Transform))
		{
			stateMachine.PerformTransition (tran, extraData);
		}
		
		public void FinishedSpawning ()
		{
			IsSpawning = false;
		}

		
		/// <summary>
		/// Constructs a character specfic state machine. This is abstract as each characters state machine will
		/// be different.
		/// </summary>
		protected abstract void ConstructFSM ();


	
	}
}
