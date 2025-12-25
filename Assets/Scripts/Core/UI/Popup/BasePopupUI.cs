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
        [SerializeField] protected float autoCloseDuration = 5f;

        protected Action OnCloseCallback;
        private Coroutine _timerCoroutine;

        private void Awake()
        {
            if (closeButton != null)
                closeButton.onClick.AddListener(ClosePopup);
        }

        public virtual void Initialize(Action onClose)
        {
            OnCloseCallback = onClose;
            StartTimer();
        }

        private void StartTimer()
        {
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

        protected virtual void ClosePopup()
        {
            if (_timerCoroutine != null) StopCoroutine(_timerCoroutine);
            OnCloseCallback?.Invoke();
            Destroy(gameObject);
        }
    }
}