using System;
using TMPro;
using UnityEngine;

namespace Powerlifter.PlayerScripts
{
    public class Wallet : MonoBehaviour
    {
        private static event Action OnUpdateView = delegate { };

        [Header("View")]
        [SerializeField] private AudioSource cashSound;
        [SerializeField] private TextMeshProUGUI moneyView;

        private static int money = 0;

        private void OnEnable()
        {
            moneyView.text = $"$ {money}";
            OnUpdateView += UpdateView;
        }

        private void OnDisable()
        {
            OnUpdateView -= UpdateView;
        }

        public static void ReceiveMoney(int amount)
        {
            money += amount;
            OnUpdateView();
        }

        public static bool TryPurchase(int amount)
        {
            if (HasMoney(amount))
            {
                money -= amount;
                OnUpdateView();
                return true;
            }

            return false;
        }

        public static bool HasMoney(int amount) => money >= amount;

        private void UpdateView()
        {
            moneyView.text = $"$ {money}";
            cashSound.Play();
        }
    }
}
