using Core;
using TMPro;
using System;
using Core.Enums;
using UnityEngine;
using UnityEngine.UI;
using Core.Utilities;
using Core.ScriptableObjects;
using Bootstrap.ScriptableObjects;

namespace HomeScene
{
    public class HomeSceneUI : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button howToPlayButton;
        [SerializeField] private TextMeshProUGUI highScore;
        [SerializeField] private GameEventChannel gameEventChannel;
        [SerializeField] private PopupEventChannel popupEventChannel;
        
        private void Start()
        {
            if (playButton == null)
            {
                throw new NullReferenceException("playButton is unassigned!");
            }
            if (howToPlayButton == null)
            {
                throw new NullReferenceException("howToPlayButton is unassigned!");
            }
            if (gameEventChannel == null)
            {
                throw new NullReferenceException("GameEventChannel is unassigned!");
            }
            if (popupEventChannel == null)
            {
                throw new NullReferenceException("PopupEventChannel is unassigned!");
            }
            
            playButton.onClick.RemoveAllAndAddNewListener(gameEventChannel.RequestSceneChange);
            howToPlayButton.onClick.RemoveAllAndAddNewListener(() =>
            {
                popupEventChannel.RaisePopup(PopupType.Tutorial, null);
            });
            highScore.text = highScore.text.Replace("{0}", ScoreManager.GetScore().ToString());
        }
    }
}