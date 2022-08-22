using UnityEngine;
using System;


namespace Assets.Scripts.Audio
{
	[Serializable]
	public class Pitch
	{
		[SerializeField] private TypePitch _typePitch;
		[SerializeField] private float _value;

		public TypePitch Type { get { return _typePitch; } }
		public float Value { get { return _value; } }
	}
}

