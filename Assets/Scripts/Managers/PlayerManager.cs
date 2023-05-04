using Enums;
using UnityEngine;
using Signals;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        private AnimationStates _states;
        private float _dashMeter;
        private void OnEnable()
        {
            SubscribeEvents();
            _dashMeter = 100;
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.CanDash += CanDash;
            PlayerSignals.Instance.OnGettingDashMeter += OnGettingDashMeter;
            PlayerSignals.Instance.OnSettingDashMeter += SetDashMeter;
            PlayerSignals.Instance.OnGettingTransform += OnGettingTransform;
        }

        private void Update()
        {
            DashMeter();
        }
        
        private void CanDash()
        {
            if (_dashMeter >= 30)
            {
                _dashMeter -= 30;
                PlayerSignals.Instance.OnDashing?.Invoke();
            }
        }

        private void DashMeter()
        {
            if (_dashMeter < 100)
            {
                _dashMeter += (Time.deltaTime)*10;
                CoreGameSignals.Instance.DashMeter?.Invoke(_dashMeter);
            }
        }

        private float OnGettingDashMeter()
        {
            return _dashMeter;
        }

        private void SetDashMeter(float amount)
        {
            _dashMeter=amount;
        }

        private Transform OnGettingTransform()
        {
            return this.transform;
        }

        public AnimationStates GetAnimationStates()
        {
            return _states;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        private void UnSubscribeEvents()
        {
            InputSignals.Instance.CanDash -= CanDash;
            PlayerSignals.Instance.OnGettingTransform -= OnGettingTransform;
        }
    }
}
