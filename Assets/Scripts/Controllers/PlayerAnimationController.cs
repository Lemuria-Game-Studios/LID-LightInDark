using System;
using UnityEngine;
using Enums;
using Signals;
using UnityEditor;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        //[SerializeField] private AnimationStates states;
        private Animator _animator;
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Blend = Animator.StringToHash("Blend");
        private static readonly int Spell = Animator.StringToHash("Spell");
        private static readonly int Attack2 = Animator.StringToHash("Attack2");
        private static readonly int Die = Animator.StringToHash("Die");

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
            InputSignals.Instance.OnGettingAnimator += OnGettingAnimator;
        }
        
        private void UnSubscribeEvents()
        {
            AnimationSignals.Instance.OnPlayingAnimation -= OnPlayingAnimation;
            InputSignals.Instance.OnGettingAnimator -= OnGettingAnimator;
        }

        private void OnPlayingAnimation(AnimationStates state)
        {
            switch (state)
            {
                case AnimationStates.Idle:
                    
                    _animator.SetFloat(Blend,0f);
                    break;
                case AnimationStates.Move:
                    _animator.SetFloat(Blend,1f);
                    break;
                case AnimationStates.CloseAttack:
                    _animator.SetTrigger(Attack);
                    break;
                case AnimationStates.RangeAttack:
                    _animator.SetTrigger(Spell);
                    break;
                case AnimationStates.Dash:
                    break;
                case AnimationStates.Attack2:
                    _animator.SetBool(Attack,false);
                    _animator.SetTrigger(Attack2);
                    break;
                case AnimationStates.Die:
                    _animator.SetTrigger(Die);
                    break;
            }
        }

        private Animator OnGettingAnimator()
        {
            return _animator;
        }

    }
}
