using Core;
using TMPro;
using Core.Enums;
using UnityEngine;
using UnityEngine.UI;
using GameScene.Managers;
using Core.ScriptableObjects.Holders;

namespace GameScene.UI
{
    public class GameHUD : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameRoundManager gameRoundManager;
        [SerializeField] private HandsUIHandler handsUIHandler;
        [SerializeField] private HandsHolder handsHolder;
        
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Image computerImage;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private Sprite defaultSprite;
        
        private HandType? _selectedHandType;

        private void InitializeHUD()
        {
            if (handsUIHandler != null && handsHolder != null)
            {
                handsUIHandler.Initialize(handsHolder.GetAllHandsData());
            }
            UpdateScoreUI(ScoreManager.GetScore());
            ShowMessage("Waiting for player's turn!");
            computerImage.sprite = defaultSprite;
            _selectedHandType = null;
        }

        private void OnEnable()
        {
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
                scoreText.text = newScore.ToString();
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
            _selectedHandType = data.Type;
        }

        private HandType? GetComputerHandType() => _selectedHandType;
    }
}