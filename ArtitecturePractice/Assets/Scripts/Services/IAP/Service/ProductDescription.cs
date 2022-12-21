using Services.IAP.Provider;
using UnityEngine.Purchasing;

namespace Services.IAP.Service
{
    public class ProductDescription
    {
        public string ID;
        public Product Product;
        public ProductConfig ProductConfig;
        public int AvailablePurchaseAmount;
    }
}