using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class HealthBarHolder : MonoBehaviour
{
	public int Health { get; private set; } = 100;
	public Action OnDied;

	[SerializeField] private Slider _healthBarSlider;
	[SerializeField] private Slider _hitBarSlider;
	[SerializeField] private Slider _faderSlider;

	private Sequence _sequence;
	private bool _isHealthDamage = false;
	private float _valueHitSlider = 0f;

	private bool _isDied = false;

	public void ResetHealth()
	{
		Health = 100;

		_isDied = false;

		_healthBarSlider.value = 1f;
		_hitBarSlider.value = 0f;
		_faderSlider.value = 0f;
	}

	public void AddDamage(int count)
	{
		Health -= count;

		if(Health == 0) _isDied = true;

		var damage = 1f / 100f * count;

		if (_sequence != null)
		{
			_sequence.Kill();

			if(_isHealthDamage == false)
			{
				_healthBarSlider.value -= damage;
				_hitBarSlider.value = _valueHitSlider;
				_faderSlider.value = _valueHitSlider;
				_hitBarSlider.GetComponent<CanvasGroup>().alpha = 0f;
			}
		}

		_isHealthDamage = false;

		_sequence = DOTween.Sequence();

		_hitBarSlider.GetComponent<CanvasGroup>().alpha = 1f;
		_valueHitSlider = _hitBarSlider.value + damage;

		var hitBarSliderValue = _hitBarSlider.DOValue(_hitBarSlider.value + damage, 0.3f);

		hitBarSliderValue.onComplete += () =>
		{
			_faderSlider.value = _hitBarSlider.value;
		};

		var hitBarSliderFader = _hitBarSlider.GetComponent<CanvasGroup>().DOFade(0f, 0.2f);

		hitBarSliderFader.onComplete += () =>
		{
			_healthBarSlider.value -= damage;
			_isHealthDamage = true;

			if (_isDied) OnDied?.Invoke();
		};

		_sequence.Append(hitBarSliderValue);
		_sequence.Append(hitBarSliderFader);
	}
}
