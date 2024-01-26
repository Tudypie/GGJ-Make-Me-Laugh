using UnityEngine;

namespace GameJam.Utilities
{
	[CreateAssetMenu(fileName = "SoundPack", menuName = "ScriptableObjects/SoundPack")]
	public class SoundPack : ScriptableObject
    {
        public AudioClip[] sounds;
    }
}
