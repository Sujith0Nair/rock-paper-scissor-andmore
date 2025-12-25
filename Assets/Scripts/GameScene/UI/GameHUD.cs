using Core;
using TMPro;
using UnityEngine;
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
        [SerializeField] private TextMeshProUGUI messageText;

        private void Start()
        {
            if (handsUIHandler != null && handsHolder != null)
            {
                handsUIHandler.Initialize(handsHolder.GetAllHandsData());
            }
            UpdateScoreUI(ScoreManager.GetScore());
            ShowMessage("");
        }

        private void OnEnable()
        {
            if (gameRoundManager == null) return;
            gameRoundManager.OnScoreUpdated += UpdateScoreUI;
            gameRoundManager.OnGameMessage += ShowMessage;
        }

        private void OnDisable()
        {
            if (gameRoundManager == null) return;
            gameRoundManager.OnScoreUpdated -= UpdateScoreUI;
            gameRoundManager.OnGameMessage -= ShowMessage;
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
    }
}