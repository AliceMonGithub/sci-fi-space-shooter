using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Enemy
{
	public class PointPositionEnemy : MonoBehaviour
	{
		[SerializeField] private int _indexPosition;
		[SerializeField] private TypePosition _typePosition;
		public bool IsBusy { get; private set; } = false;
		public Enemy Enemy { get; private set; } = null;
		public int IndexPosition { get { return _indexPosition; } }
		public TypePosition TypePosition { get { return _typePosition; } }

		public void SetEnemy(Enemy enemy)
		{
			Enemy = enemy;
			IsBusy = true;
		}

		public void Clear()
		{
			Enemy = null;
			IsBusy = false;
		}


	}
}
