using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.Settings
{
	public class SettingsHandler : MonoBehaviour
	{
		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private float _timeFadeDuration = 0.5f;

		private void OnEnable()
		{
			_canvasGroup.DOFade(1f, _timeFadeDuration);
		}

		private void OnDisable()
		{
			_canvasGroup.alpha = 0f;
		}
	}
}

