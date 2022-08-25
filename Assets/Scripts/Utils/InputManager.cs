using Assets.Scripts.UI;
using Assets.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utils
{
	public class InputManager : SingletonMono<InputManager>
	{
		public Action<int> OnHorizontal;
		public Action<int> OnMouseDown;

		[SerializeField] private UIButton _leftArrowButton;
		[SerializeField] private UIButton _rightArrowButton;

		public bool IsActive = true;

		private void Start()
		{
			_leftArrowButton.OnDownButton += () => HorizontalMove(-1);
			_leftArrowButton.OnUpButton += () => HorizontalMove(0);

			_rightArrowButton.OnDownButton += () => HorizontalMove(1);
			_rightArrowButton.OnUpButton += () => HorizontalMove(0);
		}
		private void Update()
		{
			if (IsActive)
			{
				if (Input.GetKey(KeyCode.A)) OnHorizontal?.Invoke(-1);
				if (Input.GetKey(KeyCode.D)) OnHorizontal?.Invoke(1);
				if ((Input.GetKeyUp(KeyCode.D)) || Input.GetKeyUp(KeyCode.A)) OnHorizontal?.Invoke(0);
			}
			else
			{
				OnHorizontal?.Invoke(0);
			}
		}

		private void HorizontalMove(int vectorIndex)
		{
			OnHorizontal?.Invoke(vectorIndex);
		}

	}
}
