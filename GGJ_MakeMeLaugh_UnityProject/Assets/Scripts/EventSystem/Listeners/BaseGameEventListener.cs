using UnityEngine;
using UnityEngine.Events;

namespace GameJam.EventSystem
{
	public abstract class BaseGameEventListener<T, E, UER> : MonoBehaviour, IGameEventListener<T> where E : BaseGameEvent<T> where UER : UnityEvent<T>
	{
		[SerializeField] private E gameEvent;
		[SerializeField] private UER unityEventResponse;

		public E GameEvent { get { return gameEvent; } set { gameEvent = value; } }

		void OnEnable()
		{
			if (gameEvent != null) GameEvent.RegisterListener(this);
		}

		void OnDisable()
		{
			if (gameEvent != null) GameEvent.UnregisterListener(this);
		}

		public void OnEventRaised(T item) => unityEventResponse?.Invoke(item);
	}
}