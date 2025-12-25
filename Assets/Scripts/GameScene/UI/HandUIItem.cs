using TMPro;
using System;
using Core.DTOs;
using Core.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.UI
{
    internal class HandUIItem : MonoBehaviour
    {
        [SerializeField] private Image imageSlot;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Button selectionButton;
        [SerializeField] private Sprite unselectedSprite;
        [SerializeField] private Sprite selectedSprite;
        [SerializeField] private Image selectedBaseImage;

        private HandUIData _data;
        private Action<HandUIData> _onCurrentItemSelected;
        
        internal void Initialize(HandUIData data, Action<HandUIData> onCurrentItemSelected)
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
            _onCurrentItemSelected?.Invoke(_data);
            selectedBaseImage.sprite = selectedSprite;
        }

        internal void OnAnyHandUIItemSelected(HandType type)
        {
            if (type == _data.Type) return;
            selectedBaseImage.sprite = unselectedSprite;
        }
    }
}