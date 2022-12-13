using Services;

namespace UI.Services.WindowsService
{
    public interface IWindowService : IService
    {
        public void Open(WindowID windowID);
    }
}