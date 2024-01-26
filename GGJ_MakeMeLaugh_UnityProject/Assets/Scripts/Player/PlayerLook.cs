using UnityEngine;
using GameJam.Input;

namespace GameJam.Player
{
    public class PlayerLook : MonoBehaviour
    {
		[SerializeField] private float mouseLockAngle;
		[SerializeField] private float mouseSenstivity;

		private float xRotation;

		private Camera playerCamera;
		private InputMaster inputManager;

		void Start()
		{
			inputManager = InputManager.INPUT;
			playerCamera = GetComponentInChildren<Camera>();
		}

		void Update()
		{
			var mouseLook = inputManager.Player.Look.ReadValue<Vector2>();

			float mouseX = mouseLook.x * mouseSenstivity;
			float mouseY = mouseLook.y * mouseSenstivity;
			
			xRotation -= mouseY;
			xRotation = Mathf.Clamp(xRotation, -mouseLockAngle, mouseLockAngle);

			playerCamera.transform.localRotation = Quaternion.Euler(xRotation, playerCamera.transform.localRotation.y, playerCamera.transform.localRotation.z);
			transform.Rotate(Vector3.up * mouseX);
		}
	}
}
