using Assets.Scripts.UI;
using Assets.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonMono<InputManager>
{
	public Action<int> OnHorizontal;
	public Action<int> OnMouseDown;

	[SerializeField] private UIButton _leftArrowButton;
	[SerializeField] private UIButton _rightArrowButton;

	[SerializeField] private UIButton _ShootLineButton;
	[SerializeField] private UIButton _ShootRocketButton;

	public bool IsActive = true;

	private void Start()
	{
		_leftArrowButton.OnDownButton += () => HorizontalMove(-1);
		_leftArrowButton.OnUpButton += () => HorizontalMove(0);

		_rightArrowButton.OnDownButton += () => HorizontalMove(1);
		_rightArrowButton.OnUpButton += () => HorizontalMove(0);

		_ShootLineButton.OnDownButton += () => OnMouseDownHandler(1);
		_ShootRocketButton.OnDownButton += ()=> OnMouseDownHandler(2);
	}
	private void Update()
	{
		if (IsActive)
		{
			//Move horizontal
			if (Input.GetKey(KeyCode.A))
			{
				OnHorizontal?.Invoke(-1);
			}
			if (Input.GetKey(KeyCode.D))
			{
				OnHorizontal?.Invoke(1);
			}

			if ((Input.GetKeyUp(KeyCode.D)) || Input.GetKeyUp(KeyCode.A))
			{
				OnHorizontal?.Invoke(0);
			}
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

	public void OnMouseDownHandler(int id)
	{
		OnMouseDown?.Invoke(id);
	}


}
