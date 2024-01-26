using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

namespace GameJam.Utilities
{
	public static class Utils
	{
		/// <summary>
		/// Generates a random integer within a range
		/// </summary>
		/// <param name="min">Minimum range inclusive</param>
		/// <param name="max">Maximum range inclusive</param>
		/// <returns>A random interger</returns>
		public static int RandomIntInclusive(int min, int max) => Random.Range(min, max + 1);

		/// <summary>
		/// Generates a random number thats always different from the last chosen number
		/// </summary>
		/// <param name="min">Minimum range inclusive</param>
		/// <param name="max">Minimum range exclusive</param>
		/// <param name="lastChosenNumber">Reference to set the last chosen number</param>
		/// <returns>A random different number</returns>
		public static int RandomRangeNoDupe(int min, int max, ref int lastChosenNumber)
		{
			int generatedNumber = 0;

			while (generatedNumber == lastChosenNumber) generatedNumber = Random.Range(min, max);

			lastChosenNumber = generatedNumber;

			return generatedNumber;
		}

		/// <summary>
		/// Generates a random Vector3 within a range
		/// </summary>
		/// <param name="min">Minimum range inclusive</param>
		/// <param name="max">Maximum range inclusive</param>
		/// <param name="excludeX">Don't generate X value</param>
		/// <param name="excludeY">Don't generate Y value</param>
		/// <param name="excludeZ">Don't generate Z value</param>
		/// <returns>A random Vector3</returns>
		public static Vector3 RandomVector(float min, float max, bool excludeX = false, bool excludeY = false, bool excludeZ = false) => new(excludeX ? 0f : Random.Range(min, max), excludeY ? 0f : Random.Range(min, max), excludeZ ? 0f : Random.Range(min, max));

		/// <summary>
		/// Generates a random number between the X and Y values of Vector2, inclusive
		/// </summary>
		/// <param name="vector">The Vector2 to get the range from</param>
		/// <returns>A random float</returns>
		public static float RandomFromVector2(Vector2 vector) => Random.Range(vector.x, vector.y);

		/// <summary>
		/// Generates a random point on a navmesh
		/// </summary>
		/// <param name="origin">The origin to generate the random position from</param>
		/// <param name="range">How far from the origin to generate the point</param>
		/// <param name="areaMask">Navmesh surface area mask</param>
		/// <returns>A Vector3 with the random position</returns>
		public static Vector3 RandomNavmeshPoint(Vector3 origin, float range, int areaMask)
		{
			var randomDirection = Random.insideUnitSphere * range;

			randomDirection += origin;

			NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, range, areaMask);

			return navHit.position;
		}

		/// <summary>
		/// Prints the items of a collection
		/// </summary>
		/// <param name="collection">The collection</param>
		public static void PrintCollection(ICollection collection)
		{
			foreach (var item in collection) Debug.Log(item.ToString());
		}

		/// <summary>
		/// Checks a collection for a specified type
		/// </summary>
		/// <typeparam name="T">The type of the collection</typeparam>
		/// <param name="collection">The collection to look in</param>
		/// <param name="itemValue">The item value to look for</param>
		/// <returns>True if the item value exists in the collection</returns>
		public static bool CheckCollection<T>(ICollection<T> collection, T itemValue)
		{
			foreach (var item in collection)
			{
				if (item.Equals(itemValue)) return true;
			}

			return false;
		}

		/// <summary>
		/// Checks a list of structs for duplicate items, the struct must implement IEquatable
		/// </summary>
		/// <typeparam name="T">The struct implementing IEquatable</typeparam>
		/// <param name="structList">The list to check</param>
		/// <param name="foundMaches">The amount of matches found</param>
		/// <returns>True if duplicates are found</returns>
		public static bool CheckStructListDuplicate<T>(List<T> structList, out int foundMaches) where T : struct, IEquatable<T>
		{
			var dupeDataList = new List<T>();

			dupeDataList = structList.FindAll((T structData) => {

				int matchCount = 0;

				foreach (var data in structList)
				{
					if (structData.Equals(data)) matchCount++;
				}

				return matchCount > 1;
			});

			foundMaches = dupeDataList.Count;

			return dupeDataList.Count > 1;
		}

		/// <summary>
		/// Checks if the <paramref name="collection"/> is null or empty
		/// </summary>
		/// <param name="collection">The collection to check</param>
		/// <returns>True if is null or empty, false if not</returns>
		public static bool IsCollectionNullOrEmpty(ICollection collection) => collection == null || collection.Count == 0;

