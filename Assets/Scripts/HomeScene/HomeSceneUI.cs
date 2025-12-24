using Core;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using Bootstrap.Interface;

namespace HomeScene
{
    public class HomeSceneUI : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private TextMeshProUGUI highScore;
        
        private ISceneHandler _sceneHandler;
        
        public void Initialize(ISceneHandler sceneHandler)
        {
            _sceneHandler = sceneHandler;
            InitializeUIComponents(sceneHandler);
        }

        private void InitializeUIComponents(ISceneHandler sceneHandler)
        {
            if (playButton == null)
            {
                throw new NullReferenceException("playButton is unassigned!");
            }
            playButton.onClick.RemoveAllListeners();
            playButton.onClick.AddListener(sceneHandler.SwitchScene);
            highScore.text = ScoreManager.GetScore().ToString();
        }
    }
}