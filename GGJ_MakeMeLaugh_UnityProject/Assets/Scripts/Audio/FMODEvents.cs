using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;


namespace GameJam.Audio
{
    public class FMODEvents : MonoBehaviour
    {
        public static FMODEvents Instance { get; private set; }


        [Header("Player")]
        public EventReference playerFootsteps;
        public EventReference playerJump;
        public EventReference playerLaugh;

        [Header("Shotgun")]
        public EventReference shotgunEquip;
        public EventReference shotgunShoot;

        [Header("Environment")]
        public EventReference doorOpen;
        public EventReference doorClose;
        public EventReference spikesUp;
        public EventReference monitorOn;
        public EventReference monitorOff;

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
        }
    }

}