		/// <summary>
		/// Enables a single object from a list of objects
		/// </summary>
		/// <param name="objects">Array of objects to toggle from</param>
		/// <param name="index">Object index to toggle on</param>
		public static void ToggleObject(GameObject[] objects, int index)
		{
			foreach (var @object in objects) @object.SetActive(false);

			objects[index].SetActive(true);
		}

		/// <summary>
		/// Toggles a particle system
		/// </summary>
		/// <param name="particleSystem">The particles to toggle</param>
		/// <param name="state">The state of the particles</param>
		public static void ToggleParticleSystem(ParticleSystem particleSystem, bool state)
		{
			if (state && particleSystem.isStopped)
			{
				particleSystem.Play();
			}
			else if (particleSystem.isPlaying)
			{
				particleSystem.Stop();
			}
		}

		/// <summary>
		/// Toggles an audio source
		/// </summary>
		/// <param name="audioSource">The audioSource to toggle</param>
		/// <param name="state">The state of the audio</param>
		public static void ToogleAudio(AudioSource audioSource, bool state)
		{
			if (state)
			{
				audioSource.Play();
			}
			else
			{
				audioSource.Stop();
			}
		}

		/// <summary>
		/// Toggles two materials on a renderer
		/// </summary>
		/// <param name="renderer">The object's renderer</param>
		/// <param name="firstMaterial">The first material</param>
		/// <param name="secondMaterial">The second material</param>
		public static void SwitchMaterial(Renderer renderer, Material firstMaterial, Material secondMaterial)
		{
			if (renderer.sharedMaterial == firstMaterial)
			{
				renderer.material = secondMaterial;
			}
			else
			{
				renderer.material = firstMaterial;
			}
		}

		/// <summary>
		/// Plays an audio clip removing any previously set pitch
		/// </summary>
		/// <param name="audioSource">Targeted audio source</param>
		/// <param name="audioClip">Audio clip to play</param>
		public static void PlaySoundWithNoPitch(AudioSource audioSource, AudioClip audioClip)
		{
			audioSource.pitch = 1f;

			audioSource.PlayOneShot(audioClip);
		}

		/// <summary>
		/// Plays an audio clip with a random pitch
		/// </summary>
		/// <param name="audioSource">Targeted audio source</param>
		/// <param name="audioClip">Audio clip to play</param>
		/// <param name="minPitch">Minimum pitch</param>
		/// <param name="maxPitch">Maximum pitch</param>
		public static void PlaySoundWithRandomPitch(AudioSource audioSource, AudioClip audioClip, float minPitch, float maxPitch)
		{
			audioSource.pitch = Random.Range(minPitch, maxPitch);

			audioSource.PlayOneShot(audioClip);
		}

		/// <summary>
		/// Plays a random sound from a list of clips
		/// </summary>
		/// <param name="audioSource">Targeted audioSource</param>
		/// <param name="audioClips">Audio clip array</param>
		public static void PlayRandomSound(AudioSource audioSource, List<AudioClip> audioClips) => PlayRandomSound(audioSource, audioClips.ToArray());

		/// <summary>
		/// Plays a random sound from an array of clips
		/// </summary>
		/// <param name="audioSource">Targeted audioSource</param>
		/// <param name="audioClips">Audio clip array</param>
		public static void PlayRandomSound(AudioSource audioSource, AudioClip[] audioClips)
		{
			audioSource.pitch = 1f;

			audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
		}

		/// <summary>
		/// Plays a random audio clip with a random pitch
		/// </summary>
		/// <param name="audioSource">Targeted audio source</param>
		/// <param name="audioClips">Audio clip list</param>
		/// <param name="minPitch">Minimum pitch</param>
		/// <param name="maxPitch">Maximum pitch</param>
		public static void PlayRandomSoundWithRandomPitch(AudioSource audioSource, List<AudioClip> audioClips, float minPitch, float maxPitch) => PlayRandomSoundWithRandomPitch(audioSource, audioClips.ToArray(), minPitch, maxPitch);

		/// <summary>
		/// Plays a random audio clip with a random pitch
		/// </summary>
		/// <param name="audioSource">Targeted audio source</param>
		/// <param name="audioClips">Audio clip array</param>
		/// <param name="minPitch">Minimum pitch</param>
		/// <param name="maxPitch">Maximum pitch</param>
		public static void PlayRandomSoundWithRandomPitch(AudioSource audioSource, AudioClip[] audioClips, float minPitch, float maxPitch)
		{
			audioSource.pitch = Random.Range(minPitch, maxPitch);

			audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
		}

