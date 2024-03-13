using UnityEngine;

namespace Powerlifter.Npcs
{
    public class Npc : MonoBehaviour
    {
        [SerializeField] private RagDoll ragDollPrefab;
        [SerializeField] protected Animator animator;
        [SerializeField] private string[] initAnimations;

        protected virtual void Start()
        {
            string animation = initAnimations[Random.Range(0, initAnimations.Length - 1)];
            animator.Play(animation);
        }

        public void ReceivePunch(Vector3 throwDirection)
        {
            var newRagDoll = Instantiate(ragDollPrefab, transform.position, Quaternion.Euler(throwDirection));
            newRagDoll.Init(throwDirection);

            Destroy(gameObject);
        }
    }
}
