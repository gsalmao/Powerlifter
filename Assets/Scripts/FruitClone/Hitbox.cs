using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitClone
{
    public class Hitbox : MonoBehaviour
    {
        [SerializeField] private Fruit fruit;

        public void Slice(Vector2 direction) => fruit.Slice(direction);
    }
}
