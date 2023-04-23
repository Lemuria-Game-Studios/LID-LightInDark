using System;
using UnityEngine;
using Enums;
using Signals;
namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        //[SerializeField] private AnimationStates states;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDestroy()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            AnimationSignals.Instance.OnPlayingAnimation += OnPlayingAnimation;
        }
        
        private void UnSubscribeEvents()
        {
            AnimationSignals.Instance.OnPlayingAnimation -= OnPlayingAnimation;
        }

        private void OnPlayingAnimation(AnimationStates state)
        {
            switch (state)
            {
                case AnimationStates.Idle:
                    _animator.SetFloat("Blend",0f);
                    break;
                case AnimationStates.Move:
                    _animator.SetFloat("Blend",1f);
                    break;
                case AnimationStates.CloseAttack:
                    _animator.SetTrigger("Attack");
                    break;
                case AnimationStates.RangeAttack:
                    _animator.SetTrigger("Spell");
                    break;
                case AnimationStates.Dash:
                    break;
            }
        }

    }
}
