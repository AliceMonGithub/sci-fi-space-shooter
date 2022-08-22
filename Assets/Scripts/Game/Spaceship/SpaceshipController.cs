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
	}

	private void OnDestroy()
	{
		InputManager.Instance.OnHorizontal -= OnHandleHorizontalMove;
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
			var bullet = _pools[0].GetFreeElement();

			bullet.transform.position = _pointShoot[_indexPointShooter].transform.position;
			bullet.gameObject.SetActive(true);
			_indexPointShooter = _indexPointShooter == 0 ? 1 : 0;
		}

		if (Input.GetMouseButtonDown(1))
		{
			var bullet = _pools[1].GetFreeElement();

			bullet.transform.position = _pointShoot[2].transform.position;
			bullet.gameObject.SetActive(true);
		}

	}

	private int _indexPointShooter = 0;

	private void OnHandleHorizontalMove(int vector)
	{
		_vectorIndex = vector;
		_rectTransform.localRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 6 * -vector);
	}
}
