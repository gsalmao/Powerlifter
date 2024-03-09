using UnityEngine;

namespace Powerlifter.PlayerScripts
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Powerlifter/PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
    }
}
