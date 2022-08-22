using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Scripts.Audio;

namespace Assets.Scripts.UI
{
	public delegate void CallbackOnClickButton();

	[RequireComponent(typeof(Button))]
	public class UIButton : MonoBehaviour
	{
		[SerializeField] private float _sizeClickButton = 0.9f;
		[SerializeField] private float _timeAnimationScale = 0.2f;

		private Button _button;
		private event CallbackOnClickButton _onClickButton;

		private void Awake()
		{
			if (_button == null) _button = GetComponent<Button>();

			_button.onClick.AddListener(OnHandleClickNutton);
		}

		private void OnDestroy()
		{
			_onClickButton = null;

			if (_button != null) _button.onClick.RemoveAllListeners();
		}

		private void OnHandleClickNutton()
		{
			AudioManager.Instance.PlaySound(TypeAudio.ClickButton);

			transform.DOScale(new Vector2(_sizeClickButton, _sizeClickButton), _timeAnimationScale).onComplete += () =>
			{
				transform.DOScale(Vector2.one, _timeAnimationScale).onComplete += () => _onClickButton?.Invoke();
			};
		}

		public void Subscribe(CallbackOnClickButton callback)
		{
			_onClickButton += callback;
		}

		public void UnSubscribe(CallbackOnClickButton callback)
		{
			_onClickButton -= callback;
		}
	}
}

