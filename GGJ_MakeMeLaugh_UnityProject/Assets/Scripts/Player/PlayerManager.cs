using UnityEngine;
using GameJam.Input;
using UnityEngine.SceneManagement;
using FMODUnity;

namespace GameJam.Player
{
    public class PlayerManager : MonoBehaviour
    {
		public float defaultMass;

		public StudioEventEmitter PlayerAudio;
		public CharacterController PlayerControler { get; private set; }
		public PlayerFootsteps PlayerFootsteps { get; private set; }
		public PlayerMovement PlayerMovement { get; private set; }
		public PlayerLook PlayerLook { get; private set; }
		public Camera PlayerCamera { get; private set; }

        void Awake()
        {
			PlayerCamera = Camera.main;

			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;

			PlayerControler = GetComponent<CharacterController>();
			PlayerFootsteps = GetComponent<PlayerFootsteps>();
			PlayerMovement = GetComponent<PlayerMovement>();
			PlayerLook = GetComponent<PlayerLook>();;
		}

		void Update()
		{
			if (InputManager.INPUT.UI.Back.WasPressedThisFrame()) LoadScene("MainMenu");
		}

		public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName); // Called by event
	}
}
