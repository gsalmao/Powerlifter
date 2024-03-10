using UnityEngine;
using ZHMQ.ObjectPooling;

namespace FruitClone
{
    public class BladeTrail : MonoBehaviour
    {
        [SerializeField] private float lifetime;

        private Transform _target;
        private float _timer;
        private float _returnToPoolTime = 2f;
        private bool _released;

        private void OnEnable() => Blade.OnReleaseButton += ReleaseTrail;
        
        private void OnDisable()
        {
            _timer = 0f;
            _released = false;
            Blade.OnReleaseButton -= ReleaseTrail;
        }

        public void Init(Transform target) => this._target = target;
        
        private void Update()
        {
            if(_released)
            {
                _timer += Time.deltaTime;

                if (_timer > _returnToPoolTime)
                    ObjectPool.ReturnToPool(this);
            }
            else
            {
                transform.position = _target.position;
            }
        }

        private void ReleaseTrail() => _released = true;
    }
}
