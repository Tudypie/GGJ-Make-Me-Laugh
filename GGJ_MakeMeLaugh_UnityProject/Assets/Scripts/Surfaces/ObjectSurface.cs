using UnityEngine;
using GameJam.Utilities.Attributes;

namespace GameJam.Surfaces
{
	public class ObjectSurface : MonoBehaviour
	{
		[SerializeField, Dropdown(nameof(surfaceArray))] private string surface;

		[SerializeField, HideInInspector] private string[] surfaceArray;

		public SurfaceMap surfaceMap;

		void OnValidate() => GetSurfaceTypes();

		public SurfaceMap.SurfaceTypes GetSurface()
		{
			foreach (var surface in surfaceMap.surfaceList)
			{
				if (surface.surfaceName == this.surface) return surface.surfaceType;
			}

			return SurfaceMap.SurfaceTypes.Default;
		}

		private void GetSurfaceTypes()
		{
			if (surfaceMap == null) return;

			surfaceArray = new string[surfaceMap.surfaceList.Count];

			for (int i = 0; i < surfaceMap.surfaceList.Count; i++) surfaceArray[i] = surfaceMap.surfaceList[i].surfaceName;
		}
	}
}
