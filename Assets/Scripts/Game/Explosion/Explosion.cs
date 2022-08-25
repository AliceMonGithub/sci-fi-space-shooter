using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Explosion
{
	public class Explosion : MonoBehaviour
	{
		private void OnEnable()
		{
			StartCoroutine(Delay());
		}

		IEnumerator Delay()
		{
			yield return new WaitForSeconds(0.5f);
			Destroy(gameObject);
		}
	}
}
