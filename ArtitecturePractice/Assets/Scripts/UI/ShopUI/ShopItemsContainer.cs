using System.Collections.Generic;
using Data.Assets;
using Services.AssetsProvider;
using Services.IAP.Service;
using Services.PersistentProgress;
using UnityEngine;

namespace UI.ShopUI
{
    public class ShopItemsContainer : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _unavailableItems;
        private readonly List<GameObject> _shopItems = new List<GameObject>();

        [SerializeField] private Transform _itemsParent;

        private IIAPService _iapService;
        private IPersistentProgressService _persistentProgressService;
        private IAddressableAssetProvider _assetsProvider;

        public void Construct(IIAPService iapService, IPersistentProgressService persistentProgressService, IAddressableAssetProvider addressableAssetProvider)
        {
            _iapService = iapService;
            _persistentProgressService = persistentProgressService;
            _assetsProvider = addressableAssetProvider;     
        }
        
        public void Initialize()
        {
            RefreshItems();
        }

        public void Subscribe()
        {
            _iapService.Initialized += RefreshItems;
            _persistentProgressService.PlayerProgress.PurchaseData.AmountChanged += RefreshItems;
        }

        public void CleanUp()
        {
            _iapService.Initialized -= RefreshItems;
            _persistentProgressService.PlayerProgress.PurchaseData.AmountChanged -= RefreshItems;
        }

        private void RefreshItems()
        {
            UpdateUnavailableItems();

            if (!_iapService.IsInitialized)
            {
                return;
            }
            
            ClearShopItems();

            foreach (var productDescription in _iapService.Products())
            {
                CreateItem(productDescription);
            }
        }

        private async void CreateItem(ProductDescription productDescription)
        {
            var shopItemPrefab = await _assetsProvider.GetAsset<GameObject>(AssetsAddressablesConstants.SHOP_ITEM);

            var shopItem = Instantiate(shopItemPrefab, _itemsParent);

            if (shopItem.TryGetComponent(out ShopItem item))
            {
                _shopItems.Add(shopItem);

                item.Construct(_iapService, _assetsProvider, productDescription);
                item.Initialize();
            }
        }

        private void ClearShopItems()
        {
            foreach (var shopItem in _shopItems)
            {
                Destroy(shopItem);
            }
        }

        private void UpdateUnavailableItems()
        {
            foreach (var unavailableItem in _unavailableItems)
            {
                unavailableItem.SetActive(_iapService.IsInitialized);
            }
        }
    }
}