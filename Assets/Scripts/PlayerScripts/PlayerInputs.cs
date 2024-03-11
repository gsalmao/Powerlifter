using Powerlifter.Input;
using UnityEngine;

namespace Powerlifter.PlayerScripts
{
    /// <summary>
    /// Responsible for wrapping the input of the player.
    /// </summary>
    public class PlayerInputs : MonoBehaviour
    {
        public Vector2 Movement { get; private set; }
        public bool IsMoving => Movement.magnitude > movementThreshold;

        [SerializeField] private float movementThreshold = .3f;

        private Controls _controls;

        private void Awake() => _controls = new Controls();
        private void OnDestroy() => _controls.Dispose();

        private void OnEnable() => _controls.Enable();
        private void OnDisable() => _controls.Disable();

        private void FixedUpdate() => Movement = _controls.Gameplay.Move.ReadValue<Vector2>();
    }
}
