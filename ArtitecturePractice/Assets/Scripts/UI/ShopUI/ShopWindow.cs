using Services.Ads;
using Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace UI.ShopUI
{
    public class ShopWindow : BaseWindow
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private RewardedAdItem _adItem;

        public void Construct(IAdsService adsService, IPersistentProgressService persistentProgressService)
        {
            base.Construct(persistentProgressService);
            _adItem.Construct(adsService, persistentProgressService);
        }
        
        protected override void Initialize()
        {
            _adItem.Initialize();
            
            RefreshSkullText();
        }

        protected override void SubscribeUpdates()
        {
            _adItem.Subscribe();
            
            _playerProgress.WorldData.LootData.IsAmountChanged += RefreshSkullText;
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            
            _adItem.CleanUp();
            
            _playerProgress.WorldData.LootData.IsAmountChanged -= RefreshSkullText;
        }

        private void RefreshSkullText()
        {
            _text.text = _playerProgress.WorldData.LootData.Collected.ToString();
        }
    }
}