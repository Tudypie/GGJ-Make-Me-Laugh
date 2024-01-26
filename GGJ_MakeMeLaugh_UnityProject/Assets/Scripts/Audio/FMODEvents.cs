using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;


namespace GameJam.Audio
{
    public class FMODEvents : MonoBehaviour
    {
        public static FMODEvents Instance { get; private set; }

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
