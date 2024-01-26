#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using GameJam.Utilities.Attributes;
using Random = UnityEngine.Random;

namespace GameJam.Utilities.Components
{
	public class PrefabSpawner : MonoBehaviour
	{
		private enum OffsetType
		{
			Exact,
			Random
		}

		[Serializable]
		private struct SpawnerValues
		{
			public GameObject prefab;
			public int spawnChance;
		}

		[Serializable]
		private struct PositionOffset 
		{
			public Vector2 XMinMax;
			public Vector2 YMinMax;
			public Vector2 ZMinMax;
		}

		[Serializable]
		private struct RotationOffset 
		{
			public Vector2 XMinMax;
			public Vector2 YMinMax;
			public Vector2 ZMinMax;
		}

		[SerializeField, Tooltip("Leave empty to not parent to anything")] private Transform objectToParent;
		[SerializeField] private bool destroyAfterSpawning;
		[Space]
		[SerializeField] private List<SpawnerValues> prefabList;
		[Space]
		[SerializeField] private OffsetType positionOffsetType;
		[SerializeField, ShowField(nameof(positionOffsetType), (int)OffsetType.Exact)] private Vector3 posOffset;
		[SerializeField, ShowField(nameof(positionOffsetType), (int)OffsetType.Random)] private PositionOffset positionOffset;
		[Space]
		[SerializeField] private OffsetType rotationOffsetType;
		[SerializeField, ShowField(nameof(rotationOffsetType), (int)OffsetType.Exact)] private Vector3 rotOffset;
		[SerializeField, ShowField(nameof(rotationOffsetType), (int)OffsetType.Random)] private RotationOffset rotationOffset;
		[Space]
		[SerializeField, Button(nameof(AddPoints), "Add Points")] private Void addPointsButton;
		[SerializeField, Button(nameof(SpawnPrefabs), "Spawn Prefabs")] private Void spawnPrefabsButton;
		[SerializeField, Button(nameof(RemoveSpawnedPrefabs), "Remove Spawned Prefabs")] private Void removePrefabsButton;

		private List<Transform> spawnPoints;
		private List<GameObject> spawnedPrefabs = new();

		[ExecuteInEditMode]
		void Awake()
		{
			var @event = Event.current;

			if (@event?.type == EventType.Used && @event.commandName == "Duplicate")
			{
				spawnPoints.Clear();
				spawnedPrefabs.Clear();
				AddPoints();
			}
		}

		void OnDrawGizmos()
		{
			if (Utils.IsCollectionNullOrEmpty(spawnPoints)) return;

			Gizmos.color = Color.green;

			foreach (var point in spawnPoints)
			{
				if (point == null)
				{
					spawnPoints.Remove(point);
					break;
				}				

				Gizmos.DrawWireCube(point.transform.position, new Vector3(0.3f, 0.3f, 0.3f));
			}
		}

		public void AddPoints()
		{
			if (transform.childCount != 0)
			{
				spawnPoints = GetComponentsInChildren<Transform>().ToList();

				spawnPoints.RemoveAt(0); // GetComponentsInChildren also gets the component of the object this script is attached to and we dont want that
			}
			else
			{
				Debug.LogWarning("The spawner has no children");
			}
		}

		public void SpawnPrefabs()
		{
			if (Utils.IsCollectionNullOrEmpty(spawnPoints))
			{
				Debug.LogWarning("There are no spawn points added, make you pressed the \'Add Points\' button in the inspector");
				return;
			}
			else if (Utils.IsCollectionNullOrEmpty(prefabList))
			{
				Debug.LogWarning("Prefab list is empty");
				return;
			}

			RemoveSpawnedPrefabs(); // Destroy previosuly spawned prefabs so it works like a reroll

			foreach (var point in spawnPoints)
			{
				var prefabToSpawn = GenerateRandomPrefab();

				if (prefabToSpawn == null)
				{
					Debug.LogWarning("There is a null prefab, make sure all prefab fields are assigned");
					return;
				}

				var spawnedPrefab = PrefabUtility.InstantiatePrefab(prefabToSpawn) as GameObject;

				spawnedPrefab.name = prefabToSpawn.name; // Get rid of the "(Clone)" sufix

				spawnedPrefab.transform.SetPositionAndRotation(GetPositionOffset(point.transform.position), GetRotationOffset());
				spawnedPrefab.transform.SetParent(objectToParent);

				spawnedPrefabs.Add(spawnedPrefab);
			}

			if (destroyAfterSpawning) DestroyImmediate(gameObject);			
		}

		public void RemoveSpawnedPrefabs()
		{
			foreach (var prefab in spawnedPrefabs) DestroyImmediate(prefab);			

			spawnedPrefabs.Clear();
		}

		private Vector3 GetPositionOffset(Vector3 pointPosition)
		{
			return positionOffsetType switch
			{
				OffsetType.Exact => posOffset + pointPosition,
				OffsetType.Random => new Vector3(Utils.RandomFromVector2(positionOffset.XMinMax), Utils.RandomFromVector2(positionOffset.YMinMax), Utils.RandomFromVector2(positionOffset.ZMinMax)) + pointPosition,
				_ => pointPosition,
			};
		}

		private Quaternion GetRotationOffset()
		{
			return rotationOffsetType switch
			{
				OffsetType.Exact => Quaternion.Euler(rotOffset),
				OffsetType.Random => Quaternion.Euler(Utils.RandomFromVector2(rotationOffset.XMinMax), Utils.RandomFromVector2(rotationOffset.YMinMax), Utils.RandomFromVector2(rotationOffset.ZMinMax)),
				_ => Quaternion.identity,
			};
		}

		private GameObject GenerateRandomPrefab()
		{
			while (true)
			{
				var randomNumber = Utils.RandomIntInclusive(1, 100);
				var randomIndex = Random.Range(0, prefabList.Count);

				if (prefabList[randomIndex].spawnChance >= randomNumber)
				{
					return prefabList[randomIndex].prefab;
				}
				else if (prefabList[randomIndex].spawnChance == 0 && prefabList.Count == 1)
				{
					Debug.LogWarning("The only prefab in the list has a 0 chance of spawning, make sure the spawn chance is at least 1, null has been returned");
					return null;
				}
			}
		}
	}
}
#endif