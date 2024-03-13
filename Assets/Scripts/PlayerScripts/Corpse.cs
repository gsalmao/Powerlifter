using System.Collections.Generic;
using UnityEngine;

namespace Powerlifter.PlayerScripts
{
    public class Corpse : MonoBehaviour
    {
        public static float DistanceBetweenCorpses { get; private set; } = .5f;

        [SerializeField] private int bufferSize;

        private Transform _target;

        //Position cache
        private Queue<CorpseBufferData> _buffer = new();
        private CorpseBufferData _currentBufferData;
        private Vector3 _newPosition;
        private Vector3 _positionDampVelocity;
        private float _xDistance;
        private float _zDistance;

        //Rotation cache
        private Vector3 _targetToThis;
        private Vector3 _desiredRotation;
        private Vector3 _rotationDampVelocity;
        
        public void Init(Transform target)
        {
            _target = target;

            for (int i = 0; i < bufferSize; i++)
            {
                _buffer.Enqueue(
                    new CorpseBufferData(
                        transform.rotation,
                        transform.position.x,
                        transform.position.z));
            }
        }

        private void FixedUpdate()
        {
            SetPosition();
            SetRotation();
        }

        private void SetPosition()
        {
            _buffer.Enqueue(new CorpseBufferData(_target.rotation, _target.position.x, _target.position.z));
            _currentBufferData = _buffer.Dequeue();

            SetXZPosition();
            SetYPosition();

            transform.position =
                Vector3.SmoothDamp(transform.position, _newPosition, ref _positionDampVelocity, .05f);
        }

        private void SetXZPosition()
        {
            _newPosition.x = _currentBufferData.X;
            _newPosition.z = _currentBufferData.Z;
        }

        private void SetYPosition()
        {
            _xDistance = transform.position.x - _target.position.x;
            _zDistance = transform.position.z - _target.position.z;

            _newPosition.y =
                Mathf.Sqrt(
                    Mathf.Pow(_target.position.y + DistanceBetweenCorpses, 2) -
                    Mathf.Pow(_xDistance, 2) -
                    Mathf.Pow(_zDistance, 2));

            if (float.IsNaN(_newPosition.y))
                _newPosition.y = _target.position.y;
        }

        private void SetRotation()
        {
            transform.LookAt(_target);

            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                _currentBufferData.Rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z);
        }

        public struct CorpseBufferData
        {
            public float X;
            public float Z;
            public Quaternion Rotation;

            public CorpseBufferData(Quaternion rotation, float x, float z)
            {
                Rotation = rotation;
                X = x;
                Z = z;
            }
        }
    }
}
