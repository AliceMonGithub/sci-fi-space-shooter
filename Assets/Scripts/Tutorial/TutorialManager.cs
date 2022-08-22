using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : SingletonMono<TutorialManager>
{
	[SerializeField] private List<TutorialItemHolder> _tutorialItemHolders = new List<TutorialItemHolder>();

	public void CloseTutorial()
	{
		_tutorialItemHolders.ForEach(tutorialItem => tutorialItem.Close());
	}

	public void ShowTutorial()
	{
		_tutorialItemHolders.ForEach(tutorialItem =>
		{
			tutorialItem.gameObject.SetActive(true);
			tutorialItem.Open();
		});
	}
}
