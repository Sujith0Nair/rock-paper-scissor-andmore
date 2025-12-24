using Core;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using Bootstrap.ScriptableObjects;

namespace HomeScene
{
    public class HomeSceneUI : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private TextMeshProUGUI highScore;
        [SerializeField] private GameEventChannel gameEventChannel;
        
        private void Start()
        {
            if (playButton == null)
            {
                throw new NullReferenceException("playButton is unassigned!");
            }
            if (gameEventChannel == null)
            {
                throw new NullReferenceException("GameEventChannel is unassigned!");
            }

            playButton.onClick.RemoveAllListeners();
            playButton.onClick.AddListener(gameEventChannel.RequestSceneChange);
            highScore.text = highScore.text.Replace("{0}", ScoreManager.GetScore().ToString());
        }
    }
}