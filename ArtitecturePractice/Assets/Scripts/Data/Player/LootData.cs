using System;
using Spawners.Loot;

namespace Data.Player
{
    [Serializable]
    public class LootData
    {
        public event Action IsAmountChanged;
        public int Collected;

        public void Collect(Loot loot)
        {
            Collected += loot.Value;
            IsAmountChanged?.Invoke();
        }
    }
}