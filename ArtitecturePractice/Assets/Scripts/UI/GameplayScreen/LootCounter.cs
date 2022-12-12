using Data.Player;
using TMPro;
using UnityEngine;

namespace UI.GameplayScreen
{
    public class LootCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _countText;
        private WorldData _worldData;
        
        private void Start()
        {
            OnLootAmountChanged();
        }

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            
            _worldData.LootData.IsAmountChanged += OnLootAmountChanged;
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
