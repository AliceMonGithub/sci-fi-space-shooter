using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

namespace Assets.Scripts.Audio
{
	public class AudioManager : SingletonMono<AudioManager>
	{
		[Header("Audio Sources:")]
		[SerializeField] private AudioSource _musicSource;
		[SerializeField] private AudioSource _soundSource;

		[Space]

		[Header("Audios:")]
		[SerializeField] private List<Audio> _sounds = new List<Audio>();
		[SerializeField] private List<Audio> _musics = new List<Audio>();

		[Space]

		[Header("Settings pitch to music:")]
		[SerializeField] private List<Pitch> _piths = new List<Pitch>();
		[SerializeField] private float _timeSwitchingPitch = 0.5f;


		private Dictionary<TypeAudio, Audio> _audios = new Dictionary<TypeAudio, Audio>();


		public override void Awake()
		{
			base.Awake();

			_sounds.ForEach(sound => _audios.Add(sound.Type, sound));
			_musics.ForEach(music => _audios.Add(music.Type, music));

			PlayMusic(TypeAudio.MusicMainMenu);
		}


		public void PlaySound(TypeAudio typeAudio)
		{
			var audio = GetAudioFromTargetType(typeAudio);

			_soundSource.PlayOneShot(audio.Clip);
		}

		public void PlayMusic(TypeAudio typeAudio)
		{
			SetPitchToCurrentMusic(TypePitch.Normal);

			var audio = GetAudioFromTargetType(typeAudio);

			_musicSource.clip = audio.Clip;
			_musicSource.Play();
		}

		public void SetPitchToCurrentMusic(TypePitch typePitch)
		{
			var pitch = _piths.FirstOrDefault(pitch => pitch.Type == typePitch);

			if(pitch == null)
			{
				Debug.LogError("You are trying to use a pitch that is not on the list!");
				return;
			}

			_musicSource.DOPitch(pitch.Value, _timeSwitchingPitch);
		}

		private Audio GetAudioFromTargetType(TypeAudio typeAudio)
		{
			if(_audios.ContainsKey(typeAudio) == false)
			{
				Debug.LogError("You are trying to use a type audio that is not on the list!");
				return null;
			}

			Audio audio = _audios[typeAudio];

			return audio;
		}
	}
}

