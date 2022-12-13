using Services;

namespace UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        public void CreateUIRoot();
        public void CreateShopWindow();
    }
}