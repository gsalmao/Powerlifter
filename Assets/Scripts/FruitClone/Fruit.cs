using System;
using UnityEngine;
using ZHMQ.ObjectPooling;
using Random = UnityEngine.Random;

namespace FruitClone
{
    public class Fruit : MonoBehaviour
    {
        public static event Action OnSliceFruit = delegate { };

        [SerializeField] private Rigidbody2D rb;


        [Header("Sliced Fruit")]
        [SerializeField] private Rigidbody2D[] piecesRb;
        [SerializeField] private GameObject slicedFruit;
        [SerializeField] private Transform[] maskPivots;

        [SerializeField] private GameObject splashPrefab;

        private float lifetime = 5f;
        private float _timeAlive;

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
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

        public void Slice(float angle)
        {
            slicedFruit.transform.SetPositionAndRotation(rb.transform.position, rb.transform.rotation);

            ObjectPool.Spawn(splashPrefab, rb.transform.position);

            foreach(Transform pivot in maskPivots)
                pivot.rotation = Quaternion.Euler(0, 0, angle);

            slicedFruit.SetActive(true);
            
            foreach(Rigidbody2D piece in piecesRb)
            {
                piece.velocity = rb.velocity;                       // * new Vector2(Random.Range(.7f, 1f), Random.Range(.7f, 1f));
                piece.angularVelocity = Random.Range(-45f, 45f);
            }

            rb.gameObject.SetActive(false);
        }

        private void Reset()
        {
            rb.transform.localPosition = default;
            rb.transform.localRotation = default;

            foreach (Transform pivot in maskPivots)
                pivot.rotation = default;

            foreach (Rigidbody2D rb in piecesRb)
                rb.transform.localPosition = default;

            slicedFruit.SetActive(false);
            rb.gameObject.SetActive(true);

            _timeAlive = 0;
        }
    }
}
