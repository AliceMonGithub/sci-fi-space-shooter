using Assets.Scripts.Game;
using Assets.Scripts.Settings;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Menu
{
	public class MenuHandler : MonoBehaviour
	{
		[SerializeField] private SettingsHandler _settingsHandler;

		[SerializeField] private UIButton _playButton;
		[SerializeField] private UIButton _settingsButton;

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

		}

		private void OnHandlePlayButton()
		{
			GameManager.Instance.StartGame();
		}

	}
}

