using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBullet : AbstractBullet
{
	private void OnEnable()
	{
		StartCoroutine(Delay());
	}
	private void Update()
	{
		GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, GetComponent<RectTransform>().anchoredPosition.y + 1f * SpeedMove);
	}

	private IEnumerator Delay()
	{
		yield return new WaitForSecondsRealtime(2f);
		gameObject.SetActive(false);
	}
}
