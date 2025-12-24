using System;
using Core.Enums;
using UnityEngine;

namespace Core.ScriptableObjects.Hands
{
    public abstract class Hand : ScriptableObject
    {
        internal string Name { get; private set; }
        internal Sprite HandSprite => handSprite;
        internal HandType MyType => myType;
        
        [SerializeField] private Sprite handSprite;
        [SerializeField] private HandType myType;
        [SerializeField] private HandType canDefeat;

        protected virtual void OnEnable()
        {
            Name = MyType.ToString();
        }

        public static bool operator >(Hand a, Hand b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.canDefeat.HasFlag(b.MyType);
        }

        public static bool operator <(Hand a, Hand b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return b.canDefeat.HasFlag(a.MyType);
        }

        public static bool operator ==(Hand a, Hand b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.MyType == b.MyType;
        }

        public static bool operator !=(Hand a, Hand b)
        {
            return !(a == b);
        }
        
        private bool Equals(Hand other)
        {
            return MyType == other.MyType;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType() && obj is not Hand) return false;
            return Equals((Hand)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), MyType);
        }
    }
}