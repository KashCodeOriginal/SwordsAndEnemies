using Services.AssetsProvider;
using Services.IAP.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ShopUI
{
    public class ShopItem : MonoBehaviour
    {
        public void Construct(IIAPService iapService, IAddressableAssetProvider assetsProvider,
            ProductDescription productDescription)
        {
            _iapService = iapService;
            _assetsProvider = assetsProvider;
            
            _productDescription = productDescription;
        }
        
        [SerializeField] private Button _buyItemButton;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _quantityText;
        [SerializeField] private TextMeshProUGUI _availableItemsAmountText;
        [SerializeField] private Image _itemImage;
        
        private ProductDescription _productDescription;
        private IIAPService _iapService;
        private IAddressableAssetProvider _assetsProvider;

        public Button BuyItemButton => _buyItemButton;

        public TextMeshProUGUI PriceText => _priceText;

        public TextMeshProUGUI QuantityText => _quantityText;

        public TextMeshProUGUI AvailableItemsAmountText => _availableItemsAmountText;
        
        public Image ItemImage => _itemImage;

        public async void Initialize()
        {
            BuyItemButton.onClick.AddListener(OnBuyItemClicked);

            _priceText.text = _productDescription.ProductConfig.Price; 
            _quantityText.text = _productDescription.ProductConfig.Quantity.ToString();
            _availableItemsAmountText.text = _productDescription.AvailablePurchaseAmount.ToString();
            _itemImage.sprite = await _assetsProvider.GetAsset<Sprite>(_productDescription.ProductConfig.Icon);
        }

        private void OnBuyItemClicked()
        {
            _iapService.StartPurchase(_productDescription.ID);
        }
    }
}