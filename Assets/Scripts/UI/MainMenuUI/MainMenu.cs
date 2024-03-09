#pragma warning disable CS4014

using UnityEngine;
using DG.Tweening;
using TMPro;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using Powerlifter.Scenes;

namespace Powerlifter.UI.MainMenuUI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private string gameplayScene;

        [Header("References")]
        [SerializeField] private Transform title;
        [SerializeField] private TextMeshProUGUI[] letters;
        [SerializeField] private Transform buttons;

        [SerializeField] private Button startButton;
        [SerializeField] private Button quitButton;

        private const float showLetterTime = .15f;
        private const int waitLetterTime = 200;
        private const float showButtonsTime = 2f;

        async void Start()
        {
            foreach(var letter in letters)
            {
                letter.DOColor(Color.white, showLetterTime);

                letter.transform.DOScale(1.5f, showLetterTime).SetEase(Ease.Linear);
                await UniTask.Delay(waitLetterTime);
            }

            await buttons.DOMoveX(0f, showButtonsTime);

            ActivateButtons();
        }

        private async void StartGame()
        {
            ClearButtons();

            foreach (var letter in letters)
                letter.DOColor(Color.clear, showButtonsTime);

            await buttons.DOMoveX(-300, showButtonsTime);

            ScenesController.ChangeScene(gameplayScene);
        }

        private void ActivateButtons()
        {
            startButton.onClick.AddListener(StartGame);
            quitButton.onClick.AddListener(Application.Quit);
        }

        private void ClearButtons()
        {
            startButton.onClick.RemoveAllListeners();
            quitButton.onClick.RemoveAllListeners();
        }

    }
}
