using System.Collections.Generic;
using UnityEngine;

namespace Powerlifter.PlayerScripts.Corpses
{
    public class Corpse : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float distance = 1f;
        [SerializeField] private int bufferSize;

        //Position cache
        private Queue<CorpseBufferData> _positionsBuffer = new();
        private CorpseBufferData _bufferData;
        private Vector3 _newPosition;
        private Vector3 _positionDampVelocity;
        private Vector3 _targetDirection;
        private float _xDistance;
        private float _zDistance;

        //Rotation cache
        private Vector3 _desiredRotation;
        private Vector3 _rotationDampVelocity;

        private void Start()
        {
            for(int i = 0; i < bufferSize; i++)
                _positionsBuffer.Enqueue(new CorpseBufferData(transform.position.x, transform.position.z));
        }

        private void FixedUpdate()
        {
            SetPosition();
            SetRotation();
        }

        private void SetPosition()
        {
            SetXZPosition();
            SetYPosition();
            transform.position = Vector3.SmoothDamp(transform.position, _newPosition, ref _positionDampVelocity, .05f);
        }

        private void SetXZPosition()
        {
            _positionsBuffer.Enqueue(new CorpseBufferData(target.position.x, target.position.z));
            _bufferData = _positionsBuffer.Dequeue();

            _newPosition.x = _bufferData.x;
            _newPosition.z = _bufferData.z;
        }

        private void SetYPosition()
        {
            _xDistance = transform.position.x - target.position.x;
            _zDistance = transform.position.z - target.position.z;

            _newPosition.y =
                Mathf.Sqrt(
                    Mathf.Pow(target.position.y + distance, 2) -
                    Mathf.Pow(_xDistance, 2) -
                    Mathf.Pow(_zDistance, 2));

            if (float.IsNaN(_newPosition.y))
                _newPosition.y = target.position.y;
        }

        private void SetRotation()
        {
            _targetDirection = transform.position - target.position;
            _desiredRotation = Vector3.RotateTowards(transform.forward, _targetDirection, 90f, 90f);
            transform.forward = Vector3.SmoothDamp(transform.forward, _desiredRotation, ref _rotationDampVelocity, .05f);
        }

        public struct CorpseBufferData
        {
            public float x;
            public float z;

            public CorpseBufferData(float x, float z)
            {
                this.x = x;
                this.z = z;
            }
        }
    }
}
