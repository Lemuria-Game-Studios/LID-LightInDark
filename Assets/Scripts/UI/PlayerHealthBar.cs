using System.Collections;
using Signals;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace UI
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField] private float reduceSpeed = 2;
        [SerializeField] private Image playerHealthBar;
        [SerializeField] private Image playerHealthBar2;
        private float _target=1;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.OnUpdatingHealthBar += OnUpdatingHealthBar;
        }

        private void OnUpdatingHealthBar(float maxHealth, float currentHealth)
        {
            _target = currentHealth / maxHealth;
            playerHealthBar2.fillAmount = _target;
            StartCoroutine(UpdateHealthBarWithDelay());
        }
        
        private IEnumerator UpdateHealthBarWithDelay()
        {
            yield return new WaitForSeconds(0.3f); 

            float fillAmountDelta = reduceSpeed * Time.deltaTime; 

            while (playerHealthBar.fillAmount != _target)
            {
                playerHealthBar.fillAmount = Mathf.MoveTowards(playerHealthBar.fillAmount, _target, fillAmountDelta);
                
                yield return null;
            }
        }
    }
}
