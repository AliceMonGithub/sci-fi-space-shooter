using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbstractBullet : MonoBehaviour
{
	[SerializeField] private float _countDamage;
	[SerializeField] private Explosion _explosion;
	[SerializeField] private float _speedMove;

	public float CountDamage { get { return _countDamage; } }
	public Explosion Explosion { get { return _explosion; } }
	public float SpeedMove { get { return _speedMove; } }
}
