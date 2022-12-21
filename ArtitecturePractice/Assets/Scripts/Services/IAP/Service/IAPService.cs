using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data.Player;
using Services.IAP.Provider;
using Services.PersistentProgress;
using UnityEngine.Purchasing;

namespace Services.IAP.Service
{
    public class IAPService : IIAPService
    {
        public IAPService(IAPProvider provider, IPersistentProgressService persistentProgressService)
        {
            _provider = provider;
            _persistentProgressService = persistentProgressService;
        }

        private readonly IAPProvider _provider;

        private readonly IPersistentProgressService _persistentProgressService;
        
        public bool IsInitialized => _provider.IsInitialized;

        public event Action Initialized;

        public List<ProductDescription> Products()
        {
            return ProductDescriptions().ToList();
        }

        public void Initialize()
        {
            _provider.Initialize(this);

            _provider.Initialized += () => Initialized?.Invoke();
        }

        public void StartPurchase(string productId)
        {
            _provider.StartPurchase(productId);
        }

        public PurchaseProcessingResult PurchaseProcess(Product purchasedProduct)
        {
            var providerProductConfig = _provider.ProductConfigs[purchasedProduct.definition.id];

            switch (providerProductConfig.ItemType)
            {
                case ItemType.Skulls:
                    _persistentProgressService.PlayerProgress.WorldData.LootData.Add(providerProductConfig.Quantity);
                    _persistentProgressService.PlayerProgress.PurchaseData.AddPurchase(purchasedProduct.definition.id);
                    break;
            }
            
            return PurchaseProcessingResult.Complete;
        }

        private IEnumerable<ProductDescription> ProductDescriptions()
        {
            var playerProgressPurchaseData = _persistentProgressService.PlayerProgress.PurchaseData;

            foreach (var productID in _provider.Products.Keys)
            {
                var productConfig = _provider.ProductConfigs[productID];
                var providerProduct = _provider.Products[productID];

                var boughtIAP = playerProgressPurchaseData.BoughtIAPs.Find(x => x.IAPId == productID);

                if (ProductBoughtOut(boughtIAP, productConfig))
                {
                    continue;
                }

                yield return new ProductDescription()
                {
                    ID = productID,
                    ProductConfig = productConfig,
                    Product = providerProduct,
                    AvailablePurchaseAmount = boughtIAP != null ? productConfig.MaxPurchaseCount - boughtIAP.Count: productConfig.MaxPurchaseCount
                };
            }
        }

        private static bool ProductBoughtOut(BoughtIAP boughtIAP, ProductConfig productConfig)
        {
            return boughtIAP != null && boughtIAP.Count >= productConfig.MaxPurchaseCount;
        }
    }
}