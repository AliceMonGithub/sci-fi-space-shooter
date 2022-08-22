using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	}
}
