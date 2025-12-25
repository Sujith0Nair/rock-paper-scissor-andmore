using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Bootstrap.ScriptableObjects;

namespace Bootstrap
{
    public class Boostrap : MonoBehaviour 
    {
        [SerializeField] private GameEventChannel gameEventChannel;

        private Scene? _currentScene;
        private int _currentSceneIndex = -1;

        private void OnEnable()
        {
            if (gameEventChannel == null)
            {
                throw new NullReferenceException("GameEventChannel is unassigned in Bootstrap!");
            }
            gameEventChannel.OnSceneChangeRequested += SwitchScene;
        }

        private void OnDisable()
        {
            if (gameEventChannel == null) return;
            gameEventChannel.OnSceneChangeRequested -= SwitchScene;
        }

        private void Start()
        {
            SwitchScene();
        }

        private void SwitchScene()
        {
            var totalScenesToCycle = SceneManager.sceneCountInBuildSettings - 1;
            if (totalScenesToCycle <= 0)
            {
                Debug.LogError($"No playable scenes configured in Build Settings (excluding {gameObject.scene.name} scene).");
                return;
            }

            _currentSceneIndex = (_currentSceneIndex + 1) % totalScenesToCycle;
            var sceneBuildIndexToLoad = _currentSceneIndex + 1;
            
            StartCoroutine(LoadScene(sceneBuildIndexToLoad));
        }

        private IEnumerator LoadScene(int sceneBuildIndexToLoad)
        {
            if (_currentScene != null)
            {
                yield return SceneManager.UnloadSceneAsync(_currentScene.Value);
            }
            yield return SceneManager.LoadSceneAsync(sceneBuildIndexToLoad, LoadSceneMode.Additive);
            SetSceneActive(sceneBuildIndexToLoad);
        }

        private void SetSceneActive(int sceneBuildIndexToLoad)
        {
            var scene = SceneManager.GetSceneByBuildIndex(sceneBuildIndexToLoad);
            if (!scene.IsValid())
            {
                throw new Exception("Scene not found. Index: " + sceneBuildIndexToLoad);
            }
            _currentScene = scene;
            SceneManager.SetActiveScene(scene);
        }
    }
}