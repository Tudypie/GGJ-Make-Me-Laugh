﻿using UnityEngine;
using System.Collections.Generic;

namespace GameJam.EventSystem
{
	public abstract class BaseGameEvent<T> : ScriptableObject
	{
		private readonly List<IGameEventListener<T>> eventListeners = new();

		public void Invoke(T item)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--) eventListeners[i].OnEventRaised(item);
		}

		public void RegisterListener(IGameEventListener<T> listener)
		{
			if (!eventListeners.Contains(listener)) eventListeners.Add(listener);
		}

		public void UnregisterListener(IGameEventListener<T> listener)
		{
			if (eventListeners.Contains(listener)) eventListeners.Remove(listener);
		}
	}
}