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
        


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            //PlayerSignals.Instance.OnSettingSpeed += OnSettingSpeed;
            PlayerSignals.Instance.OnDashing += OnDashing;
            InputSignals.Instance.OnGetIsDashing += OnGetIsDashing;
        }
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void FixedUpdate() {
            //Move();
            //Movement();
            MovementAndRotation();
        }

        private void MovementAndRotation()
        {
            float horizontalMovement = Input.GetAxisRaw("Horizontal"); 
        float verticalMovement = Input.GetAxisRaw("Vertical"); 
        
        switch (horizontalMovement)
        {
            case > 0 when verticalMovement > 0:
                _movementDirection = new Vector3(1, 0, 1).normalized;
                break;
            case > 0 when verticalMovement < 0:
                _movementDirection = new Vector3(1, 0, -1).normalized;
                break;
            case > 0:
                _movementDirection = new Vector3(1, 0, 0).normalized;
                break;
            case < 0 when verticalMovement > 0:
                _movementDirection = new Vector3(-1, 0, 1).normalized;
                break;
            case < 0 when verticalMovement < 0:
                _movementDirection = new Vector3(-1, 0, -1).normalized;
                break;
            case < 0:
                _movementDirection = new Vector3(-1, 0, 0).normalized;
                break;
            default:
            {
                if (verticalMovement > 0)
                {
                    _movementDirection = new Vector3(0, 0, 1).normalized;
                }
                else if (verticalMovement < 0)
                {
                    _movementDirection = new Vector3(0, 0, -1).normalized;
                }
                else
                {
                    _movementDirection = Vector3.zero;
                }

                break;
            }
        }

        if (_movementDirection != Vector3.zero)
        {
            AnimationSignals.Instance.OnPlayingAnimation?.Invoke(AnimationStates.Move);
            transform.rotation = Quaternion.LookRotation(_movementDirection);
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

        private async void OnDashing()
        {
            _isDashing = true;
            AnimationSignals.Instance.OnPlayingAnimation?.Invoke(AnimationStates.Dash);
            _rigidbody.AddForce(_movementDirection*dashSpeed,ForceMode.Impulse);
            await Task.Delay(300);
            _isDashing = false;
        }
        
        private bool OnGetIsDashing()
        {
            return _isDashing;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        private void UnSubscribeEvents()
        {
            //PlayerSignals.Instance.OnSettingSpeed -= OnSettingSpeed;
            PlayerSignals.Instance.OnDashing -= OnDashing;
            InputSignals.Instance.OnGetIsDashing -= OnGetIsDashing;
        }
    }

    
}
