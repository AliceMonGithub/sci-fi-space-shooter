using Assets.Scripts.Game.Spaceship;
using Assets.Scripts.Audio;
using UnityEngine;

namespace Assets.Scripts.Game.Bullet
{
	public class DarkBullet : AbstractBullet
	{
		private void Update()
		{
			MoveBulletDown();
		}

		public override void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.GetComponent<SpaceshipController>() != null)
			{
				AudioManager.Instance.PlaySound(TypeAudio.KillEnemy);

				Instantiate(Explosion, collision.transform);
				gameObject.SetActive(false);

				collision.GetComponent<SpaceshipController>().AddDamage(CountDamage);
			}

		}
	}
}
