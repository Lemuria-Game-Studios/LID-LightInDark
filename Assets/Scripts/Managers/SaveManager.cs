using Signals;
using UnityEngine;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnSavingGame += OnSavingGame;
            CoreGameSignals.Instance.OnLoadingGame += OnLoadingGame;
            CoreGameSignals.Instance.OnSavingSkillTree += OnSavingSkillTree;
            CoreGameSignals.Instance.OnLoadingSkillTree += OnLoadingSkillTree;
            CoreGameSignals.Instance.OnResettingSkillTree += OnResettingSkillTree;
        }

        private void OnSavingGame()
        {
            ES3.Save("transform",PlayerSignals.Instance.OnGettingTransform.Invoke());
            ES3.Save("dashMeter",PlayerSignals.Instance.OnGettingDashMeter.Invoke());
            ES3.Save("CameraTransform",CameraSignals.Instance.OnGettingCameraTransform.Invoke());
            ES3.Save("AttackPower",PlayerSignals.Instance.OnGettingAttackPower.Invoke());
            ES3.Save("Health",PlayerSignals.Instance.OnGettingHealth.Invoke());
            ES3.Save("Speed",PlayerSignals.Instance.OnGettingSpeed.Invoke());
            ES3.Save("AttackSpeed",PlayerSignals.Instance.OnGettingAttackSpeed.Invoke());
        }
        
        private void OnLoadingGame()
        {
            ES3.Load<Transform>("transform", PlayerSignals.Instance.OnGettingTransform.Invoke());
            PlayerSignals.Instance.OnSettingDashMeter(ES3.Load<float>("dashMeter"));
            ES3.Load<Transform>("CameraTransform", CameraSignals.Instance.OnGettingCameraTransform.Invoke());
            PlayerSignals.Instance.OnSettingAttributes.Invoke(ES3.Load<ushort>("AttackPower"),ES3.Load<ushort>("Health"),
                ES3.Load<float>("Speed"),ES3.Load<ushort>("AttackSpeed"));
        }

        private void OnSavingSkillTree()
        {
            ES3.Save("AttackLevel",CoreGameSignals.Instance.OnGettingAttackLevel.Invoke());
                ES3.Save("HealthLevel",CoreGameSignals.Instance.OnGettingHealthLevel.Invoke());
                ES3.Save("SpeedLevel",CoreGameSignals.Instance.OnGettingSpeedLevel.Invoke());
                ES3.Save("AttackSpeedLevel",CoreGameSignals.Instance.OnGettingAttackSpeedLevel.Invoke());
                Debug.Log(CoreGameSignals.Instance.OnGettingAttackLevel.Invoke());
                Debug.Log(CoreGameSignals.Instance.OnGettingHealthLevel.Invoke());
                Debug.Log(CoreGameSignals.Instance.OnGettingSpeedLevel.Invoke());
                Debug.Log(CoreGameSignals.Instance.OnGettingAttackSpeedLevel.Invoke());
        }

        private void OnLoadingSkillTree()
        {
            if (ES3.KeyExists("AttackLevel"))
            {
                CoreGameSignals.Instance.OnSettingLevelValues(ES3.Load<byte>("AttackLevel"),
                    ES3.Load<byte>("HealthLevel"),
                    ES3.Load<byte>("SpeedLevel"), ES3.Load<byte>("AttackSpeedLevel"));
            }
        }

        private void OnResettingSkillTree()
        {
            ES3.DeleteKey("AttackLevel");
            ES3.DeleteKey("HealthLevel");
            ES3.DeleteKey("SpeedLevel");
            ES3.DeleteKey("AttackSpeedLevel");
            ES3.DeleteKey("AttackPower");
            ES3.DeleteKey("Health");
            ES3.DeleteKey("Speed");
            ES3.DeleteKey("AttackSpeed");
            PlayerSignals.Instance.OnSettingAttributes.Invoke(20,200,
                4f,1000);
            Debug.Log("Reset");
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnSavingGame -= OnSavingGame;
            CoreGameSignals.Instance.OnLoadingGame -= OnLoadingGame;
        }
    }
}
