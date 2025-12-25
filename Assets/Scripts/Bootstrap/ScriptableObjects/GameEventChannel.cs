using System;
using UnityEngine;

namespace Bootstrap.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameEventChannel", menuName = "Events/GameEventChannel", order = 0)]
    public class GameEventChannel : ScriptableObject
    {
        internal event Action OnSceneChangeRequested;
        internal event Action OnMusicToggleRequested;

        public void RequestSceneChange()
        {
            OnSceneChangeRequested?.Invoke();
        }

        public void ToggleMusic()
        {
            
        }
    }
}