using Powerlifter.Npcs;
using Powerlifter.Utilities;
using UnityEngine;

namespace Powerlifter.PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private PlayerSettings settings;

        [Header("Triggers")]
        [SerializeField] private TriggerEvent detectNpc;
        [SerializeField] private TriggerEvent detectDeadBody;

        [Header("References")]
        [SerializeField] private PlayerInputs playerInputs;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Animator model;
        [SerializeField] private CorpsesPile corpsesPile;

        [Header("Sounds")]
        [SerializeField] private AudioSource slapSound;
        [SerializeField] private AudioSource pickupSound;

        //cache

        private Quaternion _targetRotation;
        private Vector3 _cameraForward;
        private Vector3 _cameraRight;
        private Vector3 _moveDirection;
        private Vector3 _throwForce;
        private float _punchTimer;

        private const string Punch = "Punch";
        private const string Walking = "Walking";

        private void OnEnable()
        {
            detectNpc.OnEnterTrigger += PunchNpc;
            detectDeadBody.OnEnterTrigger += PickUpDeadBody;
        }

        private void OnDisable()
        {
            detectNpc.OnEnterTrigger -= PunchNpc;
            detectDeadBody.OnEnterTrigger -= PickUpDeadBody;
        }

        private void FixedUpdate()
        {
            if (playerInputs.IsMoving && _punchTimer <= 0f)
                MovePlayer();
            else
            {
                rb.velocity = default;
                _punchTimer -= Time.fixedDeltaTime;
            }
        }

        private void Update() => model.SetBool(Walking, playerInputs.IsMoving);

        private void PunchNpc(Collider other)
        {
            slapSound.Play();

            model.PlayInFixedTime(Punch, 0, 0f);
            _punchTimer = settings.PunchDuration;

            _throwForce = model.transform.forward * settings.PunchForce;
            other.attachedRigidbody.GetComponent<Npc>().ReceivePunch(_throwForce);
        }

        private void PickUpDeadBody(Collider deadBody)
        {
            if(CorpsesPile.CorpseCount < PlayerStatus.CorpsesPossible)
            {
                pickupSound.Play();
                deadBody.GetComponent<EventHandler>().Execute();
                corpsesPile.AddCorpse();
            }
        }

        private void MovePlayer()
        {
            _cameraForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
            _cameraRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;
            _moveDirection = _cameraForward * playerInputs.Movement.y + _cameraRight * playerInputs.Movement.x;

            _targetRotation = Quaternion.LookRotation(_moveDirection.normalized, Vector3.up);

            model.transform.rotation = Quaternion.RotateTowards(
                model.transform.rotation,
                _targetRotation,
                settings.MaxRotationDegrees);

            rb.velocity = _moveDirection.normalized * settings.Speed;
        }
    }
}
