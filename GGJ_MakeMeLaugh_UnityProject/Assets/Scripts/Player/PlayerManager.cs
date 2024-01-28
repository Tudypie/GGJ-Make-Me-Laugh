using UnityEngine;
using GameJam.Input;
using UnityEngine.SceneManagement;
using FMODUnity;

namespace GameJam.Player
{
    public class PlayerManager : MonoBehaviour
    {
		public float defaultMass;
		public CharacterController PlayerControler { get; private set; }
		public PlayerFootsteps PlayerFootsteps { get; private set; }
		public PlayerMovement PlayerMovement { get; private set; }
		public PlayerLook PlayerLook { get; private set; }
		public PlayerShotgun PlayerShotgun { get; private set; }
		public Camera PlayerCamera { get; private set; }

		public static PlayerManager Instance { get; private set; }

        void Awake()
        {
			Instance = this;

			PlayerCamera = Camera.main;

			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;

			PlayerControler = GetComponent<CharacterController>();
			PlayerFootsteps = GetComponent<PlayerFootsteps>();
			PlayerMovement = GetComponent<PlayerMovement>();
			PlayerLook = GetComponent<PlayerLook>();;
			PlayerShotgun = GetComponent<PlayerShotgun>();
		}

		void Update()
		{
			//if (InputManager.INPUT.UI.Back.WasPressedThisFrame()) LoadScene("MainMenu");
		}

		public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName); // Called by event
	}
}
