using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	public class LookAtMouse : MonoBehaviour
	{

		// Update is called once per frame
		void Update ()
		{
			var mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			var dir = mousePos - transform.position;
			var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		}
	}
}
