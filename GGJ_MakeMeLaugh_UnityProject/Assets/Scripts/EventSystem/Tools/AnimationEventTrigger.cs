using UnityEngine;

namespace GameJam.EventSystem
{
    public class AnimationEventTrigger : MonoBehaviour
    {
        public void InvokeEvent(VoidEvent gameEvent) => gameEvent.Invoke(); // Called by animation event
    }
}
