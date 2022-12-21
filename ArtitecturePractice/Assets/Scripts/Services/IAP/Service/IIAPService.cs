using System;
using System.Collections.Generic;
using UnityEngine.Purchasing;

namespace Services.IAP.Service
{
    public interface IIAPService : IService
    {
        bool IsInitialized { get; }
        event Action Initialized;
        List<ProductDescription> Products();
        void Initialize();
        void StartPurchase(string productId);
    }
}