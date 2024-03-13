using UnityEngine;

namespace Powerlifter.Utilities
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private float anglesPerSecond;

        void Update() => transform.Rotate(0f, anglesPerSecond * Time.deltaTime, 0f);
    }
}
