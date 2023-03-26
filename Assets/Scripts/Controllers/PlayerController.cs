using Managers;
using UnityEngine;
using Signals;

namespace Controllers
{
    public class PlayerController : MonoBehaviour {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float speed = 5;
        [SerializeField] private float turnSpeed = 360;
        [SerializeField] private float dashSpeed = 10;
        [SerializeField] private InputManager _InputManager;
        private Rigidbody _rigidbody;
        private bool _isDashing;
        private bool _isSpelling;
        private Animator _animator;


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onSettingSpeed += SetSpeed;
            PlayerSignals.Instance.onDashing += OnDashing;
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update() {
            Look();
        }
        private void FixedUpdate() {
            Move();
        }

        private void Look() {
            if (_InputManager.input == Vector3.zero) return;

            var rot = Quaternion.LookRotation(_InputManager.input.ToIso(), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }

        private void Move()
        {
            var transform1 = transform;
                rb.MovePosition(transform1.position + transform1.forward * (_InputManager.input.normalized.magnitude * speed * Time.deltaTime));
                
                if (_InputManager.input != Vector3.zero)
            {
                AnimationSignals.Instance.onMovementAnimation?.Invoke(_animator);
            }
            else if(_InputManager.input==Vector3.zero)
            {
                AnimationSignals.Instance.onIdleAnimation?.Invoke(_animator);
            }
        }

        private void SetSpeed(float num)
        {
            speed = num;
        }

        private void OnDashing()
        {
            _rigidbody.AddForce(transform.forward*dashSpeed,ForceMode.Impulse);
            Debug.Log("Dash");
        }
    }

    

    public static class Helpers 
    {
        private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
    }
}