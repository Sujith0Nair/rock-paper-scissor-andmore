using System;
using Core.Enums;
using UnityEngine;

namespace Core.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PopupEventChannel", menuName = "Events/PopupEventChannel")]
    public class PopupEventChannel : ScriptableObject
    {
        public event Action<PopupType, Action> OnPopupRequested;

        public void RaisePopup(PopupType type, Action onClose)
        {
            OnPopupRequested?.Invoke(type, onClose);
        }
    }
}