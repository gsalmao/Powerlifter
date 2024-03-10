using Powerlifter.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ZHMQ.ObjectPooling;

namespace FruitClone
{
    public class Blade : MonoBehaviour
    {
        public static event Action OnReleaseButton = delegate { };

        [SerializeField] private BladeTrail bladeTrailPrefab;
        [SerializeField] private Collider2D bladeTrigger;
        [SerializeField] private float minSliceVelocity;

        private Camera _camera;
        private Controls _controls;

        private Vector3 _mousePosition;
        private Vector3 _worldPosition;
        private Vector3 _direction;
        private float _velocity;
        private bool _buttonDown;

        private void Awake()
        {
            _camera = Camera.main;
            _controls = new Controls();
        }
        private void OnDestroy() => _controls.Dispose();

        private void OnEnable()
        {
            _controls.Enable();
            _controls.Gameplay.Slice.performed += ActivateBlade;
            _controls.Gameplay.Slice.canceled += DeactivateBlade;
        }

        private void OnDisable()
        {
            _controls.Gameplay.Slice.performed -= ActivateBlade;
            _controls.Gameplay.Slice.canceled -= DeactivateBlade;
            _controls.Disable();
        }

        private void FixedUpdate()
        {
            _mousePosition = Mouse.current.position.ReadValue();
            _mousePosition.z = 10f;
            _worldPosition = _camera.ScreenToWorldPoint(_mousePosition);
            _direction = _worldPosition - transform.position;
            _velocity = _direction.magnitude;

            transform.position = _worldPosition;
            bladeTrigger.enabled = _buttonDown && _velocity > minSliceVelocity;
        }

        private void ActivateBlade(InputAction.CallbackContext ctx)
        {
            var newTrail = ObjectPool.Spawn(bladeTrailPrefab, transform.position, default);
            newTrail.Init(transform);
            _buttonDown = true;
        }

        private void DeactivateBlade(InputAction.CallbackContext ctx)
        {
            OnReleaseButton();
            _buttonDown = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.GetComponent<Hitbox>()?.Slice(_direction);
        }
    }
}
