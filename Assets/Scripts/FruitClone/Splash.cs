using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using ZHMQ.ObjectPooling;

namespace FruitClone
{
    public class Splash : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sprite;

        private const float spawnDuration = .3f;
        private const float fadeDuration = 3f;

        private async void OnEnable()
        {
            DOTween.Kill(sprite);
            
            sprite.color = Color.clear;

            transform.DOScale(4f, spawnDuration).SetId(sprite);
            await sprite.DOColor(Color.white, spawnDuration);
            await sprite.DOColor(Color.clear, fadeDuration).SetEase(Ease.Linear);

            ObjectPool.ReturnToPool(gameObject);
        }
    }
}
