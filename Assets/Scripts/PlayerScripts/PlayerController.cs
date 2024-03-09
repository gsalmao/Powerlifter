using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Powerlifter.Input;

namespace Powerlifter.PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform playerModel;
        [SerializeField] private PlayerSettings settings;

        private Controls _controls;
        private Vector2 _moveInput;
        private Vector3 _cameraForward;
        private Vector3 _cameraRight;
        private Vector3 _moveDirection;

        private void Awake() => _controls = new Controls();
        private void OnDestroy() => _controls.Dispose();
        private void OnEnable() => _controls.Enable();
        private void OnDisable() => _controls.Disable();

        private void FixedUpdate()
        {
            _cameraForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
            _cameraRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;

            _moveInput = _controls.Gameplay.Move.ReadValue<Vector2>();
            _moveDirection = _cameraForward * _moveInput.y + _cameraRight * _moveInput.x;
            
            playerModel.rotation = Quaternion.LookRotation(_moveDirection.normalized, Vector3.up);
            rb.velocity = _moveDirection * settings.Speed;
        }

    }
}
