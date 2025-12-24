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
        [SerializeField] private Transform handsParent;
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
            foreach (var handData in handsData)
            {
                var instance = Instantiate(referenceHandUI, handsParent);
                instance.SetActive(true);
                var handItem = instance.GetComponentInChildren<HandUIItem>();
                handItem.Initialize(handData, SetupMainStageContents);
            }
        }

        internal void SetupMainStageContents(string handName, Sprite handSprite = null)
        {
            currentMessage.text = $"Player selected {handName}";
            selectionImage.sprite = handSprite;
        }

        internal void Cleanup()
        {
            for (var i = handsParent.childCount - 1; i > 0; i--)
            {
                Destroy(handsParent.GetChild(i).gameObject);
            }
        }

        private void OnDestroy() => Cleanup();
    }
}
