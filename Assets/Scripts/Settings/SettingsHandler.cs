using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Assets.Scripts.UI;
using TMPro;

namespace Assets.Scripts.Settings
{
	public class SettingsHandler : MonoBehaviour
	{
		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private float _timeFadeDuration = 0.5f;
		[SerializeField] private UIButton _closeButton;
		[SerializeField] private TMP_Text _nameGameText;

		private void Start()
		{
			_closeButton.Subscribe(Close);
		}

		private void OnEnable()
		{
			_canvasGroup.DOFade(1f, _timeFadeDuration);
		}

		private void OnDisable()
		{
			_canvasGroup.alpha = 0f;
		}

		private void OnDestroy()
		{
			_closeButton.UnSubscribe(Close);
		}

		public void Close()
		{
			_nameGameText.DOFade(1f, _timeFadeDuration);

			_canvasGroup.DOFade(0f, _timeFadeDuration).onComplete += () => 
			{
				gameObject.SetActive(false);
			};
		}
	}
}

