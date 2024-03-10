using Powerlifter.Input;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ZHMQ.ObjectPooling;

namespace FruitClone
{
    public class DebugFruits : MonoBehaviour
    {
        [SerializeField] private Transform fruitSpawnPoint;
        [SerializeField] private Fruit prefab;
        [SerializeField] private float force;
        [SerializeField] private float sliceAngle;

        private List<Fruit> fruits = new();
        private Controls _controls;

        private void Awake() => _controls = new Controls();
        private void OnDestroy() => _controls.Dispose();

        private void OnEnable()
        {
            _controls.Enable();
            _controls.Gameplay.Throw.performed += SpawnFruit;
            _controls.Gameplay.Slice.performed += KillAll;
        }
        private void OnDisable()
        {
            _controls.Gameplay.Throw.performed -= SpawnFruit;
            _controls.Gameplay.Slice.performed -= KillAll;
            _controls.Disable();
        }

        private void SpawnFruit(InputAction.CallbackContext ctx)
        {
            Fruit newFruit = ObjectPool.Spawn(prefab, fruitSpawnPoint.position, fruitSpawnPoint.rotation);
            newFruit.Init(force);
            fruits.Add(newFruit);
        }

        private void KillAll(InputAction.CallbackContext ctx)
        {
            foreach(Fruit fruit in fruits)
                fruit.Slice(sliceAngle);

            fruits.Clear();
        }
    }
}