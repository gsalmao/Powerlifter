using Powerlifter.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FruitClone
{
    public class Blade : MonoBehaviour
    {
        [SerializeField] private Transform bladeTrigger;

        private Camera _camera;
        private Controls _controls;

        private Vector3 _mousePosition;
        private Vector3 _worldPosition;

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

            _worldPosition = _camera.ScreenToWorldPoint(
                new Vector3(
                    _mousePosition.x,
                    _mousePosition.y,
                    _camera.nearClipPlane));

            _worldPosition.z = 0f;

            bladeTrigger.position = _worldPosition;
        }

        private void ActivateBlade(InputAction.CallbackContext ctx)
        {
            bladeTrigger.gameObject.SetActive(true);
        }

        private void DeactivateBlade(InputAction.CallbackContext ctx)
        {
            bladeTrigger.gameObject.SetActive(false);
        }
    }
}
