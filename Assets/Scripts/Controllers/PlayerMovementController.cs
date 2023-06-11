using UnityEngine;
using Signals;
using Enums;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour {
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
        //private Vector3 _lastDirection;
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

        private void Update()
        {
            OnMovementAndRotation();
        }

        private void OnMovementAndRotation()
        {
            if (CoreGameSignals.Instance.OnGettingGameState() == GameStates.Game)
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
                    //_lastDirection = _movementDirection;
                }
                else if (_movementDirection == Vector3.zero)
                {
                    AnimationSignals.Instance.OnPlayingAnimation?.Invoke(AnimationStates.Idle);
                }
        
                Vector3 velocity = _movementDirection * PlayerSignals.Instance.OnGettingSpeed.Invoke();
        
                transform.position += velocity * Time.deltaTime;
        
        

                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
                float rayDistance;

                if (groundPlane.Raycast(ray, out rayDistance) && CoreGameSignals.Instance.OnGettingGameState()==GameStates.Game)
                {
                    Vector3 lookAtPos = ray.GetPoint(rayDistance);
                    transform.LookAt(new Vector3(lookAtPos.x, transform.position.y, lookAtPos.z));
                }
            }
            else
            {
                _movementDirection = Vector3.zero;
            }
            
        }

        private void OnDashing()
        {
            AnimationSignals.Instance.OnPlayingAnimation?.Invoke(AnimationStates.Dash);
            _rigidbody.AddForce(_movementDirection.normalized*dashSpeed,ForceMode.Impulse);
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
