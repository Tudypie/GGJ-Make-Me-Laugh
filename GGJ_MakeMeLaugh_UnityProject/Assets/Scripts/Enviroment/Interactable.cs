using UnityEngine;
using UnityEngine.Events;
using GameJam.EventSystem;
using GameJam.Utilities.Attributes;

namespace GameJam.Enviroment
{
    public class Interactable : MonoBehaviour
    {
		[Header("Interaction Config:")]
        public string cannotInteractMessage;
        public string interactMessage;
		[Space]
        public bool canInteract;
        [SerializeField, ShowField(nameof(canInteract))] private bool multipleInteractions;
        [SerializeField] private bool useScriptableObjectEvent;
		[Space]
        [SerializeField, ShowField(nameof(useScriptableObjectEvent))] private VoidEvent gameEvent;
        [SerializeField, HideField(nameof(useScriptableObjectEvent))] private UnityEvent unityGameEvent;

		public void Interact()
        {
            if (!multipleInteractions) canInteract = false;

            if (useScriptableObjectEvent)
            {
                gameEvent.Invoke();
            }
            else
            {
                unityGameEvent.Invoke();
            }
        }

		public void ToggleInteraction() => canInteract = !canInteract;

		public void SetCanInteract(bool value) => canInteract = value;

        public void SetInteractionText(string value) => interactMessage = value;
	}
}
