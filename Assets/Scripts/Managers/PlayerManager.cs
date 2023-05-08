using System.Threading.Tasks;
using Enums;
using UnityEngine;
using Signals;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        private AnimationStates _states;
        private float _dashMeter;
        [SerializeField] private bool _canDash=false;
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
            InputSignals.Instance.OnGettingAnimationState += OnGetttingAnimationStates;
            InputSignals.Instance.OnGetCanDash += OnGetCanDash;
        }

        private void Update()
        {
            DashMeter();
        }
        
        private void CanDash()
        {
            if (_dashMeter >= 30 && !_canDash)
            {
                _canDash = true;
                _dashMeter -= 30;
                PlayerSignals.Instance.OnDashing?.Invoke();
                CanDashAsync();
            }
        }

        private async Task CanDashAsync()
        {
            await Task.Delay(300);
            _canDash = false;
        }
        private bool OnGetCanDash()
        {
            return _canDash;
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

        public AnimationStates OnGetttingAnimationStates()
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
