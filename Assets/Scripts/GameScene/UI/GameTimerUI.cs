using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GameScene.Managers;

namespace GameScene.UI
{
    public class GameTimerUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameRoundManager gameRoundManager;
        [SerializeField] private Image fillImage;
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private Gradient gradient;

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
            if (slider != null) slider.value = progress;
            if (fillImage != null) fillImage.color = gradient.Evaluate(progress);
        }
    }
}