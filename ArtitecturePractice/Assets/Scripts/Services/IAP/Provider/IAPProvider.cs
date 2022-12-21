using System;
using System.Collections.Generic;
using System.Linq;
using Extentions;
using Services.IAP.Service;
using UnityEngine;
using UnityEngine.Purchasing;
using Product = UnityEngine.Purchasing.Product;

namespace Services.IAP.Provider
{
    public class IAPProvider : IStoreListener
    {
        private const string IAP_CONFIGS_PATH = "IAP/Products";
        
        private Dictionary<string, ProductConfig> _productConfigs;
        private Dictionary<string, Product> _products;

        private IExtensionProvider _extensions;
        private IStoreController _controller;
        private IAPService _aipService;

        public bool IsInitialized => _controller != null && _extensions != null;

        public Dictionary<string, ProductConfig> ProductConfigs => _productConfigs;
        public Dictionary<string, Product> Products => _products;

        public event Action Initialized;

        public void Initialize(IAPService aipService)
        {
            _aipService = aipService;
            
            _productConfigs = new Dictionary<string, ProductConfig>();
            _products = new Dictionary<string, Product>();
            
            Load();
            
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            foreach (var productConfig in ProductConfigs.Values)
            {
                builder.AddProduct(productConfig.ID, productConfig.ProductType);
            }
            
            UnityPurchasing.Initialize(this, builder);
        }

        public void StartPurchase(string productID)
        {
            _controller.InitiatePurchase(productID);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _controller = controller;
            _extensions = extensions;

            Initialized?.Invoke();
            
            foreach (var product in _controller.products.all)
            {
                _products.Add(product.definition.id, product);
            }
            
            Debug.Log("IAP initialized");
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log($"IAP not initialized: {error}");
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            Debug.Log($"Purchase completed: {purchaseEvent.purchasedProduct.definition.id}");

            return _aipService.PurchaseProcess(purchaseEvent.purchasedProduct);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log($"Purchase failed: {product.definition.id} with error: {failureReason}, transaction id: {product.transactionID}") ;
        }

        private void Load()
        {
            _productConfigs = 
                Resources.Load<TextAsset>(IAP_CONFIGS_PATH)
                    .text.ToDeserialized<ProductConfigWrapper>()
                    .Configs
                    .ToDictionary(x => x.ID, x => x);
            
            Debug.Log(_productConfigs);
        }
    }
}