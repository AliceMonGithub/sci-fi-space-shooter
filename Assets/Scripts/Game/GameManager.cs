using Assets.Scripts.Audio;
using Assets.Scripts.Utils;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game
{
	public class GameManager : SingletonMono<GameManager>
	{
		[SerializeField] private CanvasGroup _gameHud;
		[SerializeField] private CanvasGroup _menuUI;

		public override void Awake()
		{
			base.Awake();
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

