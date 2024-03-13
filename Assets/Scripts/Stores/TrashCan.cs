using Powerlifter.PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Powerlifter.Stores
{
    public class TrashCan : MonoBehaviour
    {
        [SerializeField] private int corpsePrice = 5;

        private void OnTriggerEnter(Collider other)
        {
            if (CorpsesPile.CorpseCount == 0)
                return;

            Wallet.ReceiveMoney(CorpsesPile.CorpseCount * corpsePrice);
            CorpsesPile.ReleaseCorpses();
        }
    }
}
