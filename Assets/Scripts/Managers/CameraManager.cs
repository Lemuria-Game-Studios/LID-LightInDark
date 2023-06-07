using System;
using UnityEngine;
using Signals;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private float smoothTime = 0.3f; 
        private Vector3 _offset; 

        private Vector3 _velocity = Vector3.zero;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CameraSignals.Instance.OnGettingCameraTransform += OnGettingCameraTransform;
        }
        private Transform OnGettingCameraTransform()
        {
            return transform;
        }
        private void OnDisable()
        {
            CameraSignals.Instance.OnGettingCameraTransform -= OnGettingCameraTransform;
        }

        private void LateUpdate()
        {
            FollowPlayer();
        }

        private void FollowPlayer()
        {
            Vector3 targetPosition = PlayerSignals.Instance.OnGettingTransform().position + _offset;
            
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        }

        

    }
}
