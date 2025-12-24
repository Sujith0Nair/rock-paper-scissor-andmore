using Core;
using System;
using UnityEngine;
using GameScene.UI;
using GameScene.Utilities;
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
        [SerializeField] private HandsUIHandler handsUIHandler;

        public event Action<int, float> OnTimerTick;
        public event Action<int> OnScoreUpdated;
        public event Action<string> OnGameMessage;

        private void Start() => StartNewRound();

        private void OnDestroy() => Timer.StopCountdown();

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
                HandleLoss("Time's Up!");
                return;
            }

            var playerHand = handsHolder.GetHandForType(handsUIHandler.CurrentHand.Value);
            var computerHand = handsHolder.GetRandomHand();

            if (HasPlayerWon(playerHand, computerHand, out var message))
            {
                HandleWin(message);
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
                    HandleLoss(message);
                }
            }
        }

        private void HandleWin(string message)
        {
            var newScore = ScoreManager.GetScore() + 1;
            ScoreManager.SetScore(newScore);
            
            OnScoreUpdated?.Invoke(newScore);
            OnGameMessage?.Invoke(message);

            Invoke(nameof(StartNewRound), 2f);
        }

        private void HandleLoss(string message)
        {
            OnGameMessage?.Invoke(message);
            Invoke(nameof(TriggerSceneChange), 2f);
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