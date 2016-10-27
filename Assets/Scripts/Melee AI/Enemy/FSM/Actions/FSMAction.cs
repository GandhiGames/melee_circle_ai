using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	public abstract class FSMAction
	{
		public abstract Command GetAction (Transform player);
		protected abstract bool OkToAct ();
		public abstract void Enter ();
		public abstract void Exit ();

	}
}
