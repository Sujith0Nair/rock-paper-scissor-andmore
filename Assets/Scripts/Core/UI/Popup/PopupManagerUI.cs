using System;
using Core.Enums;
using UnityEngine;
using Core.ScriptableObjects;
using System.Collections.Generic;

namespace Core.UI.Popup
{
    public class PopupManagerUI : MonoBehaviour
    {
        [Serializable]
        private struct PopupEntry
        {
            public PopupType type;
            public BasePopupUI prefab;
        }

        [Header("Dependencies")]
        [SerializeField] private PopupEventChannel popupEventChannel;
        [SerializeField] private Transform popupRoot;

        [Header("Configuration")]
        [SerializeField] private List<PopupEntry> popups;

        private Dictionary<PopupType, BasePopupUI> _popupMap;

        private void Awake()
        {
            InitializeMap();
        }

        private void InitializeMap()
        {
            _popupMap = new Dictionary<PopupType, BasePopupUI>();
            foreach (var entry in popups)
            {
                if (!_popupMap.ContainsKey(entry.type))
                {
                    _popupMap.Add(entry.type, entry.prefab);
                }
            }
        }

        private void OnEnable()
        {
            if (popupEventChannel != null)
                popupEventChannel.OnPopupRequested += ShowPopup;
        }

        private void OnDisable()
        {
            if (popupEventChannel != null)
                popupEventChannel.OnPopupRequested -= ShowPopup;
        }

        private void ShowPopup(PopupType type, Action onClose)
        {
            if (_popupMap.TryGetValue(type, out var prefab))
            {
                var popupInstance = Instantiate(prefab, popupRoot);
                popupInstance.Initialize(onClose);
            }
            else
            {
                Debug.LogWarning($"No popup prefab defined for type: {type}. Proceeding immediately.");
                onClose?.Invoke();
            }
        }
    }
}