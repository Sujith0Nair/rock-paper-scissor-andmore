using System;

namespace Core.Enums
{
    [Flags]
    public enum HandType
    {
        Rock = 1 << 1,
        Paper =  1 << 2,
        Scissor = 1 << 3,
        Lizard = 1 << 4,
        Spock = 1 << 5,
    }
}