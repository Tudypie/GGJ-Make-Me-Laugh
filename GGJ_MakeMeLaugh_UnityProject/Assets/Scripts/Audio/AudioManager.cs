using FMOD.Studio;
using FMODUnity;
using System.Diagnostics;
using UnityEngine;

namespace GameJam.Audio
{

    public class AudioManager : MonoBehaviour
    {
        [HideInInspector]
        public AudioSource audioSource;

        public static AudioManager Instance { get; private set; }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            audioSource = GetComponent<AudioSource>();
        }

        public void PlayAudio(AudioClip audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        public void PlayAudio(EventReference sound)
        {
            RuntimeManager.PlayOneShot(sound, transform.position);
        }

        public void PlayAudio(EventReference sound, Vector3 worldPos)
        {
            RuntimeManager.PlayOneShot(sound, worldPos);
        }

        public EventInstance CreateInstance(EventReference eventReference)
        {
            EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
            return eventInstance;
        }

    }

}