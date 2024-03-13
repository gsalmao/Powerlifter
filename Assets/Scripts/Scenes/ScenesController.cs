using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Powerlifter.Scenes
{
    public class ScenesController : MonoBehaviour
    {

        private static event Action<string> OnChangeScene = delegate { };

        private static bool _changing;

        [SerializeField] private Image fade;
        [SerializeField] private string firstScene;
        [SerializeField] private float fadeSpeed = .25f;
        private string _currentScene = string.Empty;

        private async void OnEnable()
        {
            OnChangeScene += ChangeSceneAsync;

            await SceneManager.LoadSceneAsync(firstScene, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(firstScene));
            _currentScene = firstScene;

            DOTween.Kill(fade);
            fade.color = Color.black;
            await fade.DOColor(Color.clear, fadeSpeed);
        }

        private void OnDisable() => OnChangeScene -= ChangeSceneAsync;

        public static void ChangeScene(string nextScene)
        {
            if (_changing)
                return;
            _changing = true;
            
            OnChangeScene(nextScene);
        }

        private async void ChangeSceneAsync(string nextScene)
        {
            DOTween.Kill(fade);

            fade.color = Color.clear;
            await fade.DOColor(Color.black, fadeSpeed);

            await SceneManager.UnloadSceneAsync(_currentScene);
            await SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);

            _currentScene = nextScene;

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextScene));

            await fade.DOColor(Color.clear, fadeSpeed);
            _changing = false;
        }
    }
}
