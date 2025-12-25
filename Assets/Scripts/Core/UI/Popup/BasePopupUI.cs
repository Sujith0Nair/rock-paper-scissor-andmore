using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Core.UI.Popup
{
    public class BasePopupUI : MonoBehaviour
    {
        [Header("Base Settings")]
        [SerializeField] protected Button closeButton;
        [SerializeField] protected Image timerFillImage;
        [SerializeField] protected bool canAutoClose = true;
        [SerializeField] protected float autoCloseDuration = 5f;

        private Action _onCloseCallback;
        private Coroutine _timerCoroutine;

        private void Awake()
        {
            if (closeButton != null)
                closeButton.onClick.AddListener(ClosePopup);
        }

        public void Initialize(Action onClose)
        {
            _onCloseCallback = onClose;
            StartTimer();
        }

        private void StartTimer()
        {
            if (!canAutoClose)
            {
                return;
            }
            if (_timerCoroutine != null) StopCoroutine(_timerCoroutine);
            _timerCoroutine = StartCoroutine(TimerRoutine());
        }

        private IEnumerator TimerRoutine()
        {
            var elapsed = 0f;
            while (elapsed < autoCloseDuration)
            {
                elapsed += Time.deltaTime;
                if (timerFillImage != null)
                    timerFillImage.fillAmount = 1f - (elapsed / autoCloseDuration);
                yield return null;
            }
            ClosePopup();
        }

        private void ClosePopup()
        {
            if (_timerCoroutine != null) StopCoroutine(_timerCoroutine);
            _onCloseCallback?.Invoke();
            Destroy(gameObject);
        }
    }
}