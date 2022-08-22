using Assets.Scripts.UI;
using Assets.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonMono<InputManager>
{
	public Action<int> OnHorizontal;

	[SerializeField] private UIButton _leftArrowButton;
	[SerializeField] private UIButton _rightArrowButton;

	private void Start()
	{
		_leftArrowButton.OnDownButton += () => HorizontalMove(-1);
		_leftArrowButton.OnUpButton += () => HorizontalMove(0);

		_rightArrowButton.OnDownButton += () => HorizontalMove(1);
		_rightArrowButton.OnUpButton += () => HorizontalMove(0);
	}
	private void Update()
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

	private void HorizontalMove(int vectorIndex)
	{
		OnHorizontal?.Invoke(vectorIndex);
	}


}
