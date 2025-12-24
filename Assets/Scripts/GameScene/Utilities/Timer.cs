using System;
using UnityEngine;
using System.Collections;

namespace GameScene.Utilities
{
    internal class Timer : MonoBehaviour
    {
        private static Timer _instance;

        private Action _onTimerOver;
        private Action<int> _onTick;
        private Coroutine _countdownCoroutine;
        private WaitForSeconds _waitForSeconds;

        internal static void StartCountdown(int countdownSeconds, Action onTimerOver, Action<int> onTick = null)
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<Timer>();
                if (_instance == null)
                {
                    var go = new GameObject("[GlobalTimer]");
                    _instance = go.AddComponent<Timer>();
                }
            }
            _instance.Run(countdownSeconds, onTimerOver, onTick);
        }

        internal static void StopCountdown()
        {
            if (_instance != null)
            {
                _instance.StopTimer();
            }
        }

        private void Awake()
        {
            _waitForSeconds = new WaitForSeconds(1f);
        }

        private void Run(int countdownSeconds, Action onTimerOver, Action<int> onTick)
        {
            StopTimer();
            if (countdownSeconds < 0)
            {
                Debug.LogWarning("Countdown duration cannot be negative. Timer not started.");
                return;
            }
            _onTimerOver = onTimerOver;
            _onTick = onTick;
            _countdownCoroutine = StartCoroutine(CountdownCoroutine(countdownSeconds));
        }

        private void StopTimer()
        {
            if (_countdownCoroutine != null)
            {
                StopCoroutine(_countdownCoroutine);
                _countdownCoroutine = null;
            }
            _onTimerOver = null;
            _onTick = null;
        }

        private IEnumerator CountdownCoroutine(int countdownSeconds)
        {
            for (var i = countdownSeconds; i >= 0; i--)
            {
                _onTick?.Invoke(i);
                yield return _waitForSeconds;
            }
            _onTimerOver?.Invoke();
            _countdownCoroutine = null;
            StopTimer();
        }

        private void OnDisable() => StopTimer();

        private void OnDestroy() => _instance = null;
    }
}