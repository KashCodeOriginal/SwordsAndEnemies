using System;
using System.Collections.Generic;

namespace Data.Player
{
    [Serializable]
    public class PurchaseData
    {
        public List<BoughtIAP> BoughtIAPs = new List<BoughtIAP>();
        
        public event Action AmountChanged;

        public void AddPurchase(string id)
        {
            var boughtIAP = GetBoughtIAP(id);

            if (boughtIAP != null)
            {
                boughtIAP.Count++;
            }
            else
            {
                BoughtIAPs.Add(new BoughtIAP
                {
                    IAPId = id,
                    Count = 1
                });   
            }
            
            AmountChanged?.Invoke();
        }

        private BoughtIAP GetBoughtIAP(string id)
        {
            return BoughtIAPs.Find(x => x.IAPId == id);
        }
    }
}