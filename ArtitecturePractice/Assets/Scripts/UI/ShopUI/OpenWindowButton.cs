using UI.Services.WindowsService;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ShopUI
{
    public class OpenWindowButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private WindowID _windowID;
        private IWindowService _windowService;

        private void Awake()
        {
            _button.onClick.AddListener(Open);
        }

        public void Construct(IWindowService windowService)
        {
            _windowService = windowService;
        }

        private void Open()
        {
            _windowService.Open(_windowID);
        }
    }
}