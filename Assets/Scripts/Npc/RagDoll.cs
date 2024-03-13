using UnityEngine;

namespace Powerlifter.Npcs
{
    public class RagDoll : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] rigidbodies;
        [SerializeField] private float minTimeToPickup = .3f;
        [SerializeField] private Collider pickUpCollider;
        [SerializeField] private float lifetime = 20f;

        private float _timer;

        public void Init(Vector3 throwDirection)
        {
            _timer = minTimeToPickup;

            foreach (var rigidbody in rigidbodies)
                rigidbody.AddForce(throwDirection, ForceMode.Impulse);
        }

        public void Destroy() => Destroy(gameObject);

        private void Update()
        {
            _timer -= Time.deltaTime;

            if(_timer > 0f)
                return;
            
            if(_timer < (minTimeToPickup - lifetime))
                Destroy(gameObject);

            pickUpCollider.gameObject.SetActive(true);
            enabled = false;
        }
    }
}
