using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bot : MonoBehaviour
{
	[SerializeField] private List<Transform> targets = new List<Transform>();
	private void Start()
	{
		GetComponentInChildren<HealthBarHolder>().ResetHealth();
		GetComponentInChildren<HealthBarHolder>().OnDied += () =>
		{
			Destroy(gameObject);
		};

		List<Vector3> _paths = new List<Vector3>();

		foreach (var item in targets)
		{
			_paths.Add(item.position);
		}
		transform.DOPath(_paths.ToArray(), 3f);
		

	}
	public void AddDamage(float count)
	{
		GetComponentInChildren<HealthBarHolder>().AddDamage(Convert.ToInt32(count));
		GetComponent<RectTransform>().DOPunchPosition(new Vector3(transform.position.x, transform.position.y + 15, 1), 1f);
	}
}
