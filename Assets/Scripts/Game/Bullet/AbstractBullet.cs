using Assets.Scripts.Audio;
using Bot = Assets.Scripts.Game.Enemy;
using Effects = Assets.Scripts.Game.Explosion;
using System.Collections;
using UnityEngine;


namespace Assets.Scripts.Game.Bullet
{
	public abstract class AbstractBullet : MonoBehaviour
	{
		[SerializeField] private RectTransform _rectTransform;
		[SerializeField] private float _countDamage;
		[SerializeField] private Effects.Explosion _explosion;
		[SerializeField] private float _speedMove;

		public float CountDamage { get { return _countDamage; } }
		public Effects.Explosion Explosion { get { return _explosion; } }
		public float SpeedMove { get { return _speedMove; } }
		public RectTransform RectTransform { get { return _rectTransform; } }


		private void OnEnable()
		{
			StartCoroutine(DelayDeactivateBullet());
		}

		public virtual void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.GetComponent<Bot.Enemy>() != null)
			{
				AudioManager.Instance.PlaySound(TypeAudio.KillEnemy);

				Instantiate(Explosion, collision.transform);
				gameObject.SetActive(false);

				collision.GetComponent<Bot.Enemy>().AddDamage(CountDamage);
			}
		}

		protected virtual void MoveBulletUp()
		{
			_rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, _rectTransform.anchoredPosition.y + 1f * SpeedMove);
		}


		private IEnumerator DelayDeactivateBullet()
		{
			yield return new WaitForSecondsRealtime(2f);
			gameObject.SetActive(false);
		}

		protected virtual void MoveBulletDown()
		{
			_rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, _rectTransform.anchoredPosition.y - 1f * SpeedMove);
		}
	}
}
