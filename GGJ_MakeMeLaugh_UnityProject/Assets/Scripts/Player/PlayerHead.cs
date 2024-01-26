using UnityEngine;
using GameJam.Player;

namespace GameJam
{
    public class PlayerHead : MonoBehaviour
    {
		[SerializeField] private Transform head;

		[Header("Bob Config:")]
		public float bobSpeed;
		[SerializeField] private Vector3 bobAmount;

		private float timer;
		private Vector3 headOriginalPos;

		private PlayerManager playerManager;

		void Awake()
		{
			headOriginalPos = head.localPosition;
			playerManager = GetComponent<PlayerManager>();
		}

		void Update()
		{
			// Making the head bob while walking using math thats definetly not copied from somewhere else
			if (playerManager.PlayerMovement.IsWalking)
			{
				timer += Time.deltaTime * bobSpeed;
				head.localPosition = new Vector3(headOriginalPos.x + Mathf.Sin(timer) * bobAmount.x, headOriginalPos.y + Mathf.Sin(timer) * bobAmount.y, headOriginalPos.z + Mathf.Sin(timer) * bobAmount.z);
			}
			else
			{
				timer = 0;
				head.localPosition = new Vector3(Mathf.Lerp(head.localPosition.x, headOriginalPos.x, Time.deltaTime * bobSpeed), Mathf.Lerp(head.localPosition.y, headOriginalPos.y, Time.deltaTime * bobSpeed), Mathf.Lerp(head.localPosition.z, headOriginalPos.z, Time.deltaTime * bobSpeed));
			}
		}
	}
}
