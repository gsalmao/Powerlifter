using System;
using System.Collections.Generic;
using UnityEngine;
using ZHMQ.ObjectPooling;
using Random = UnityEngine.Random;

namespace FruitClone
{
    public class Fruit : MonoBehaviour
    {
        public static event Action OnSliceFruit = delegate { };

        private static List<Fruit> fruitsList = new();

        [SerializeField] private Rigidbody2D rb;

        [Header("Masks")]
        [SerializeField] private SpriteMask topMask;
        [SerializeField] private SpriteMask bottomMask;

        [Header("Sprite Renderers")]
        [SerializeField] private SpriteRenderer topSprite;
        [SerializeField] private SpriteRenderer bottomSprite;

        [Header("Sliced Fruit")]
        [SerializeField] private Rigidbody2D[] piecesRb;
        [SerializeField] private GameObject slicedFruit;
        [SerializeField] private Transform[] maskPivots;

        [SerializeField] private GameObject splashPrefab;

        private float lifetime = 5f;
        private float _timeAlive;

        private void OnEnable()
        {
            fruitsList.Add(this);

            int currentIndex = fruitsList.Count - 1;

            topMask.backSortingOrder = currentIndex * 6;
            topSprite.sortingOrder = currentIndex * 6 + 1;
            topMask.frontSortingOrder = currentIndex * 6 + 2;

            bottomMask.backSortingOrder = currentIndex * 6 + 3;
            bottomSprite.sortingOrder = currentIndex * 6 + 4;
            bottomMask.frontSortingOrder = currentIndex * 6 + 5;

        }

        private void OnDisable()
        {
            fruitsList.Remove(this);
            Reset();
        }

        private void Update()
        {
            _timeAlive += Time.deltaTime;
            if (_timeAlive > lifetime)
                ObjectPool.ReturnToPool(this);
        }

        public void Init(float force)
        {
            rb.angularVelocity = Random.Range(-180f, 180f);
            rb.velocity = transform.right * force;
        }

        public void Slice(Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            slicedFruit.transform.SetPositionAndRotation(rb.transform.position, rb.transform.rotation);

            ObjectPool.Spawn(splashPrefab, rb.transform.position);

            foreach(Transform pivot in maskPivots)
                pivot.rotation = Quaternion.Euler(0, 0, angle);

            slicedFruit.SetActive(true);
            
            foreach(Rigidbody2D piece in piecesRb)
            {
                piece.velocity = rb.velocity + direction * 2f;
                piece.angularVelocity = Random.Range(-45f, 45f);
            }

            rb.gameObject.SetActive(false);
        }

        private void Reset()
        {
            rb.transform.localPosition = default;
            rb.transform.localRotation = default;

            slicedFruit.transform.SetLocalPositionAndRotation(default, default);

            foreach (Transform pivot in maskPivots)
                pivot.localRotation = default;

            foreach (Rigidbody2D rb in piecesRb)
                rb.transform.SetLocalPositionAndRotation(default, default);

            slicedFruit.SetActive(false);
            rb.gameObject.SetActive(true);

            _timeAlive = 0;
        }
    }
}
