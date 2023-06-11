using UnityEngine;
using TMPro;
using Signals;
using Enums;

namespace UI_Events
{
    public class SkillTree : MonoBehaviour
    {
        [Header("Skill Levels")]
        [SerializeField] private byte attackLevel;
        [SerializeField] private byte healthLevel;
        [SerializeField] private byte attackSpeedLevel;
        [SerializeField] private byte speedLevel;
        [Header("Costs")]
        [SerializeField] private ushort attackLevelCost;
        [SerializeField] private ushort healthLevelCost;
        [SerializeField] private ushort attackSpeedLevelCost;
        [SerializeField] private ushort speedLevelCost;
        [Header("Level Texts")]
        [SerializeField] private TextMeshProUGUI attackLevelText;
        [SerializeField] private TextMeshProUGUI healthLevelText;
        [SerializeField] private TextMeshProUGUI attackSpeedLevelText;
        [SerializeField] private TextMeshProUGUI speedLevelText;
        [Header("Cost Texts")]
        [SerializeField] private TextMeshProUGUI attackLevelCostText;
        [SerializeField] private TextMeshProUGUI healthLevelCostText;
        [SerializeField] private TextMeshProUGUI attackSpeedLevelCostText;
        [SerializeField] private TextMeshProUGUI speedLevelCostText;

        private void OnEnable()
        {
            SubscribeEvents();
            CoreGameSignals.Instance.OnLoadingSkillTree.Invoke();
            WritingValues();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnGettingAttackLevel += OnGettingAttackLevel;
            CoreGameSignals.Instance.OnGettingHealthLevel += OnGettingHealthLevel;
            CoreGameSignals.Instance.OnGettingSpeedLevel += OnGettingSpeedLevel;
            CoreGameSignals.Instance.OnGettingAttackSpeedLevel += OnGettingAttackSpeedLevel;
            CoreGameSignals.Instance.OnSettingLevelValues += OnSettingLevelsValue;
            CoreGameSignals.Instance.OnGettingAttackLevelCost += OnGettingAttackLevelCost;
            CoreGameSignals.Instance.OnGettingHealthLevelCost += OnGettingHealthLevelCost;
            CoreGameSignals.Instance.OnGettingAttackSpeedLevelCost += OnGettingAttackSpeedLevelCost;
            CoreGameSignals.Instance.OnGettingSpeedLevelCost += OnGettingSpeedLevelCost;
            CoreGameSignals.Instance.OnSettingLevelCostValues += OnSettingLevelCostsValue;
        }

        private void WritingValues()
        {
            attackLevelText.text = "Attack: " + attackLevel;
            attackLevelCostText.text = "Cost: " + attackLevelCost;
            healthLevelText.text = "Health: " +healthLevel;
            healthLevelCostText.text = "Cost: " + healthLevelCost;
            speedLevelText.text = "Speed: " +speedLevel;
            speedLevelCostText.text = "Cost: " + speedLevelCost;
            attackSpeedLevelText.text = "Arrow: " +attackSpeedLevel;
            attackSpeedLevelCostText.text = "Cost: " + attackSpeedLevelCost;
        }

        public void AttackLevelButton()
        {
            if (CoreGameSignals.Instance.OnGettingMoney.Invoke() >= attackLevelCost)
            {
                if (attackLevel < 5)
                {
                    attackLevel++;
                    CoreGameSignals.Instance.OnSettingMoney.Invoke(MoneyOperations.Spend, attackLevelCost);
                    attackLevelCost += 50;
                    WritingValues();
                    PlayerSignals.Instance.OnLevelUp(LevelUp.AttackPower);
                    //CoreGameSignals.Instance.OnSavingSkillTree.Invoke();
                }
            }
        }
        public void HealthLevelButton()
        {
            if (CoreGameSignals.Instance.OnGettingMoney.Invoke() >= healthLevelCost)
            {
                if (healthLevel < 5)
                {
                    healthLevel++;
                    CoreGameSignals.Instance.OnSettingMoney.Invoke(MoneyOperations.Spend, healthLevelCost);
                    healthLevelCost += 50;
                    WritingValues();
                    PlayerSignals.Instance.OnLevelUp(LevelUp.Health);
                    //CoreGameSignals.Instance.OnSavingSkillTree.Invoke(); 
                }
            }
        }
        public void SpeedLevelButton()
        {
            if (CoreGameSignals.Instance.OnGettingMoney.Invoke() >= speedLevelCost)
            {
                Debug.Log(CoreGameSignals.Instance.OnGettingMoney.Invoke());
                if (speedLevel < 5)
                {
                    speedLevel++;
                    CoreGameSignals.Instance.OnSettingMoney.Invoke(MoneyOperations.Spend, speedLevelCost);
                    speedLevelCost += 50;
                    WritingValues();
                    PlayerSignals.Instance.OnLevelUp(LevelUp.Speed);
                    //CoreGameSignals.Instance.OnSavingSkillTree.Invoke();
                }
            }
        }
        public void AttackSpeedLevelButton()
        {
            if (CoreGameSignals.Instance.OnGettingMoney.Invoke() >= attackSpeedLevelCost)
            {
                Debug.Log(CoreGameSignals.Instance.OnGettingMoney.Invoke());
                if (attackSpeedLevel < 5)
                {
                    attackSpeedLevel++;
                    CoreGameSignals.Instance.OnSettingMoney.Invoke(MoneyOperations.Spend, attackSpeedLevelCost);
                    attackSpeedLevelCost += 50;
                    WritingValues();
                    PlayerSignals.Instance.OnLevelUp(LevelUp.AttackSpeed);
                    //CoreGameSignals.Instance.OnSavingSkillTree.Invoke();
                }
                
            }
        }
        private byte OnGettingAttackLevel()
        {
            return attackLevel;
        }
        private byte OnGettingHealthLevel()
        {
            return healthLevel;
        }
        private byte OnGettingSpeedLevel()
        {
            return speedLevel;
        }
        private byte OnGettingAttackSpeedLevel()
        {
            return attackSpeedLevel;
        }

        private ushort OnGettingHealthLevelCost()
        {
            return healthLevelCost;
        }
        private ushort OnGettingAttackLevelCost()
        {
            return attackLevelCost;
        }
        private ushort OnGettingAttackSpeedLevelCost()
        {
            return attackSpeedLevelCost;
        }
        private ushort OnGettingSpeedLevelCost()
        {
            return speedLevelCost;
        }

        private void OnSettingLevelsValue(byte aL, byte hL, byte sL, byte asL)
        {
            attackLevel = aL;
            healthLevel = hL;
            speedLevel = sL;
            attackSpeedLevel = asL;
        }

        private void OnSettingLevelCostsValue(ushort aL, ushort hL, ushort sL, ushort asL)
        {
            attackLevelCost = aL;
            healthLevelCost = hL;
            speedLevelCost = sL;
            attackSpeedLevelCost = asL;
        }
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnGettingAttackLevel -= OnGettingAttackLevel;
            CoreGameSignals.Instance.OnGettingHealthLevel -= OnGettingHealthLevel;
            CoreGameSignals.Instance.OnGettingSpeedLevel -= OnGettingSpeedLevel;
            CoreGameSignals.Instance.OnGettingAttackSpeedLevel -= OnGettingAttackSpeedLevel;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}
