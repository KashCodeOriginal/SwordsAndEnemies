using Data.Player;
using TMPro;
using UnityEngine;

namespace UI.GameplayScreen
{
    public class LootCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _countText;
        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            
            _worldData.LootData.IsAmountChanged += OnLootAmountChanged;
            
            OnLootAmountChanged();
        }

        private void OnLootAmountChanged()
        {
            _countText.text = _worldData.LootData.Collected.ToString();
        }
        
        private void OnDestroy()
        {
            _worldData.LootData.IsAmountChanged -= OnLootAmountChanged;
        }
    }
}
