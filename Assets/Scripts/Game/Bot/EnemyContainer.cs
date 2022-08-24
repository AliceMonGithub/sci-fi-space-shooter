using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyContainer : MonoBehaviour
{
	[SerializeField] private List<PointSpawnEnemy> _spawnPintList;
	[SerializeField] private List<PointPositionEnemy> _pointPositionsLeft = new List<PointPositionEnemy>();
	[SerializeField] private List<PointPositionEnemy> _pointPositionsRight = new List<PointPositionEnemy>();
	private List<Enemy> _enemys = new List<Enemy>();

	public bool AddEnemy(Enemy enemy)
	{
		bool isAddEnemy = false;

		PointPositionEnemy pointPosition = GetFreePointPosition(TypePosition.Left);

		if (pointPosition == null)
		{
			pointPosition = GetFreePointPosition(TypePosition.Right);
		}

		if (pointPosition != null)
		{
			foreach (var spawnPoint in _spawnPintList)
			{
				if (spawnPoint.TypePosition == pointPosition.TypePosition)
				{
					enemy.transform.localPosition = spawnPoint.transform.localPosition;
					break;
				}
			}

			pointPosition.SetEnemy(enemy);

			var path = GenerationPathFromPointPositionEndTarget(pointPosition);
			enemy.SetPathToStartEnemy(path);

			_enemys.Add(enemy);

			enemy.SubscribeOnDied(OnHandleDiedEnemy);

			isAddEnemy = true;
		}

		return isAddEnemy;
	}

	private void OnHandleDiedEnemy(Enemy enemy)
	{
		PointPositionEnemy pointPosition = GetPointPositionTarget(enemy.PointPosition);

		if (pointPosition != null)
		{
			pointPosition.Clear();
			_enemys.Remove(enemy);
		}
	}

	private List<PointPositionEnemy> GenerationPathFromPointPositionEndTarget(PointPositionEnemy pointPositionEnd)
	{
		var path = new List<PointPositionEnemy>();
		var currentListPositions = new List<PointPositionEnemy>();

		switch (pointPositionEnd.TypePosition)
		{
			case TypePosition.Left: currentListPositions = _pointPositionsLeft; break;
			case TypePosition.Right: currentListPositions = _pointPositionsRight; break;
		}

		currentListPositions.ForEach(position =>
		{
			

			if(position.IndexPosition > pointPositionEnd.IndexPosition)
			{
				Debug.Log($"{position.IndexPosition}>{pointPositionEnd.IndexPosition}");
				path.Add(position);
			}
		});

		path.Add(pointPositionEnd);

		BubbleSorting(ref path);

		return path;


		void BubbleSorting(ref List<PointPositionEnemy> positions)
		{
			int length = positions.Count;

			PointPositionEnemy temp = positions[0];

			for (int i = 0; i < length; i++)
			{
				for (int j = i + 1; j < length; j++)
				{
					if (positions[i].IndexPosition < positions[j].IndexPosition)
					{
						temp = positions[i];

						positions[i] = positions[j];

						positions[j] = temp;
					}
				}
			}
		}
	}

	private PointPositionEnemy GetPointPositionTarget(PointPositionEnemy pointPosition)
	{
		PointPositionEnemy position = _pointPositionsLeft.FirstOrDefault(point => point == pointPosition);

		if(position == null)
		{
			position = _pointPositionsRight.FirstOrDefault(point => point == pointPosition);
		}

		return position;
	}

	private PointPositionEnemy GetFreePointPosition(TypePosition type)
	{
		PointPositionEnemy pointPositionEnemy = null;
		List<PointPositionEnemy> positions = new List<PointPositionEnemy>();

		switch (type)
		{
			case TypePosition.Left: positions = _pointPositionsLeft; break;
			case TypePosition.Right: positions = _pointPositionsRight; break;
		}

		positions.ForEach(pointPosition =>
		{
			if(pointPosition.IsBusy == false)
			{
				pointPositionEnemy = pointPosition;
				return;
			}
		});

		return pointPositionEnemy;
	}
}
