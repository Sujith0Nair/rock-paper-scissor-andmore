using TMPro;
using System;
using Core.DTOs;
using Core.Enums;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace GameScene.UI
{
    internal class HandsUIHandler : MonoBehaviour
    {
        [SerializeField] private GameObject referenceHandUI;
        [SerializeField] private Transform handsParent;
        [SerializeField] private TextMeshProUGUI currentMessage;
        [SerializeField] private Image selectedBase;
        [SerializeField] private Image selectedHandIcon;
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite unselectedBaseSprite;
        [SerializeField] private Sprite selectedBaseSprite;
        
        internal HandType? CurrentHand { get; private set; }

        private List<HandUIItem> _handItems;

        private void Start()
        {
            if (referenceHandUI == null)
            {
                throw new NullReferenceException("ReferenceHandUI is null");
            }
            referenceHandUI.SetActive(false);
            if (currentMessage == null)
            {
                throw new NullReferenceException("CurrentMessage is null");
            }

            _handItems = new List<HandUIItem>();
            ResetSelection();
        }

        internal void Initialize(IReadOnlyList<HandUIData> handsData)
        {
            Cleanup();

            foreach (var handData in handsData)
            {
                var instance = Instantiate(referenceHandUI, handsParent);
                instance.SetActive(true);
                var handItem = instance.GetComponentInChildren<HandUIItem>();
                handItem.Initialize(handData, data =>
                {
                    SetupMainStageContents(data.Name, data.Sprite, data.Type);
                });
                _handItems.Add(handItem);
            }
        }

        internal void ResetSelection()
        {
            CurrentHand = null;
            if (currentMessage != null) currentMessage.text = string.Empty;
            if (selectedHandIcon != null)
            {
                selectedHandIcon.sprite = defaultSprite;
                selectedHandIcon.color = new Color(1,1,1, 0);
            }
            if (selectedBase != null) selectedBase.sprite = unselectedBaseSprite;
        }

        private void SetupMainStageContents(string handName, Sprite handSprite, HandType? handType)
        {
            currentMessage.text = $"Player selected {handName}";
            if (selectedBase != null) selectedBase.sprite = selectedBaseSprite;
            if (selectedHandIcon != null)
            {
                selectedHandIcon.sprite = handSprite;
                selectedHandIcon.color = new Color(1,1,1, 1);
            }
            CurrentHand = handType;
            for (var i = 0; i < _handItems.Count && handType != null; i++)
            {
                var handUIItem = _handItems[i];
                handUIItem.OnAnyHandUIItemSelected(handType.Value);
            }
        }

        private void Cleanup()
        {
            foreach (Transform child in handsParent)
            {
                if (child.gameObject == referenceHandUI) continue;
                Destroy(child.gameObject);
            }
            _handItems?.Clear();
        }

        private void OnDestroy() => Cleanup();
    }
}
