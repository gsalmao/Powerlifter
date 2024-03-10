using Powerlifter.Input;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ZHMQ.ObjectPooling;

namespace FruitClone
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Fruit prefab;
        [SerializeField] private float force;
        public void Spawn()
        {
            Fruit newFruit = ObjectPool.Spawn(prefab, transform.position, transform.rotation);
            newFruit.Init(force);
        }
    }
}