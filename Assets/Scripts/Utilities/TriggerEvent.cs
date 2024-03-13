using System;
using UnityEngine;

namespace Powerlifter.Utilities
{
    public class TriggerEvent : MonoBehaviour
    {
        public event Action<Collider> OnEnterTrigger = delegate { };

        private void OnTriggerEnter(Collider other) => OnEnterTrigger(other);
    }
}
