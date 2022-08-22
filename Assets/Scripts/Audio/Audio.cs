using System;
using UnityEngine;

namespace Assets.Scripts.Audio
{
	[Serializable]
	public class Audio
	{
		[SerializeField] private TypeAudio _typeAudio;
		[SerializeField] private AudioClip _audioClip;
		[SerializeField] private float _pitchDefault;

		public TypeAudio Type { get { return _typeAudio; } }
		public AudioClip Clip { get { return _audioClip; } }
		public float Pitch { get { return _pitchDefault; } }
	}
}

