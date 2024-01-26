using UnityEngine;
using UnityEngine.Events;
using GameJam.Utilities.Attributes;

namespace GameJam.EventSystem
{
	public class EventTrigger : MonoBehaviour
	{
		[SerializeField] private bool triggerOnEnter;
		[SerializeField, ShowField(nameof(triggerOnEnter))] private bool destroyOnCollisionEnter;
		[SerializeField, ShowField(nameof(triggerOnEnter))] private bool useScriptableObjectEnter;

		[SerializeField, ConditionalField(ConditionType.AND, nameof(triggerOnEnter), nameof(useScriptableObjectEnter))] 
		private VoidEvent triggerEnterEvent;

		[SerializeField, ConditionalField(ConditionType.AND, new bool[2] { false, true }, nameof(triggerOnEnter), nameof(useScriptableObjectEnter))] 
		private UnityEvent triggerEnterUnityEvent;
		[Space]
		[SerializeField, HideField(nameof(destroyOnCollisionEnter))] 
		private bool triggerOnExit;

		[SerializeField, ConditionalField(ConditionType.AND, new bool[2] { false, true }, nameof(triggerOnExit), nameof(destroyOnCollisionEnter))] 
		private bool destroyOnCollisionExit;

		[SerializeField, ConditionalField(ConditionType.AND, new bool[2] { false, true }, nameof(triggerOnExit), nameof(destroyOnCollisionEnter))] 
		private bool useScriptableObjectExit;

		[SerializeField, ConditionalField(ConditionType.AND, new bool[3] { false, false, true }, nameof(triggerOnExit), nameof(useScriptableObjectExit), nameof(destroyOnCollisionEnter))] 
		private VoidEvent triggerExitEvent;

		[SerializeField, ConditionalField(ConditionType.AND, new bool[3] { false, true, true }, nameof(triggerOnExit), nameof(useScriptableObjectExit), nameof(destroyOnCollisionEnter))] 
		private UnityEvent triggerExitUnityEvent;

		void OnTriggerEnter(Collider collider)
		{
			if (triggerOnEnter && collider.CompareTag("Player"))
			{
				if (useScriptableObjectEnter)
				{
					triggerEnterEvent.Invoke();
				}
				else
				{
					triggerEnterUnityEvent.Invoke();
				}

				if (destroyOnCollisionEnter) Destroy(gameObject);
			}
		}

		void OnTriggerExit(Collider collider)
		{
			if (triggerOnExit && collider.CompareTag("Player"))
			{
				if (useScriptableObjectExit)
				{
					triggerExitEvent.Invoke();
				}
				else
				{
					triggerExitUnityEvent.Invoke();
				}

				if (destroyOnCollisionExit) Destroy(gameObject);
			}
		}
	}
}
