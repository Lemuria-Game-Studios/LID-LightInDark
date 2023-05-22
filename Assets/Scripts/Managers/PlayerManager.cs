using System.Threading.Tasks;
using Cinemachine;
using Enums;
using UnityEngine;
using Signals;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [Header("Attributes")] [SerializeField]
        private ushort maxHealth;
        [SerializeField] private ushort attackPower;
        [SerializeField] private ushort health;
        [SerializeField] private float speed = 4;
        [SerializeField] private ushort attackSpeed;

        private AnimationStates _states;
        private float _dashMeter;
        [SerializeField] private bool canDash;
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
            InputSignals.Instance.OnGettingAnimationState += OnGettingAnimationStates;
            InputSignals.Instance.OnGetCanDash += OnGetCanDash;
            PlayerSignals.Instance.OnGettingAttackSpeed += OnGettingAttackSpeed;
            PlayerSignals.Instance.OnGettingSpeed += OnGettingSpeed;
            PlayerSignals.Instance.OnLevelUp += OnLevelUp;
            PlayerSignals.Instance.OnGettingHealth += OnGettingHealth;
            PlayerSignals.Instance.OnGettingAttackPower += OnGettingAttackPower;
            PlayerSignals.Instance.OnSettingAttributes += OnSettingAttributes;
        }

        private void Update()
        {
            DashMeter();
        }
        
        private void CanDash()
        {
            if (_dashMeter >= 30 && !canDash)
            {
                canDash = true;
                _dashMeter -= 30;
                PlayerSignals.Instance.OnDashing?.Invoke();
                CanDashAsync();
            }
        }

        private async Task CanDashAsync()
        {
            await Task.Delay(300);
            canDash = false;
        }
        private bool OnGetCanDash()
        {
            return canDash;
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
            return transform;
        }

        private float OnGettingSpeed()
        {
            return speed;
        }

        private ushort OnGettingAttackSpeed()
        {
            return attackSpeed;
        }
        private ushort OnGettingAttackPower()
        {
            return attackPower;
        }
        private ushort OnGettingHealth()
        {
            return health;
        }
        private ushort OnGettingMaxHealth()
        {
            return maxHealth;
        }

        private void ChangingHealth(HealthOperations states,ushort amount)
        {
            switch (states)
            {
                case HealthOperations.Attack:
                    health -= amount;
                    if (health <= 0)
                    {
                        Die();
                    }
                    break;
                case HealthOperations.Heal:
                    if (health >= maxHealth)
                    {
                        health += amount;
                    }
                    break;
            }
            
        }

        private void Die()
        {
            //Ölüm
        }

        private AnimationStates OnGettingAnimationStates()
        {
            return _states;
        }

        private void OnLevelUp(LevelUp state)
        {
            switch (state)
            {
              case LevelUp.AttackPower:
                  attackPower += 5;
                  break;
              case LevelUp.Health:
                  health += 50;
                  break;
              case LevelUp.Speed:
                  speed += 0.75f;
                  break;
              case LevelUp.AttackSpeed:
                  attackSpeed -= 100;
                  break;
            }
            CoreGameSignals.Instance.OnSavingGame.Invoke();
        }

        private void OnSettingAttributes(ushort aP, ushort hP, float sp, ushort aS)
        {
            attackPower = aP;
            maxHealth = hP;
            speed = sp;
            attackSpeed = aS;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        private void UnSubscribeEvents()
        {
            InputSignals.Instance.CanDash -= CanDash;
            PlayerSignals.Instance.OnGettingTransform -= OnGettingTransform;
            PlayerSignals.Instance.OnGettingAttackSpeed -= OnGettingAttackSpeed;
            PlayerSignals.Instance.OnGettingSpeed -= OnGettingSpeed;
        }
    }
}
