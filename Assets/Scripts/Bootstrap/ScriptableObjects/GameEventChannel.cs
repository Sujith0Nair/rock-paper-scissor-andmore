using System;
using UnityEngine;

namespace Bootstrap.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameEventChannel", menuName = "Events/GameEventChannel", order = 0)]
    public class GameEventChannel : ScriptableObject
    {
        internal event Action OnSceneChangeRequested;

        public void RequestSceneChange()
        {
            OnSceneChangeRequested?.Invoke();
        }
    }
}