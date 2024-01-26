using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;


namespace GameJam.Audio
{
    public class FMODEvents : MonoBehaviour
    {
        public static FMODEvents Instance { get; private set; }

        [Header("Player Sounds")]
        public EventReference playerFootsteps;
        public EventReference playerJump;

        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
    }

}
