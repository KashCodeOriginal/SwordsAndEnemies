using System;

namespace Data.Player
{
    [Serializable]
    public class HeroState
    {
        public float CurrentHP;
        public float MaxHP;

        public void ResetHP() => CurrentHP = MaxHP;
    }
}