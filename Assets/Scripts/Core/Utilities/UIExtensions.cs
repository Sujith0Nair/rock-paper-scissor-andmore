using UnityEngine.UI;
using UnityEngine.Events;

namespace Core.Utilities
{
    public static class UIExtensions
    {
        public static void RemoveAllAndAddNewListener(this Button.ButtonClickedEvent onClick, UnityAction action)
        {
            onClick.RemoveAllListeners();
            onClick.AddListener(action);
        }
    }
}