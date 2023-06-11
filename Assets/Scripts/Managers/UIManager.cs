using Enums;
using UnityEngine;
using Signals;
using TMPro;
using UnityEngine.UI;


namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;
        [SerializeField] private Image dashMeter;
        [SerializeField] private TextMeshProUGUI moneyText;
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnUIManagement += OnUIManagement;
            CoreGameSignals.Instance.DashMeter += DashMeter;
            CoreGameSignals.Instance.OnMoneyUI += OnMoneyUI;
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnUIManagement -= OnUIManagement;
            CoreGameSignals.Instance.DashMeter -= DashMeter;
        }

        private void OnUIManagement(GameStates state)
        {
            if (canvas.transform.childCount >= 2)
            {
                Destroy(canvas.transform.GetChild(1).gameObject);
            }
            switch (state)
            {
                case GameStates.Game:
                    //Instantiate(Resources.Load<GameObject>("UI/InGameUI"), canvas.transform, false);
                    break;
                case GameStates.Pause:
                    Instantiate(Resources.Load<GameObject>("UI/Pause Screen"), canvas.transform, false);
                    break;
                case GameStates.SkillTree:
                    Instantiate(Resources.Load<GameObject>("UI/Skill Screen"), canvas.transform, false);
                    break;
                case GameStates.Dead:
                    Instantiate(Resources.Load<GameObject>("UI/Lose Screen"), canvas.transform, false);
                    break;
            }
        }
        private void DashMeter(float amount)
        {
            dashMeter.fillAmount = amount / 100;
        }

        private void OnMoneyUI()
        {
            moneyText.text = "Gold: " + CoreGameSignals.Instance.OnGettingMoney.Invoke().ToString();
        }
    }
}
