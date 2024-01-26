using UnityEngine;
using GameJam.Utilities;
using System.Collections.Generic;

namespace GameJam.Surfaces
{
	public class TerrainSurface : MonoBehaviour
	{
		[SerializeField] private List<SurfaceMap.SurfaceTypes> surfaceLayer;

		private TerrainChecker terrainChecker;

		void Awake() => terrainChecker = new();

		public SurfaceMap.SurfaceTypes GetTerrainSurface(Vector3 position)
		{
			var terrainLayer = terrainChecker.GetActiveTerrainTextureId(position);

			if (terrainLayer > surfaceLayer.Count - 1) return SurfaceMap.SurfaceTypes.Default;

			return surfaceLayer[terrainLayer];
		}
 	}
}
