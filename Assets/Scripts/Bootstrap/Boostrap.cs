using System;
using UnityEngine;
using System.Collections;
using Bootstrap.Interface;
using UnityEngine.SceneManagement;

namespace Bootstrap
{
    public class Boostrap : MonoBehaviour, ISceneHandler
    {
        private Scene? _currentScene;
        private int _currentSceneIndex = -1;

        private void Start() => SwitchScene();

        public void SwitchScene()
        {
            _currentSceneIndex = ++_currentSceneIndex % SceneManager.sceneCount - 1;
            var scene = SceneManager.GetSceneAt(_currentSceneIndex + 1);
            StartCoroutine(LoadScene(scene.name));
        }

        private IEnumerator LoadScene(string sceneName)
        {
            if (_currentScene != null)
            {
                yield return SceneManager.UnloadSceneAsync(_currentScene.Value);
            }
            yield return SceneManager.LoadSceneAsync(sceneName,  LoadSceneMode.Additive);
            SetSceneActive(sceneName);
        }

        private static void SetSceneActive(string sceneName)
        {
            var scene = SceneManager.GetSceneByName(sceneName);
            if (!scene.IsValid())
            {
                throw new Exception("Scene not found: " + sceneName);
            }
            SceneManager.SetActiveScene(scene);
        }
    }
}