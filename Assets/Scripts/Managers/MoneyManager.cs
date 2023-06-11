using UnityEngine;
using Enums;
using Signals;

namespace Managers
{
    public class MoneyManager : MonoBehaviour
    {
        [SerializeField]private ushort money= 0;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void Start()
        {
            CoreGameSignals.Instance.OnMoneyUI.Invoke();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnGettingMoney += OnGettingMoney;
            CoreGameSignals.Instance.OnSettingMoney += OnSettingMoney;
        }

        private ushort OnGettingMoney()
        {
            return money;
        }

        private void OnSettingMoney(MoneyOperations op, ushort value )
        {
            switch (op)
            {
                case MoneyOperations.Earn:
                    money += value;
                    break;
                case MoneyOperations.Spend:
                    money -= value;
                    break;
                case MoneyOperations.Save:
                    money = value;
                    break;
            }
            CoreGameSignals.Instance.OnMoneyUI.Invoke();
        }
    }
}
