using System;
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
            PlayerSignals.Instance.OnAttacking += OnAttacking;
            PlayerSignals.Instance.OnSpelling += OnSpelling;
            PlayerSignals.Instance.CanDash += CanDash;
        }

        private void Update()
        {
            DashMeter();
        }

        private void OnAttacking()
        {
            AnimationSignals.Instance.OnPlayingAnimation?.Invoke(AnimationStates.CloseAttack);
            //PlayerSignals.Instance.onSettingSpeed?.Invoke(1);
        }

        private void OnSpelling()
        {
            AnimationSignals.Instance.OnPlayingAnimation?.Invoke(AnimationStates.RangeAttack);
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

        public float GetDashMeter()
        {
            return _dashMeter;
        }
        public float SetDashMeter(float amount)
        {
            return _dashMeter=amount;
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
            PlayerSignals.Instance.OnAttacking -= OnAttacking;
            PlayerSignals.Instance.OnSpelling -= OnSpelling;
            PlayerSignals.Instance.CanDash -= CanDash;
        }
    }
}
