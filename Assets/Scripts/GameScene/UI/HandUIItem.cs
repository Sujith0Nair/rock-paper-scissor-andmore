using TMPro;
using System;
using Core.DTOs;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.UI
{
    internal class HandUIItem : MonoBehaviour
    {
        [SerializeField] private Image imageSlot;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Button selectionButton;

        private HandUIData _data;
        private Action<string, Sprite> _onCurrentItemSelected;
        
        internal void Initialize(HandUIData data, Action<string, Sprite> onCurrentItemSelected)
        {
            _data = data;
            _onCurrentItemSelected = onCurrentItemSelected;
            if (text != null) text.text = data.Name;
            if (imageSlot != null) imageSlot.sprite = data.Sprite;
            if (selectionButton == null) throw new NullReferenceException("Button reference cannot be null.");
            
            selectionButton.onClick.RemoveAllListeners();
            selectionButton.onClick.AddListener(OnHandUIItemSelected);
        }

        private void OnHandUIItemSelected()
        {
            _onCurrentItemSelected?.Invoke(_data.Name, imageSlot.sprite);
        }
    }
}