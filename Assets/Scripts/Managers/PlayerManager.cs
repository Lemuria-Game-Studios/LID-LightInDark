using System;
using Enums;
using UnityEngine;
using Signals;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.OnAttacking += OnAttacking;
            PlayerSignals.Instance.OnSpelling += OnSpelling;
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

        private void OnDestroy()
        {
            UnSubscribeEvents();
        }
        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.OnAttacking -= OnAttacking;
            PlayerSignals.Instance.OnSpelling -= OnSpelling;
        }
    }
}
