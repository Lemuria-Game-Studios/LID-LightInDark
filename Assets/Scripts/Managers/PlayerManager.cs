using System;
using System.Threading.Tasks;
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
        [SerializeField] private short health;
        [SerializeField] private float speed = 4;
        [SerializeField] private ushort attackSpeed;
        [SerializeField] private ushort arrowPower;

        private AnimationStates _states;
        [SerializeField] private bool canDash;
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.CanDash += CanDash;
            /*PlayerSignals.Instance.OnGettingDashMeter += OnGettingDashMeter;
            PlayerSignals.Instance.OnSettingDashMeter += SetDashMeter;*/
            PlayerSignals.Instance.OnGettingTransform += OnGettingTransform;
            InputSignals.Instance.OnGettingAnimationState += OnGettingAnimationStates;
            InputSignals.Instance.OnGetCanDash += OnGetCanDash;
            PlayerSignals.Instance.OnGettingAttackSpeed += OnGettingAttackSpeed;
            PlayerSignals.Instance.OnGettingSpeed += OnGettingSpeed;
            PlayerSignals.Instance.OnLevelUp += OnLevelUp;
            PlayerSignals.Instance.OnGettingHealth += OnGettingHealth;
            PlayerSignals.Instance.OnGettingAttackPower += OnGettingAttackPower;
            PlayerSignals.Instance.OnSettingAttributes += OnSettingAttributes;
            PlayerSignals.Instance.OnGettingArrowPower += OnGettingArrowPower;
        }

        private void CanDash()
        {
            if (!canDash)
            {
                canDash = true;
                PlayerSignals.Instance.OnDashing?.Invoke();
                CanDashAsync();
            }
        }

        private async Task CanDashAsync()
        {
            await Task.Delay(400);
            canDash = false;
        }
        private bool OnGetCanDash()
        {
            return canDash;
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
        private short OnGettingHealth()
        {
            return health;
        }
        private ushort OnGettingMaxHealth()
        {
            return maxHealth;
        }

        private ushort OnGettingArrowPower()
        {
            return arrowPower;
        }

        public void ChangingHealth(HealthOperations states,short amount)
        {
            switch (states)
            {
                case HealthOperations.Attack:
                    Debug.Log("Hit by Enemy");
                    health -= amount;
                    if (health <= 0)
                    {
                        Die();
                    }
                    PlayerSignals.Instance.OnUpdatingHealthBar.Invoke(Convert.ToSingle(maxHealth),Convert.ToSingle(health));
                    break;
                case HealthOperations.Heal:
                    health += amount;
                    if (health >= maxHealth)
                    {
                        health = (short)maxHealth;
                    }
                    PlayerSignals.Instance.OnUpdatingHealthBar.Invoke(Convert.ToSingle(maxHealth),Convert.ToSingle(health));
                    break;
            }
            
        }

        private async void Die()
        {
            AnimationSignals.Instance.OnPlayingAnimation?.Invoke(AnimationStates.Die);
            CoreGameSignals.Instance.OnChangeGameState?.Invoke(GameStates.Dead);
            await DyingStop();
        }

        private async Task DyingStop()
        {
            await Task.Delay(2000);
            CoreGameSignals.Instance.OnUIManagement?.Invoke(GameStates.Dead);
            CoreGameSignals.Instance.OnPausingGame?.Invoke();
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
                  maxHealth += 50;
                  ChangingHealth(HealthOperations.Heal,50);
                  PlayerSignals.Instance.OnUpdatingHealthBar.Invoke(Convert.ToSingle(maxHealth),Convert.ToSingle(health));
                  break;
              case LevelUp.Speed:
                  speed += 0.3f;
                  break;
              case LevelUp.AttackSpeed:
                  arrowPower += 5;
                  break;
            }
            //CoreGameSignals.Instance.OnSavingGame.Invoke();
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
