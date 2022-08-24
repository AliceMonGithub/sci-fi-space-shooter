using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Assets.Scripts.Audio;

public class Enemy : MonoBehaviour
{
	public HealthBarHolder HealthBar { get { return _healhBar; } }
	public PointPositionEnemy PointPosition { get; internal set; }

	[SerializeField] private HealthBarHolder _healhBar;
	[SerializeField] private PointShoot _pointShoot;
	[SerializeField] private Explosion _explosionDead;

	private Action<Enemy> _callbackDiedEnemy;
	private List<Vector3> _pathToStart = new List<Vector3>();

	public void AddDamage(float count)
	{
		GetComponentInChildren<HealthBarHolder>().AddDamage(Convert.ToInt32(count));
		GetComponent<RectTransform>().DOPunchPosition(new Vector3(transform.position.x, transform.position.y + 15, 1), 1f);
	}
	private IEnumerator Delay()
	{
		while (true)
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(1, 5f));
			AudioManager.Instance.PlaySound(TypeAudio.LineShoot);
			var bullet = EnemyManager.Instance._pools[0].GetFreeElement();

			bullet.transform.position = _pointShoot.transform.position;
			bullet.gameObject.SetActive(true);
		}
		
	}

	public void Init()
	{
		_healhBar.ResetHealth();
		_healhBar.OnDied += OnHandleDiedEnemy;
	}

	private void OnHandleDiedEnemy()
	{
		AudioManager.Instance.PlaySound(TypeAudio.ExplosionDead);
		var explosion = Instantiate(_explosionDead);
		explosion.transform.position = transform.position;

		_callbackDiedEnemy?.Invoke(this);
		Destroy(gameObject);
	}

	public void SetPathToStartEnemy(List<PointPositionEnemy> pointPositions)
	{
		pointPositions.ForEach(point =>
		{
			_pathToStart.Add(point.transform.position);
		});

		PointPosition = pointPositions[pointPositions.Count - 1];
	}

	public void MoveToPath()
	{
		transform.DOPath(_pathToStart.ToArray(), 3f).onComplete += () =>
		{
			StartCoroutine(Delay());
		};
	}

	public void SubscribeOnDied(Action<Enemy> callback)
	{
		_callbackDiedEnemy += callback;
	}


}
