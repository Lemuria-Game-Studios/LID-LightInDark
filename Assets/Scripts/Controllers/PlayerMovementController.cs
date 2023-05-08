using System;
using UnityEngine;
using Signals;
using Enums;
using System.Threading.Tasks;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour {
        [SerializeField] private float speed = 5;
        [SerializeField] private float dashSpeed = 10;
        private Rigidbody _rigidbody;
        [SerializeField] private Camera mainCamera;
        private Vector3 _mousePosition;
        private bool _isDashing;
        private bool _isSpelling;
        private float _turnSmoothVelocity;
        private Vector3 _moveDirection;
        private Vector3 _movementDirection = Vector3.zero;
        private const int _rotationAngle = 45;
        private Vector3 _lastDirection;
        private float _horizontalMovement;
        private float _verticalMovement;


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            //PlayerSignals.Instance.OnSettingSpeed += OnSettingSpeed;
            PlayerSignals.Instance.OnDashing += OnDashing;
            //InputSignals.Instance.OnMovementAndRotation += OnMovementAndRotation;
        }
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            OnMovementAndRotation();
        }

        private void OnMovementAndRotation()
        {
            if (!InputSignals.Instance.OnGetCanDash.Invoke())
            {
                _horizontalMovement = Input.GetAxisRaw("Horizontal"); 
                _verticalMovement = Input.GetAxisRaw("Vertical"); 
            }
            
            _movementDirection = new Vector3(_horizontalMovement, 0f, _verticalMovement).normalized;
            Quaternion rotation = Quaternion.Euler(0f, _rotationAngle, 0f);
        _movementDirection = rotation * _movementDirection;
        

        if (_movementDirection != Vector3.zero)
        {
            AnimationSignals.Instance.OnPlayingAnimation?.Invoke(AnimationStates.Move);
            transform.rotation = Quaternion.LookRotation(_movementDirection);
            _lastDirection = _movementDirection;
        }
        else if (_movementDirection == Vector3.zero)
        {
            AnimationSignals.Instance.OnPlayingAnimation?.Invoke(AnimationStates.Idle);
        }
        
        Vector3 velocity = _movementDirection * speed;
        
        transform.position += velocity * Time.deltaTime;
        
        

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 lookAtPos = ray.GetPoint(rayDistance);
            transform.LookAt(new Vector3(lookAtPos.x, transform.position.y, lookAtPos.z));
        }
        }

        /*private void Move()
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
        }*/

        private void OnSettingSpeed(float num)
        {
            speed = num;
        }

        private void OnDashing()
        {
            AnimationSignals.Instance.OnPlayingAnimation?.Invoke(AnimationStates.Dash);
            _rigidbody.AddForce(_movementDirection*dashSpeed,ForceMode.Impulse);
        }
        
       
        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        private void UnSubscribeEvents()
        {
            //PlayerSignals.Instance.OnSettingSpeed -= OnSettingSpeed;
            PlayerSignals.Instance.OnDashing -= OnDashing;
        }
    }

    
}
