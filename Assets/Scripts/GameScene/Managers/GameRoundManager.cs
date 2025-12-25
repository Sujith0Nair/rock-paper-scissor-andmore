using Core;
using System;
using UnityEngine;
using GameScene.UI;
using GameScene.Utilities;
using Core.Enums;
using Core.ScriptableObjects;
using Bootstrap.ScriptableObjects;
using Core.ScriptableObjects.Hands;
using Core.ScriptableObjects.Holders;

namespace GameScene.Managers
{
    public class GameRoundManager : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField, Range(1, 10)] private int roundDurationSeconds = 5;

        [Header("Dependencies")]
        [SerializeField] private HandsHolder handsHolder;
        [SerializeField] private GameEventChannel gameEventChannel;
        [SerializeField] private PopupEventChannel popupEventChannel;
        [SerializeField] private HandsUIHandler handsUIHandler;

        public event Action<int, float> OnTimerTick;
        public event Action<int> OnScoreUpdated;
        public event Action<string> OnGameMessage;

        private void Start() => ShowTutorial();

        private void OnDestroy() => Timer.StopCountdown();

        private void ShowTutorial()
        {
            popupEventChannel.RaisePopup(PopupType.Tutorial, StartNewRound);
        }

        private void StartNewRound()
        {
            handsUIHandler.ResetSelection();
            Timer.StartCountdown(roundDurationSeconds, OnRoundTimerEnded, OnTimerTickCallback);
        }

        private void OnTimerTickCallback(int secondsRemaining)
        {
            var progress = (float)secondsRemaining / roundDurationSeconds;
            OnTimerTick?.Invoke(secondsRemaining, progress);
        }

        private void OnRoundTimerEnded() => ProcessRoundResult();

        private void ProcessRoundResult()
        {
            if (handsUIHandler.CurrentHand == null)
            {
                // Treat time out as a loss or handle separately if needed
                popupEventChannel.RaisePopup(PopupType.ComputerWon, () => TriggerSceneChange());
                return;
            }

            var playerHand = handsHolder.GetHandForType(handsUIHandler.CurrentHand.Value);
            var computerHand = handsHolder.GetRandomHand();

            if (HasPlayerWon(playerHand, computerHand, out var message))
            {
                // Update score immediately or after popup? Usually implies the win happened, so update score now.
                var newScore = ScoreManager.GetScore() + 1;
                ScoreManager.SetScore(newScore);
                OnScoreUpdated?.Invoke(newScore);

                popupEventChannel.RaisePopup(PopupType.PlayerWon, StartNewRound);
            }
            else
            {
                if (playerHand == computerHand)
                {
                    OnGameMessage?.Invoke("Draw! Try again.");
                    Invoke(nameof(StartNewRound), 2f);
                }
                else
                {
                    popupEventChannel.RaisePopup(PopupType.ComputerWon, () => TriggerSceneChange());
                }
            }
        }

        private void TriggerSceneChange()
        {
            gameEventChannel.RequestSceneChange();
        }

        private static bool HasPlayerWon(Hand player, Hand computer, out string message)
        {
            if (player == computer)
            {
                message = "Draw!";
                return false;
            }

            if (player > computer)
            {
                message = "Player won!";
                return true;
            }

            message = "Player lost!";
            return false;
        }
    }
}