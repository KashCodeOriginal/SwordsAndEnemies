using System;
using UnityEngine.Purchasing;

namespace Services.IAP.Provider
{
    [Serializable]
    public class ProductConfig
    {
        public string ID;
        
        public ProductType ProductType;
        
        public int MaxPurchaseCount;
        
        public ItemType ItemType;

        public int Quantity;
        
        public string Price;
        
        public string Icon;
    }
}