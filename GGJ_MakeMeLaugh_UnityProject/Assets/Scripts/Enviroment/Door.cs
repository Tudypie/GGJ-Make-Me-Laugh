using System.Collections;
using UnityEngine;
using FMODUnity;
using GameJam.Audio;

namespace GameJam.Enviroment
{
    public class Door : MonoBehaviour
    {
        private Animator anim;
        private Interactable interactable;
        private Transform player;

        bool canInteract = true;
        bool open = false;
        float interactDelay = 0.6f;

        [Header("Settings")]
        [SerializeField] private bool checkForPlayerPos = true;

        [Header("Audio")]
        [SerializeField] private bool overrideDefaultSounds = false;
        [SerializeField] private EventReference openSound;
        [SerializeField] private EventReference closeSound;

        public bool Open { get { return open; } }

        #region default methods

        private void Start()
        {
            anim = GetComponent<Animator>();
            interactable = GetComponent<Interactable>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        #endregion

        #region custom methods

        public void InteractDoor()
        {
            if (!canInteract)
                return;

            canInteract = false;
            interactable.ToggleInteraction();
            Invoke("InteractDelay", interactDelay);

            if (!open)
            {
                interactable.SetInteractionText("close door");
                anim.SetBool("Close", false);
                if (checkForPlayerPos)
                    anim.SetInteger("Open", CheckPlayerPosition());
                else
                    anim.SetBool("Open", true);

                if (overrideDefaultSounds)
                {
                    AudioManager.Instance.PlayAudio(openSound, transform.position);
                }
                else
                {
                    AudioManager.Instance.PlayAudio(FMODEvents.Instance.doorOpen, transform.position);
                }
            }
            else
            {
                interactable.SetInteractionText("open door");
                if (checkForPlayerPos)
                    anim.SetInteger("Open", 0);
                else
                    anim.SetBool("Open", false);

                anim.SetBool("Close", true);

                if (overrideDefaultSounds)
                {
                    AudioManager.Instance.PlayAudio(closeSound, transform.position);
                }
                else
                {
                    AudioManager.Instance.PlayAudio(FMODEvents.Instance.doorClose, transform.position);
                }
            }

            open = !open;
        }

        void InteractDelay()
        {
            canInteract = true;
            interactable.ToggleInteraction();
        }

        private int CheckPlayerPosition()
        {
            Vector3 toTarget = (player.position - transform.position).normalized;
            return Vector3.Dot(toTarget, transform.forward) > 0 ? 1 : -1;
        }

        private bool AnimationFinished()
        {
            return anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
        }

        private void SetAnimatorSpeed(float speed = 1)
        {
            anim.speed = speed;
        }

        public void OpenDoor()
        {
            if (open) { return; }

            anim.SetInteger("Open", 1);
            anim.SetBool("Close", false);
            AudioManager.Instance.PlayAudio(FMODEvents.Instance.doorOpen, transform.position);
            open = true;
        }

        public void CloseDoor()
        {
            if (!open) { return; }

            anim.SetInteger("Open", 0);
            anim.SetBool("Close", true);
            AudioManager.Instance.PlayAudio(FMODEvents.Instance.doorClose, transform.position);
            open = false;
        }

        #endregion
    }

}