using Assets.Scripts.Audio;
using Assets.Scripts.Utils;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game
{
	public class GameManager : SingletonMono<GameManager>
	{
		[SerializeField] private CanvasGroup _gameHud;
		[SerializeField] private CanvasGroup _menuUI;
		[SerializeField] private HealthBarHolder _healthBarHolder;

		public override void Awake()
		{
			base.Awake();

			_healthBarHolder.ResetHealth();

			var isTutorial = Convert.ToBoolean(PlayerPrefs.GetInt("first_tutorial_input", 1));

			if(isTutorial == true)
			{
				TutorialManager.Instance.ShowTutorial();
				PlayerPrefs.SetInt("first_tutorial_input", 0);
			}
			else
			{
				TutorialManager.Instance.CloseTutorial();
			}
		}

		public void StartGame()
		{
			_gameHud.alpha = 0f;
			_gameHud.gameObject.SetActive(true);

			_gameHud.DOFade(1f, 0.5f);
			_menuUI.DOFade(0f, 0.5f).onComplete += () =>
			{
				_menuUI.gameObject.SetActive(false);
			};

			AudioManager.Instance.PlayMusic(TypeAudio.MusicGame);
		}
	}
}

