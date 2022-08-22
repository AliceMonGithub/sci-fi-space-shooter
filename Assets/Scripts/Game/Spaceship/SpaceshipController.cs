using Assets.Scripts.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
	[SerializeField] private RectTransform _rectTransform;
	[SerializeField] private float _speedMove;

	private List<PoolControllerMono<AbstractBullet>> _pools;
	[SerializeField] private List<AbstractBullet> _lineBulletPrefabs;
	[SerializeField] private Transform _bulletContent;
	private int _vectorIndex = 0;
	[SerializeField] private int _poolCount;

	[SerializeField] List<PointShoot> _pointShoot;

	[SerializeField] private HealthBarHolder _stamynaBarHolder;
	[SerializeField] private HealthBarHolder _healthBarHolder;

	private void Start()
	{
		InputManager.Instance.OnHorizontal += OnHandleHorizontalMove;

		_pools = new List<PoolControllerMono<AbstractBullet>>();

		for (int i = 0; i < _poolCount; i++)
		{
			var pool = new PoolControllerMono<AbstractBullet>(_lineBulletPrefabs[i], _poolCount, _bulletContent);
			_pools.Add(pool);
			_pools[i].IsAutoExpand = true;
		}

		_stamynaBarHolder.ResetHealth();
		_stamynaBarHolder.OnDied += () =>
		{
			_isRocketShhot = false;
			StartCoroutine(Delay());
		};
	}

	internal void AddDamage(float countDamage)
	{
		_healthBarHolder.AddDamage(Convert.ToInt32(countDamage));
	}

	private bool _isRocketShhot = true;

	private IEnumerator Delay()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.2f);
			_stamynaBarHolder.AddDamage(-10);
			if (_stamynaBarHolder.Health >= 100)
			{
				break;
			}
		}
		_stamynaBarHolder.ResetHealth();
		_isRocketShhot = true;
	}

	private void OnDestroy()
	{
		if(InputManager.Instance != null)
		{
			InputManager.Instance.OnHorizontal -= OnHandleHorizontalMove;
		}
	}

	private void Update()
	{
		if(_vectorIndex != 0)
		{
			var translateVector = _rectTransform.anchoredPosition;
			translateVector.x += _vectorIndex * Time.deltaTime * _speedMove;
			_rectTransform.anchoredPosition = translateVector;
		}

		if (Input.GetMouseButtonDown(0))
		{
			AudioManager.Instance.PlaySound(TypeAudio.LineShoot);
			var bullet = _pools[0].GetFreeElement();

			bullet.transform.position = _pointShoot[_indexPointShooter].transform.position;
			bullet.gameObject.SetActive(true);
			_indexPointShooter = _indexPointShooter == 0 ? 1 : 0;
		}

		if (Input.GetMouseButtonDown(1) && _isRocketShhot == true)
		{
			AudioManager.Instance.PlaySound(TypeAudio.RocketShoot);
			var bullet = _pools[1].GetFreeElement();

			bullet.transform.position = _pointShoot[2].transform.position;
			bullet.gameObject.SetActive(true);

			_stamynaBarHolder.AddDamage(35);
		}

	}

	private int _indexPointShooter = 0;

	private void OnHandleHorizontalMove(int vector)
	{
		_vectorIndex = vector;
		_rectTransform.localRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 6 * -vector);
	}
}
