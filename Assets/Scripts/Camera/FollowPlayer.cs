using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	public class FollowPlayer : MonoBehaviour
	{
		public Transform Player;
	
		private static readonly float X_OFFSET = 0.5f;
	
		// Update is called once per frame
		void Update ()
		{
			transform.position = new Vector3 (Player.position.x, Player.position.y, -X_OFFSET);
		}
	}
}
