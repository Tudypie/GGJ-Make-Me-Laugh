using UnityEngine;
using GameJam.Surfaces;
using GameJam.Utilities;
using GameJam.Audio;
using FMODUnity;
using FMOD.Studio;

namespace GameJam.Player
{
	public class PlayerFootsteps : MonoBehaviour
	{
		[Header("Footsteps Config:")]
		public float deafultTimeBetwenFootsteps;
		[SerializeField] private LayerMask groundTypeMask;
		[SerializeField] private EventReference footstepSound;

        private float timeBetwenFootsteps;
		private bool overrideSurface;

		private SurfaceMap.SurfaceTypes surfaceType;
		private PlayerManager playerManager;

		void Awake()
		{
			playerManager = GetComponent<PlayerManager>();

            //playerFootstepsEvent = AudioManager.Instance.CreateInstance(FMODEvents.Instance.playerFootsteps);

            timeBetwenFootsteps = deafultTimeBetwenFootsteps;
		}

		void Update()
		{
			// Set the timeBetwenFootsteps to 0 when the player jumps so when he lands it plays a footstep sound
			if (!playerManager.PlayerMovement.IsGrounded) timeBetwenFootsteps = 0f;

			if (playerManager.PlayerMovement.IsWalking) timeBetwenFootsteps -= Time.deltaTime;

			if (!overrideSurface) surfaceType = GetGroundSurface();

			if (timeBetwenFootsteps <= 0f && playerManager.PlayerMovement.IsGrounded && !playerManager.PlayerMovement.IsOnLadder)
			{
				AudioManager.Instance.PlayAudio(footstepSound);
				timeBetwenFootsteps = deafultTimeBetwenFootsteps;
			}
		}

		public SurfaceMap.SurfaceTypes GetGroundSurface()
		{
			if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 2f, groundTypeMask))
			{
				if (hit.collider.TryGetComponent(out TerrainSurface terrainSurface))
				{
					return terrainSurface.GetTerrainSurface(transform.position);
				}
				else if (hit.collider.TryGetComponent(out ObjectSurface objectSurface))
				{
					return objectSurface.GetSurface();
				}
			}

			return SurfaceMap.SurfaceTypes.Default;
		}

		public void OverrideSurface(bool doOverride, SurfaceMap.SurfaceTypes surfaceType = SurfaceMap.SurfaceTypes.Default)
		{
			overrideSurface = doOverride;
			this.surfaceType = surfaceType;
		}
	}
}
