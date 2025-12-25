using Core;
using System;
using Core.Enums;
using UnityEngine;
using GameScene.UI;
using System.Collections;
using GameScene.Utilities;
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
        public event Action OnRoundStarted;
        public event Action OnRoundEnded;
        public event Func<HandType?> GetComputerHandType;

        private void Start() => StartNewRound();

        private void OnDestroy() => Timer.StopCountdown();

        private void StartNewRound()
        {
            OnRoundStarted?.Invoke();
            handsUIHandler.ResetSelection();
            Timer.StartCountdown(roundDurationSeconds, OnRoundTimerEnded, OnTimerTickCallback);
        }

        private void OnTimerTickCallback(int secondsRemaining)
        {
            var progress = (float)secondsRemaining / roundDurationSeconds;
            OnTimerTick?.Invoke(secondsRemaining, progress);
        }

        private void OnRoundTimerEnded()
        {
            OnRoundEnded?.Invoke();
            StartCoroutine(ProcessRoundResult());
        }

        private IEnumerator ProcessRoundResult()
        {
            if (handsUIHandler.CurrentHand == null)
            {
                popupEventChannel.RaisePopup(PopupType.ComputerWon, TriggerSceneChange);
                yield break;
            }

            HandType? computerHandType = null;
            while (computerHandType == null)
            {
                computerHandType = GetComputerHandType?.Invoke();
                yield return null;
            }

            var playerHand = handsHolder.GetHandForType(handsUIHandler.CurrentHand.Value);
            var computerHand = handsHolder.GetHandForType(computerHandType.Value);

            if (HasPlayerWon(playerHand, computerHand))
            {
                var newScore = ScoreManager.GetScore() + 1;
                ScoreManager.SetScore(newScore);
                OnScoreUpdated?.Invoke(newScore);

                popupEventChannel.RaisePopup(PopupType.PlayerWon, StartNewRound);
            }
            else
            {
                if (playerHand == computerHand)
                {
                    popupEventChannel.RaisePopup(PopupType.Draw, StartNewRound);
                }
                else
                {
                    popupEventChannel.RaisePopup(PopupType.ComputerWon, TriggerSceneChange);
                }
            }
        }

        private void TriggerSceneChange()
        {
            gameEventChannel.RequestSceneChange();
        }

        private static bool HasPlayerWon(Hand player, Hand computer)
        {
            if (player == computer)
            {
                return false;
            }

            return player > computer;
        }
    }
}