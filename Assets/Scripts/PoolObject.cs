using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	public class PoolObject : MonoBehaviour
	{

		public void Execute ()
		{
			ObjectPool.instance.PoolObject (this.gameObject);
		}
	}
}
