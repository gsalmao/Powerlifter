
using UnityEngine;

namespace Powerlifter.Npcs
{
    public class NpcSpawner : MonoBehaviour
    {
        [SerializeField] private NpcWalker npcPrefab;
        [SerializeField] private float minWaitTime = 3f;
        [SerializeField] private float maxWaitTime = 6f;

        private float _timer;

        private void Start() => ResetTimer();
        
        private void Update()
        {
            _timer -= Time.deltaTime;

            if(_timer <= 0f)
            {
                Spawn();
                ResetTimer();
            }
        }

        private void ResetTimer() => _timer = Random.Range(minWaitTime, maxWaitTime);
        private void Spawn() => Instantiate(npcPrefab, transform.position, transform.rotation);
    }
}
