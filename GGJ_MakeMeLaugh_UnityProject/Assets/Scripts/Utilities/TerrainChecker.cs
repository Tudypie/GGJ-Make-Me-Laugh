using UnityEngine;

namespace GameJam.Utilities
{
	public class TerrainChecker
	{
		private int alphamapWidth;
		private int alphamapHeight;
		private int textureNumber;
		private float[,,] splatmapData;

		private TerrainData terrainData;

		public TerrainChecker()
		{
			terrainData = Terrain.activeTerrain.terrainData;

			alphamapWidth = terrainData.alphamapWidth;
			alphamapHeight = terrainData.alphamapHeight;

			splatmapData = terrainData.GetAlphamaps(0, 0, alphamapWidth, alphamapHeight);
			textureNumber = splatmapData.Length / (alphamapWidth * alphamapHeight);
		}

		private Vector3 ConvertToSplatMapCoordinate(Vector3 worldPosition)
		{
			var splatPosition = new Vector3();
			var terrain = Terrain.activeTerrain;
			var terrainPosition = terrain.transform.position;

			splatPosition.x = ((worldPosition.x - terrainPosition.x) / terrain.terrainData.size.x) * terrain.terrainData.alphamapWidth;
			splatPosition.z = ((worldPosition.z - terrainPosition.z) / terrain.terrainData.size.z) * terrain.terrainData.alphamapHeight;

			return splatPosition;
		}

		public int GetActiveTerrainTextureId(Vector3 position)
		{
			var terrainCordinates = ConvertToSplatMapCoordinate(position);
			var activeTerrainIndex = 0;
			var largestOpacity = 0f;

			for (int i = 0; i < textureNumber; i++)
			{
				if (largestOpacity < splatmapData[(int)terrainCordinates.z, (int)terrainCordinates.x, i])
				{
					activeTerrainIndex = i;
					largestOpacity = splatmapData[(int)terrainCordinates.z, (int)terrainCordinates.x, i];
				}
			}

			return activeTerrainIndex;
		}
	}
}
