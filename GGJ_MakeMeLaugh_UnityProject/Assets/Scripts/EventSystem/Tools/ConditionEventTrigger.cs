using UnityEngine;
using UnityEngine.Events;
using GameJam.Utilities.Attributes;

namespace GameJam.EventSystem
{
    public class ConditionEventTrigger : MonoBehaviour
    {
        [SerializeField] private int conditionCount;
        [Space]
        [SerializeField] private bool useScriptableObjectEvent;
        [SerializeField, ShowField(nameof(useScriptableObjectEvent))] private VoidEvent gameEvent;
        [SerializeField, HideField(nameof(useScriptableObjectEvent))] private UnityEvent unityEvent;

        private bool hasCalledEvent;
        private bool[] conditions;

        void Awake() => conditions = new bool[conditionCount];

		void Update()
        {
            if (!hasCalledEvent && CheckCondition())
            {
                if (useScriptableObjectEvent)
                {
                    gameEvent.Invoke();
                }
                else
                {
                    unityEvent.Invoke();
                }

                hasCalledEvent = true;
            }
        }

		public void ToggleCondition(int conditionIndex) => conditions[conditionIndex] = !conditions[conditionIndex]; // Called by event

		private bool CheckCondition()
        {
            foreach (var condition in conditions)
            {
                if (!condition) return false;
            }

            return true;
        }
	}
}
