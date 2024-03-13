using UnityEngine;
using UnityEngine.Events;

namespace Powerlifter.Utilities
{
    public class EventHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent unityEvent;

        public void Execute() => unityEvent.Invoke();
    }
}
