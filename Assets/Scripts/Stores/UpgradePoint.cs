using Powerlifter.PlayerScripts;
using UnityEngine;

namespace Powerlifter.Stores
{
    public class UpgradePoint : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(Wallet.TryPurchase(PlayerStatus.UpdatePrice))
                PlayerStatus.UpdatePlayerStatus();
        }
    }
}
