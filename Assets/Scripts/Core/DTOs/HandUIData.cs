using System;
using Core.Enums;
using UnityEngine;

namespace Core.DTOs
{
    [Serializable]
    public struct HandUIData
    {
        public readonly string Name;
        public readonly Sprite Sprite;
        public readonly HandType Type;

        public HandUIData(string name, Sprite sprite, HandType type)
        {
            Name = name;
            Sprite = sprite;
            Type = type;
        }
    }
}