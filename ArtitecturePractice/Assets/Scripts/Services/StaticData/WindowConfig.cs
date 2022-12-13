using System;
using UI.Services.WindowsService;
using UI.ShopUI;

namespace Services.StaticData
{
    [Serializable]
    public class WindowConfig
    {
        public WindowID WindowID;
        public BaseWindow Prefab;
    }
}