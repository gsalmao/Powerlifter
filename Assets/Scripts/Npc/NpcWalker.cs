using UnityEngine;

namespace Powerlifter.Npcs
{
    public class NpcWalker : Npc
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float speed = 3f;
        [SerializeField] private float walkDistance = 20f;

        private Vector3 _initPosition;

        protected override void Start()
        {
            _initPosition = transform.position;
            animator.Play("Walk");
        }

        private void FixedUpdate()
        {
            rb.velocity = transform.forward * speed;

            if (Vector3.Distance(_initPosition, transform.position) > walkDistance)
                Destroy(gameObject);
        }
        
    }
}
