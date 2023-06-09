using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Task = System.Threading.Tasks.Task;
using Signals;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnLoadingScene += OnLoadingScene;
        }

        private async void OnLoadingScene(string sceneName)
        {
            var scene = SceneManager.LoadSceneAsync(sceneName);
            scene.allowSceneActivation = false;
            do
            {
                await Task.Delay(100);
            } while (scene.progress<0.9f);

            scene.allowSceneActivation = true;
        }

        
    }
}
