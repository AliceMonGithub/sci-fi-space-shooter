using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Assets.Scripts.Audio;

public class Enemy : MonoBehaviour
{
	[SerializeField] private HealthBarHolder _healhBar; 

	public HealthBarHolder HealthBar { get { return _healhBar; } }

	private Action<Enemy> _callbackDiedEnemy;

	private Vector3[] _path;

	[SerializeField] private PointShoot pointShoot;


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

			bullet.transform.position = pointShoot.transform.position;
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
		_callbackDiedEnemy?.Invoke(this);
		Destroy(gameObject);
	}

	public void SetPath(Vector3[] path)
	{
		_path = path;
	}

	public void MoveToPath()
	{
		transform.DOPath(_path, 3f);
		
	}

	public void OnEnable()
	{
		StartCoroutine(Delay());
	}

	public void Subscribe(Action<Enemy> callback)
	{
		_callbackDiedEnemy += callback;
	}


}
