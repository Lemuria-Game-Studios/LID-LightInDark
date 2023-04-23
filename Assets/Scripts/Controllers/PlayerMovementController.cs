using Managers;
using UnityEngine;
using Signals;
using Enums;
using Extensions;
using System.Threading.Tasks;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float speed = 5;
        [SerializeField] private float turnSpeed = 360;
        [SerializeField] private float dashSpeed = 10;
        [SerializeField] private InputManager ınputManager;
        private Rigidbody _rigidbody;
        private bool _isDashing;
        private bool _isSpelling;
        private float _dashMeter;
        


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.OnSettingSpeed += OnSettingSpeed;
            PlayerSignals.Instance.OnDashing += OnDashing;
        }
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _dashMeter = 100;
        }
       
        private void Update() {
            Look();
        }
        private void FixedUpdate() {
            Move();
        }
      
        private void Look() {
            if (ınputManager.input == Vector3.zero) return;

            var rot = Quaternion.LookRotation(ınputManager.input.ToIso(), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }
        
        private void Move()
        {
            var transform1 = transform;
                rb.MovePosition(transform1.position + transform1.forward * (ınputManager.input.normalized.magnitude * speed * Time.deltaTime));
                
                if (ınputManager.input != Vector3.zero)
            {
                AnimationSignals.Instance.OnPlayingAnimation?.Invoke(AnimationStates.Move);
            }
            else if(ınputManager.input==Vector3.zero)
            {
                AnimationSignals.Instance.OnPlayingAnimation?.Invoke(AnimationStates.Idle);
            }
        }

        private void OnSettingSpeed(float num)
        {
            speed = num;
        }

        private async void OnDashing()
        {
            _isDashing = true;
            AnimationSignals.Instance.OnPlayingAnimation?.Invoke(AnimationStates.Dash);
            _rigidbody.AddForce(transform.forward*dashSpeed,ForceMode.Impulse);
            await Task.Delay(300);
            _isDashing = false;
        }

        
        public bool GetIsDashing()
        {
            return _isDashing;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.OnSettingSpeed -= OnSettingSpeed;
            PlayerSignals.Instance.OnDashing -= OnDashing;
        }
    }
    
    /*public static class Helpers 
    {
        private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
    }*/
}