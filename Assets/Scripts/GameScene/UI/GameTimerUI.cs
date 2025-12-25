using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GameScene.Managers;
using System.Collections;

namespace GameScene.UI
{
    internal class GameTimerUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameRoundManager gameRoundManager;
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI timerText;

        private Coroutine _sliderCoroutine;
        
        private void OnEnable()
        {
            if (gameRoundManager != null)
            {
                gameRoundManager.OnTimerTick += UpdateTimerUI;
            }
        }

        private void OnDisable()
        {
            if (gameRoundManager != null)
            {
                gameRoundManager.OnTimerTick -= UpdateTimerUI;
            }
        }

        private void UpdateTimerUI(int secondsRemaining, float progress)
        {
            if (timerText != null) timerText.text = secondsRemaining.ToString();
            if (slider == null) return;
            StopCoroutine();
            _sliderCoroutine = StartCoroutine(LerpSlider(progress));
        }

        private IEnumerator LerpSlider(float progress)
        {
            var startValue = slider.value;
            var elapsed = 0f;
            const float duration = 1f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                slider.value = Mathf.Lerp(startValue, progress, elapsed / duration);
                yield return null;
            }
            slider.value = progress;
        }
        
        private void StopCoroutine()
        {
            if (_sliderCoroutine != null)
            {
                StopCoroutine(_sliderCoroutine);
            }
            _sliderCoroutine = null;
        }
    }
}