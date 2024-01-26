using UnityEngine;
using GameJam.Audio;
using GameJam.Input;
using FMODUnity;
using FMOD.Studio;

namespace GameJam.Player
{
	public class PlayerMovement : MonoBehaviour
	{
		[Header("Movement Config:")]
		public float defaultPlayerSpeed;
		[SerializeField] private EventReference playerJumpSound;
        [SerializeField] private float jumpHeight;
        [Space]
		[SerializeField] private Transform groundCheckTransform;
		[SerializeField] private LayerMask groundCheckMask;

		private PlayerManager playerManager;
		private InputMaster inputManager;
		private Vector3 velocity;

		public float PlayerSpeed { get; set; }
		public float Mass { get; set; }
		public bool IsInWater { get; set; }
		public bool IsOnLadder { get; set; }
		public bool IsWalking { get; private set; }

		public bool IsGrounded => Physics.CheckSphere(groundCheckTransform.position, 0.45f, groundCheckMask);

		void Awake()
		{
			playerManager = GetComponent<PlayerManager>();
			inputManager = InputManager.INPUT;

            //playerJumpEvent = AudioManager.Instance.CreateInstance(FMODEvents.Instance.playerJump);

            PlayerSpeed = defaultPlayerSpeed;
			Mass = playerManager.defaultMass;
		}

		void Update()
		{
			IsWalking = inputManager.Player.Movement.IsPressed() && IsGrounded;

			if (inputManager.Player.Jump.WasPressedThisFrame()) Jump();

			if (IsGrounded && velocity.y < 0f) velocity.y = -2f;

			if (IsOnLadder && velocity.y < -2f) velocity.y = -2f; // Reset the velocity when on the ladder

			if (!IsGrounded && (playerManager.PlayerControler.collisionFlags & CollisionFlags.Above) != 0) velocity.y = -velocity.y; // Fix for player sticking on ceilings for a short time when jumping

			Move();
		}

		private void Move()
		{
			var movementInput = inputManager.Player.Movement.ReadValue<Vector2>();
			var move = transform.right * movementInput.x + transform.forward * movementInput.y;

			if (IsOnLadder) move.y = Vector3.up.y * movementInput.y;

			if (velocity.y > -30f && !IsOnLadder) velocity.y += Mass * Time.deltaTime; // Add gravity to the player

			playerManager.PlayerControler.Move(PlayerSpeed * Time.deltaTime * move);

			if (!IsOnLadder) playerManager.PlayerControler.Move(velocity * Time.deltaTime);
		}

		private void Jump()
		{
			if (IsGrounded)
			{
				AudioManager.Instance.PlayAudio(playerJumpSound);
				velocity.y = Mathf.Sqrt(jumpHeight * -2f * Mass);
			}
		}
	}
}
