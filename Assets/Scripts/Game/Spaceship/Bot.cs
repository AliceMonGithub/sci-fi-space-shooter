using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bot : MonoBehaviour
{
	private void Start()
	{
		GetComponentInChildren<HealthBarHolder>().ResetHealth();
		GetComponentInChildren<HealthBarHolder>().OnDied += () =>
		{
			Destroy(gameObject);
		};

		

	}
	public void AddDamage(float count)
	{
		GetComponentInChildren<HealthBarHolder>().AddDamage(Convert.ToInt32(count));
		GetComponent<RectTransform>().DOPunchPosition(new Vector3(transform.position.x, transform.position.y + 15, 1), 1f);
	}
}
