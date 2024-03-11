using UnityEngine;

namespace Powerlifter.PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float maxRotationDegrees = 15f;

        [Header("References")]
        [SerializeField] private PlayerInputs playerInputs;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Animator playerModel;
        [SerializeField] private PlayerSettings settings;

        private Vector3 _cameraForward;
        private Vector3 _cameraRight;
        private Vector3 _moveDirection;

        private Quaternion _targetRotation;

        private const string Walking = "Walking";

        private void FixedUpdate()
        {
            if (playerInputs.IsMoving)
                MovePlayer();
            else
                rb.velocity = default;

            playerModel.SetBool(Walking, playerInputs.IsMoving);
        }

        private void Update()
        {
            UpdatePlayerAnimations();
        }

        private void MovePlayer()
        {
            _cameraForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
            _cameraRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;
            _moveDirection = _cameraForward * playerInputs.Movement.y + _cameraRight * playerInputs.Movement.x;
            rb.velocity = _moveDirection.normalized * settings.Speed;
        }

        private void UpdatePlayerAnimations()
        {
            if (playerInputs.IsMoving)
            {
                _targetRotation = Quaternion.LookRotation(_moveDirection.normalized, Vector3.up);
                playerModel.transform.rotation = Quaternion.RotateTowards(playerModel.transform.rotation, _targetRotation, maxRotationDegrees);
            }

            playerModel.SetBool(Walking, playerInputs.IsMoving);
        }
    }
}
