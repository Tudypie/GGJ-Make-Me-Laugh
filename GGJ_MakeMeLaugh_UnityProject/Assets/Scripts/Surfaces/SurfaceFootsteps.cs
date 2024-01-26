using System;
using UnityEngine;
using GameJam.Utilities;
using System.Collections.Generic;
using GameJam.Utilities.Attributes;
using Void = GameJam.Utilities.Void;

namespace GameJam.Surfaces
{
	[CreateAssetMenu(fileName = "SurfaceFootsteps", menuName = "ScriptableObjects/SurfaceFootsteps")]
	public class SurfaceFootsteps : ScriptableObject
	{
		[Serializable]
		public struct FootstepSoundsData : IEquatable<FootstepSoundsData>
		{
			public SurfaceMap.SurfaceTypes surfaceType;
			public SoundPack soundPack;

			public readonly bool Equals(FootstepSoundsData other) => surfaceType == other.surfaceType;
		}

		public List<FootstepSoundsData> footstepSounds;

		[MessageBox("The list contains duplicate surface or soundpack", nameof(containsDuplicate), false, MessageMode.Warning)]
		[SerializeField] private Void messageHolder;

		[SerializeField, HideInInspector] private bool containsDuplicate;

		void OnValidate() => containsDuplicate = Utils.CheckStructListDuplicate(footstepSounds, out _);
	}
}
