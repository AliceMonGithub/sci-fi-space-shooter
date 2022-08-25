using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Tutorial
{
	public class TutorialManager : SingletonMono<TutorialManager>
	{
		[SerializeField] private List<TutorialItemHolder> _tutorialItemHolders = new List<TutorialItemHolder>();

		public void CloseTutorial()
		{
			InputManager.Instance.IsActive = true;
			_tutorialItemHolders.ForEach(tutorialItem => tutorialItem.Close());
		}

		public void ShowTutorial()
		{
			InputManager.Instance.IsActive = false;
			_tutorialItemHolders.ForEach(tutorialItem =>
			{
				tutorialItem.gameObject.SetActive(true);
				tutorialItem.Open();
			});
		}
	}
}