		/// <summary>
		/// Compares two vectors to see if they are similar
		/// </summary>
		/// <param name="firstVector">The first vector to compare</param>
		/// <param name="secondVector">The second vector to compare</param>
		/// <returns>True if all vector values are similar</returns>
		public static bool ApproximateVector(Vector3 firstVector, Vector3 secondVector) => Mathf.Approximately(firstVector.x, secondVector.x) && Mathf.Approximately(firstVector.y, secondVector.y) && Mathf.Approximately(firstVector.z, secondVector.z);

		/// <summary>
		/// Returns the floor for X Y Z vector values
		/// </summary>
		/// <param name="vector">The vector to floor</param>
		/// <returns>A floored vector</returns>
		public static Vector3 FloorVector(Vector3 vector)
		{
			vector.x = Mathf.Floor(vector.x);
			vector.y = Mathf.Floor(vector.y);
			vector.z = Mathf.Floor(vector.z);

			return vector;
		}

		/// <summary>
		/// Returns the ceil for the X Y Z vector values
		/// </summary>
		/// <param name="vector">The vector to ceil</param>
		/// <returns>A ceiled vector</returns>
		public static Vector3 CeilVector(Vector3 vector)
		{
			vector.x = Mathf.Ceil(vector.x);
			vector.y = Mathf.Ceil(vector.y);
			vector.z = Mathf.Ceil(vector.z);

			return vector;
		}

		/// <summary>
		/// Gets the child of a child by index
		/// </summary>
		/// <param name="parent">The parent of the children</param>
		/// <param name="firstChildIndex">The index of the first child</param>
		/// <param name="secondChildIndex">The index of the second child</param>
		/// <returns>The grandchild's transform</returns>
		public static Transform GetGrandchildByIndex(Transform parent, int firstChildIndex = 0, int secondChildIndex = 0) => parent.GetChild(firstChildIndex).GetChild(secondChildIndex);

		/// <summary>
		/// Gets the child of a child by index
		/// </summary>
		/// <param name="parent">The parent of the children</param>
		/// <param name="firstChildName">The name of the first child</param>
		/// <param name="secondChildIndex">The index of the second child</param>
		/// <returns>The grandchild's transform</returns>
		public static Transform GetGrandchildByIndex(Transform parent, string firstChildName, int secondChildIndex = 0) => parent.Find(firstChildName).GetChild(secondChildIndex);

		/// <summary>
		/// Gets the child of a child by name
		/// </summary>
		/// <param name="parent">The parent of the children</param>
		/// <param name="firstChildName">The name of the first child</param>
		/// <param name="secondChildName">The name of the second child</param>
		/// <returns>The grandchild's transform</returns>
		public static Transform GetGrandchildByName(Transform parent, string firstChildName, string secondChildName) => parent.Find(firstChildName).Find(secondChildName);

		/// <summary>
		/// Gets the child of a child by name
		/// </summary>
		/// <param name="parent">The parent of the children</param>
		/// <param name="secondChildName">The name of the second child</param>
		/// <param name="firstChildIndex">The index of the first child</param>
		/// <returns>The grandchild's transform</returns>
		public static Transform GetGrandchildByName(Transform parent, string secondChildName, int firstChildIndex = 0) => parent.GetChild(firstChildIndex).Find(secondChildName);

		/// <summary>
		/// Execute logic once
		/// </summary>
		/// <param name="logic">The logic to execute</param>
		/// <param name="wasExecuted">Set the referenced bool to false to execute again</param>
		public static void DoOnce(Action logic, ref bool wasExecuted)
		{			
			if (!wasExecuted)
			{
				wasExecuted = true;
				logic.Invoke();
			}
		}

		/// <summary>
		/// Finds the specified type <typeparamref name="T"/> from an object's name, can also be used to find disabled GameObjects. This is an expensive function, shouldn't be used in the update methods
		/// </summary>
		/// <typeparam name="T">The type you want to look for</typeparam>
		/// <param name="name">The name of the object you need</param>
		/// <returns>The desired type</returns>
		public static T FindType<T>(string name) where T : Object
		{
			var objects = Object.FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None);

			foreach (var @object in objects)
			{
				if (@object.name != name) continue;

				return @object;
			}

			return null;
		}
	}
}
