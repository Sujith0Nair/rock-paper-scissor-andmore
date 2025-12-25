using Core.DTOs;
using Core.Enums;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Core.ScriptableObjects.Hands;

namespace Core.ScriptableObjects.Holders
{
    [CreateAssetMenu(fileName = "HandsHolder", menuName = "Holders/HandsHolder", order = 0)]
    public class HandsHolder : ScriptableObject
    {
        [SerializeField]
        private List<Hand> allHands;

        public IReadOnlyList<HandUIData> GetAllHandsData()
        {
            return allHands == null ? 
                new List<HandUIData>() : 
                allHands.Select(hand => new HandUIData(hand.Name, hand.HandSprite, hand.MyType)).ToList();
        }

        public Hand GetHandForType(HandType type)
        {
            return allHands?.Find(hand => hand.MyType == type);
        }

        public HandUIData GetRandomHandUIData()
        {
            var randomHand = allHands?[Random.Range(0, allHands.Count)];
            return randomHand != null ? new HandUIData(randomHand.Name, randomHand.HandSprite, randomHand.MyType) : default;
        }

        private void OnValidate()
        {
            if (allHands == null) return;
            var encounteredTypes = new HashSet<HandType>();
            for (var i = allHands.Count - 1; i >= 0; i--)
            {
                if (allHands[i] == null)
                {
                    allHands.RemoveAt(i);
                    continue;
                }
                
                if (encounteredTypes.Add(allHands[i].MyType)) continue;
                
                Debug.LogWarning($"Duplicate HandType '{allHands[i].MyType}' found in HandsHolder. Removing one instance.");
                allHands.RemoveAt(i);
            }
        }
    }
}