using TMPro;
using UnityEngine;
using GameJam.Input;
using GameJam.Utilities;
using GameJam.Enviroment;

namespace GameJam.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
		[SerializeField] private int interactionDistance;
		[Space]
		[SerializeField] private LayerMask interactionMask;

		private TMP_Text intercationText;
		private PlayerManager playerManager;

		void Awake()
		{
			playerManager = GetComponent<PlayerManager>();
			intercationText = Utils.FindType<TMP_Text>("InteractionText");
		}

		void Update()
        {
			var ray = playerManager.PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		
			if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactionMask) && hit.collider.CompareTag("Interactable"))
			{
				hit.collider.TryGetComponent(out Interactable interactable);
				
				if (interactable.canInteract)
				{
					intercationText.text = $"Press [E] {interactable.interactMessage}";
				
					if (InputManager.INPUT.Player.Interact.WasPressedThisFrame()) interactable.Interact();
				}
				else
				{
					intercationText.text = interactable.cannotInteractMessage;
				}
			}
			else
			{
				intercationText.text = string.Empty;
			}
		}
	}
}
