using UnityEngine;
using System.Collections;

namespace MeleeAI
{
	public interface ILimitedAttack
	{
		bool AttackComplete { get; }
	}
}
