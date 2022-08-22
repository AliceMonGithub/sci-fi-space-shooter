using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialItemHolder : MonoBehaviour
{
	[SerializeField] private CanvasGroup _canvasGroup;
	public void Close()
	{
		_canvasGroup.DOFade(0f, 0.3f).onComplete += () =>
		{
			gameObject.SetActive(false);
		};
	}

	public void Open()
	{
		_canvasGroup.alpha = 0f;
		_canvasGroup.DOFade(1f, 0.3f);
	}
}
