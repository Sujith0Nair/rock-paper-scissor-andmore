using TMPro;
using Core.DTOs;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.UI
{
    internal class HandUIItem : MonoBehaviour
    {
        [SerializeField] private Image imageSlot;
        [SerializeField] private TextMeshProUGUI text;

        private HandUIData _data;
        
        internal void Initialize(HandUIData data)
        {
            _data = data;
            if (text != null ) text.text = data.Name;
            if (imageSlot != null ) imageSlot.sprite = data.Sprite;
        }
    }
}