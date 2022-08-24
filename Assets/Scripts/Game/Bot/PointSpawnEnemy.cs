using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawnEnemy : MonoBehaviour
{
	[SerializeField] private TypePosition _typePosition;
	public TypePosition TypePosition { get { return _typePosition; } }
}
