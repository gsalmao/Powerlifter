using UnityEngine;

namespace Powerlifter.PlayerScripts
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Powerlifter/PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; } = 5f;
        [field: SerializeField] public float MaxRotationDegrees { get; private set; } = 15f;
        [field: SerializeField] public float PunchDuration { get; private set; } = .8f;
        [field: SerializeField] public float PunchForce { get; private set; } = 2f;
    }
}
