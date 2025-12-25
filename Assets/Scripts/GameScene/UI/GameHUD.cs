using TMPro;
using Core.Enums;
using UnityEngine;
using UnityEngine.UI;
using GameScene.Managers;
using System.Collections;
using Core.ScriptableObjects.Holders;
using Core.Utilities;

namespace GameScene.UI
{
    internal class GameHUD : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameRoundManager gameRoundManager;
        [SerializeField] private HandsUIHandler handsUIHandler;
        [SerializeField] private HandsHolder handsHolder;
        
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI scoreText;

        [SerializeField] private Image computerImageBase;
        [SerializeField] private Image computerImage;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite unselectedBaseSprite;
        [SerializeField] private Sprite selectedBaseSprite;
        [SerializeField] private Button backButton;
        
        private HandType? _selectedHandType;
        private WaitForSeconds _waitForSeconds;

        private void InitializeHUD()
        {
            if (handsUIHandler != null && handsHolder != null)
            {
                handsUIHandler.Initialize(handsHolder.GetAllHandsData());
            }
            ShowMessage("Waiting for player's turn!");
            computerImage.sprite = defaultSprite;
            computerImage.color = new Color(1, 1, 1, 0);
            computerImageBase.sprite = unselectedBaseSprite;
            _selectedHandType = null;
            backButton.onClick.RemoveAllAndAddNewListener(gameRoundManager.SwitchScene);
        }

        private void OnEnable()
        {
            _waitForSeconds = new WaitForSeconds(1f);
            if (gameRoundManager == null) return;
            gameRoundManager.OnScoreUpdated += UpdateScoreUI;
            gameRoundManager.OnRoundStarted += InitializeHUD;
            gameRoundManager.OnRoundEnded += SelectHandForComputer;
            gameRoundManager.GetComputerHandType += GetComputerHandType;
        }

        private void OnDisable()
        {
            if (gameRoundManager == null) return;
            gameRoundManager.OnScoreUpdated -= UpdateScoreUI;
            gameRoundManager.OnRoundStarted -= InitializeHUD;
            gameRoundManager.OnRoundEnded -= SelectHandForComputer;
            gameRoundManager.GetComputerHandType -= GetComputerHandType;
        }

        private void UpdateScoreUI(int newScore)
        {
            if (scoreText != null)
            {
                scoreText.text = $"Score : {newScore}";
            }
        }

        private void ShowMessage(string message)
        {
            if (messageText != null)
            {
                messageText.text = message;
            }
        }

        private void SelectHandForComputer()
        {
            var data = handsHolder.GetRandomHandUIData();
            ShowMessage($"Computer selected: {data.Name}");
            computerImage.sprite = data.Sprite;
            computerImage.color = new Color(1, 1, 1, 1);
            computerImageBase.sprite = selectedBaseSprite;
            StartCoroutine(SetHandTypeAfterSecond(data.Type));
        }

        private IEnumerator SetHandTypeAfterSecond(HandType handType)
        {
            yield return _waitForSeconds;
            _selectedHandType = handType;
        }

        private HandType? GetComputerHandType() => _selectedHandType;
    }
}