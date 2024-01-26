using System;
using UnityEngine;
using GameJam.Utilities;
using System.Collections.Generic;
using GameJam.Utilities.Attributes;
using Void = GameJam.Utilities.Void;

namespace GameJam.Surfaces
{
	[CreateAssetMenu(fileName = "SurfaceMap", menuName = "ScriptableObjects/SurfaceMap")]
	public class SurfaceMap : ScriptableObject
	{
		public enum SurfaceTypes
		{
			Default,
			SurfaceType01,
			SurfaceType02,
			SurfaceType03,
			SurfaceType04,
			SurfaceType05,
			SurfaceType06,
			SurfaceType07,
			SurfaceType08,
			SurfaceType09,
			SurfaceType10,
			SurfaceType11,
			SurfaceType12,
			SurfaceType13,
			SurfaceType14,
			SurfaceType15,
			SurfaceType16,
			SurfaceType17,
			SurfaceType18,
			SurfaceType19,
			SurfaceType20
		}

		[Serializable]
		public struct SurfaceData : IEquatable<SurfaceData>
		{
			public SurfaceTypes surfaceType;
			public string surfaceName;

			public readonly bool Equals(SurfaceData other) => surfaceType == other.surfaceType || surfaceName == other.surfaceName;
		}

		public List<SurfaceData> surfaceList;

		[MessageBox("The list contains duplicate surface or name", nameof(containsDuplicate), false, MessageMode.Warning)]
		[SerializeField] private Void messageHolder;

		[SerializeField, HideInInspector] private bool containsDuplicate;

		void OnValidate() => containsDuplicate = Utils.CheckStructListDuplicate(surfaceList, out _);

		public static SurfaceTypes GetSurfaceFromString(SurfaceMap surfaceMap, string surfaceName)
		{
			if (surfaceMap.containsDuplicate)
			{
				Debug.LogError("The SurfaceMap list contains duplicate items");
				return SurfaceTypes.Default;
			}

			foreach (var keyValuePair in surfaceMap.surfaceList)
			{
				if (keyValuePair.surfaceName == surfaceName) return keyValuePair.surfaceType;
			}

			return SurfaceTypes.Default;
		}
	}
}
