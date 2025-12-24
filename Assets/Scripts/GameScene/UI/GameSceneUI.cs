using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Bootstrap.Interface;

namespace GameScene.UI
{
    public class GameSceneUI : MonoBehaviour
    {
        [SerializeField] private Gradient gradient;
        [SerializeField] private Image fillImage;
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI timerText;
        
        private ISceneHandler _sceneHandler;
        
        public void Initialize(ISceneHandler sceneHandler)
        {
            _sceneHandler = sceneHandler;
        }
    }
}
