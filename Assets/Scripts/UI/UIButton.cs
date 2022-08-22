using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Assets.Scripts.UI
{
	public delegate void CallbackOnClickButton();

	[RequireComponent(typeof(Button))]
	public class UIButton : MonoBehaviour
	{
		private Button _button;
		private event CallbackOnClickButton _onClickButton;

		private void Awake()
		{
			if (_button == null) _button = GetComponent<Button>();

			_button.onClick.AddListener(OnHandleClickNutton);
		}

		private void OnHandleClickNutton()
		{
			transform.DOScale(new Vector2(0.9f, 0.9f), 0.2f).onComplete += () =>
			{
				transform.DOScale(Vector2.one, 0.2f).onComplete += () => _onClickButton?.Invoke();
			};
		}

		public void Subscribe(CallbackOnClickButton callback)
		{
			_onClickButton += callback;
		}

		private void OnDestroy()
		{
			_onClickButton = null;

			if(_button != null) _button.onClick.RemoveAllListeners();
		}
	}
}

