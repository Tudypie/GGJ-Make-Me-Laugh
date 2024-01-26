using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deadshot.Utilities
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
