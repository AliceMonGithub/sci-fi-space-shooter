using Assets.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SingletonMono<EnemyManager>
{
	[SerializeField] private Enemy _enemyPrefab;
	[SerializeField] private List<Enemy> _enemys;
	[SerializeField] private Transform _transform;

	[SerializeField] private List<Transform> _paths;

	private Action _onCallbackEndRoundEnemys;
	public List<PoolControllerMono<AbstractBullet>> _pools;
	[SerializeField] private AbstractBullet _bullet;
	[SerializeField] private Transform _contetn;

	public void CreateEnemy(int round)
	{
		for (int i = 0; i < round; i++)
		{
			Enemy enemy = Instantiate(_enemyPrefab, _transform);
			List<Vector3> path = new List<Vector3>();
			for (int j = 0; j < round * UnityEngine.Random.Range(1, 3); j++)
			{
				path.Add(_paths[UnityEngine.Random.Range(0, _paths.Count - 1)].position);
			}
			enemy.SetPath(path.ToArray());
			enemy.Init();
			_enemys.Add(enemy);
			enemy.Subscribe(OnHandleDiedBot);
			enemy.MoveToPath();
			
		}

		_pools = new List<PoolControllerMono<AbstractBullet>>();

		for (int i = 0; i < 1; i++)
		{
			var pool = new PoolControllerMono<AbstractBullet>(_bullet, 1, _contetn);
			_pools.Add(pool);
			_pools[i].IsAutoExpand = true;
		}

	}
	private void OnHandleDiedBot(Enemy enemy)
	{
		_enemys.Remove(enemy);
		if(_enemys.Count <= 0)
		{
			_onCallbackEndRoundEnemys?.Invoke();
		}
	}

	public void Subscribe(Action callbackEndRound)
	{
		_onCallbackEndRoundEnemys += callbackEndRound;
	}
}
