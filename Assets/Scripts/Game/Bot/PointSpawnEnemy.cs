using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Enemy
{
	public class PointSpawnEnemy : MonoBehaviour
	{
		[SerializeField] private TypePosition _typePosition;
		public TypePosition TypePosition { get { return _typePosition; } }
	}
}
