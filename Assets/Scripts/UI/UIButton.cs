using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Scripts.Audio;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
	public delegate void CallbackOnClickButton();

	[RequireComponent(typeof(Button))]
	public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		[SerializeField] private float _sizeClickButton = 0.9f;
		[SerializeField] private float _timeAnimationScale = 0.2f;

		private Button _button;

		private event CallbackOnClickButton _onClickButton;
		private Sequence _sequence;

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

			
			_onClickButton?.Invoke();
		}

		public void Subscribe(CallbackOnClickButton callback)
		{
			_onClickButton += callback;
		}

		public void UnSubscribe(CallbackOnClickButton callback)
		{
			_onClickButton -= callback;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (_sequence != null) _sequence.Kill();

			_sequence.Append(transform.DOScale(new Vector2(_sizeClickButton, _sizeClickButton), _timeAnimationScale));
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (_sequence != null) _sequence.Kill();

			_sequence.Append(transform.DOScale(Vector2.one, _timeAnimationScale));
		}
	}
}

