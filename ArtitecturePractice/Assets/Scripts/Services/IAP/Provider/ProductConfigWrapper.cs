using System;
using System.Collections.Generic;

namespace Services.IAP.Provider
{
    [Serializable]
    public class ProductConfigWrapper
    {
        public List<ProductConfig> Configs = new List<ProductConfig>();
    }
}