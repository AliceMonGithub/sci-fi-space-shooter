using Assets.Scripts.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBullet : AbstractBullet
{
	private void OnEnable()
	{
		StartCoroutine(Delay());
	}
	private void Update()
	{
		GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, GetComponent<RectTransform>().anchoredPosition.y - 1f * SpeedMove);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.GetComponent<SpaceshipController>() != null)
		{
			AudioManager.Instance.PlaySound(TypeAudio.KillEnemy);
			Instantiate(Explosion, collision.transform);
			gameObject.SetActive(false);
			collision.GetComponent<SpaceshipController>().AddDamage(CountDamage);
			Debug.Log(collision.gameObject.name);
		}
		
	}

	private IEnumerator Delay()
	{
		yield return new WaitForSecondsRealtime(2f);
		gameObject.SetActive(false);
	}
}
