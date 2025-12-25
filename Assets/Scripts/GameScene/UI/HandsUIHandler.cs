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
        [SerializeField] private Image selectionImage;
        [SerializeField] private Sprite defaultSprite;
        
        internal HandType? CurrentHand { get; private set; }

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
            }
        }

        internal void ResetSelection()
        {
            CurrentHand = null;
            if (currentMessage != null) currentMessage.text = string.Empty;
            if (selectionImage == null) return;
            selectionImage.sprite = defaultSprite; 
            selectionImage.enabled = defaultSprite != null;
        }

        private void SetupMainStageContents(string handName, Sprite handSprite, HandType? handType)
        {
            currentMessage.text = $"Player selected {handName}";
            if (selectionImage != null)
            {
                selectionImage.sprite = handSprite;
                selectionImage.enabled = handSprite != null;
            }
            CurrentHand = handType;
        }

        private void Cleanup()
        {
            foreach (Transform child in handsParent)
            {
                if (child.gameObject == referenceHandUI) continue;
                Destroy(child.gameObject);
            }
        }

        private void OnDestroy() => Cleanup();
    }
}
