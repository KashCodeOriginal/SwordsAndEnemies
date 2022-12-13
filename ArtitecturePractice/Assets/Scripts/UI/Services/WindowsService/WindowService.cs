using UI.Services.Factory;

namespace UI.Services.WindowsService
{
    public class WindowService : IWindowService
    {
        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        private readonly IUIFactory _uiFactory;

        public void Open(WindowID windowID)
        {
            switch (windowID)
            {
                case WindowID.Unknown:
                    break;
                case WindowID.Shop:
                    _uiFactory.CreateShopWindow();
                    break;
            }
        }
    }
}