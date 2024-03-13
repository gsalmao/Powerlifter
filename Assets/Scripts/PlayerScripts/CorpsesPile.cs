using System;
using System.Collections.Generic;
using UnityEngine;

namespace Powerlifter.PlayerScripts
{
    public class CorpsesPile : MonoBehaviour
    {
        private static event Action OnReleaseCorpses = delegate { };

        public static int CorpseCount = 0;

        [SerializeField] private List<Transform> corpses = new();
        [SerializeField] private Corpse corpsePrefab;
        [SerializeField] private Transform corpsesBase;

        private Vector3 _corpsePosition;

        public void OnEnable() => OnReleaseCorpses += ReleaseCorpsesInstance;
        public void OnDisable() => OnReleaseCorpses -= ReleaseCorpsesInstance;
        
        public static void ReleaseCorpses() => OnReleaseCorpses();

        public void AddCorpse()
        {
            _corpsePosition = corpsesBase.position;
            _corpsePosition.y += Corpse.DistanceBetweenCorpses + Corpse.DistanceBetweenCorpses * (corpses.Count);
            
            var newCorpse = Instantiate(corpsePrefab, _corpsePosition, corpsesBase.rotation, transform);

            if (corpses.Count > 0)
                newCorpse.Init(corpses[corpses.Count - 1]);
            else
                newCorpse.Init(corpsesBase);

            corpses.Add(newCorpse.transform);

            CorpseCount = corpses.Count;
        }

        private void ReleaseCorpsesInstance()
        {
            foreach (var corpse in corpses)
                Destroy(corpse.gameObject);
            corpses.Clear();
            CorpseCount = 0;
        }

    }
}
