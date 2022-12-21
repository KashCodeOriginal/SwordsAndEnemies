using Services.Ads;
using Services.AssetsProvider;
using Services.IAP.Service;
using Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace UI.ShopUI
{
    public class ShopWindow : BaseWindow
    {
        public void Construct(IAdsService adsService, IIAPService iapService,
            IPersistentProgressService persistentProgressService, IAddressableAssetProvider addressableAssetProvider)
        {
            base.Construct(persistentProgressService);
            _adItem.Construct(adsService, persistentProgressService);
            _shopItemsContainer.Construct(iapService, persistentProgressService, addressableAssetProvider);
        }

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private RewardedAdItem _adItem;
        [SerializeField] private ShopItemsContainer _shopItemsContainer;

        protected override void Initialize()
        {
            _adItem.Initialize();
            _shopItemsContainer.Initialize();
            RefreshSkullText();
        }

        protected override void SubscribeUpdates()
        {
            _adItem.Subscribe();
            
            _shopItemsContainer.Subscribe();
            
            _playerProgress.WorldData.LootData.IsAmountChanged += RefreshSkullText;
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            
            _adItem.CleanUp();
            
            _shopItemsContainer.CleanUp();
            
            _playerProgress.WorldData.LootData.IsAmountChanged -= RefreshSkullText;
        }

        private void RefreshSkullText()
        {
            _text.text = _playerProgress.WorldData.LootData.Collected.ToString();
        }
    }
}