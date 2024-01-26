using UnityEngine;
using GameJam.Utilities.Attributes;

namespace GameJam.Utilities.Components
{
    public class DestroyObject : MonoBehaviour
    {
        [SerializeField] private bool isRandom;
        [Space]
        [SerializeField] private float time;
        [SerializeField, ShowField(nameof(isRandom))] private float time2;

        void OnEnable()
        {
            if (isRandom)
            {
				Invoke(nameof(Destroy), Random.Range(time, time2));
			}
            else
            {
                Invoke(nameof(Destroy), time);
            }
        }

        private void Destroy() => Destroy(gameObject);
    }
}
