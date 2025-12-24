using TMPro;
using System;
using Core.DTOs;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace GameScene.UI
{
    internal class HandsUIHandler : MonoBehaviour
    {
        [SerializeField] private GameObject referenceHandUI;
        [SerializeField] private TextMeshProUGUI currentMessage;
        [SerializeField] private Image selectionImage;

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

            SetupMainStageContents("Waiting for player's turn!");
        }

        internal void Initialize(IReadOnlyList<HandUIData> handsData)
        {
            var parent = referenceHandUI.transform.parent;
            foreach (var handData in handsData)
            {
                var instance = Instantiate(referenceHandUI, parent);
                var handItem = instance.GetComponentInChildren<HandUIItem>();
                handItem.Initialize(handData, (handName, handSprite) =>
                {
                    SetupMainStageContents($"Player selected {handName}", handSprite);
                });
            }
        }

        internal void SetupMainStageContents(string message, Sprite sprite = null)
        {
            currentMessage.text = message;
            selectionImage.sprite = sprite;
        }

        internal void Cleanup()
        {
            var parent = referenceHandUI.transform.parent;
            for (var i = 1; i < parent.childCount; i++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }

        private void OnDestroy() => Cleanup();
    }
}