using Assets.Scripts.Game;
using Assets.Scripts.Settings;
using Assets.Scripts.UI;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.Menu
{
	public class MenuHandler : MonoBehaviour
	{
		[SerializeField] private SettingsHandler _settingsHandler;

		[SerializeField] private UIButton _playButton;
		[SerializeField] private UIButton _settingsButton;

		[SerializeField] private TMP_Text _nameGameText;

		private void Start()
		{
			_playButton.Subscribe(OnHandlePlayButton);
			_settingsButton.Subscribe(OnHandleSettingsButton);
		}

		private void OnDestroy()
		{
			_playButton.UnSubscribe(OnHandlePlayButton);
			_settingsButton.UnSubscribe(OnHandleSettingsButton);
		}

		private void OnHandleSettingsButton()
		{
			_nameGameText.DOFade(0, 0.5f);
			_settingsHandler.gameObject.SetActive(true);
		}

		private void OnHandlePlayButton()
		{
			GameManager.Instance.StartGame();
		}

	}
}

