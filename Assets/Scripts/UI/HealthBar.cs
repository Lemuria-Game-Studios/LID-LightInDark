using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image healthBarSprite;
        [FormerlySerializedAs("_reduceSpeed")] [SerializeField] private float reduceSpeed = 2;
        private float _target=1;
        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
        }

        public async void UptadeHealthBar(float maxHealth, float currentHealth)
        {
            _target = currentHealth / maxHealth;
            if (_target <= 0)
            {
                await CanvasDisAble();
            }
        }

        private async Task CanvasDisAble()
        {
            await Task.Delay(2000);
            GetComponent<Canvas>().enabled = false;
        }

        private void Update()
        {
            healthBarSprite.fillAmount =
                Mathf.MoveTowards(healthBarSprite.fillAmount, _target, reduceSpeed * Time.deltaTime);
            transform.rotation = _cam.transform.rotation;
        }
    }
}
