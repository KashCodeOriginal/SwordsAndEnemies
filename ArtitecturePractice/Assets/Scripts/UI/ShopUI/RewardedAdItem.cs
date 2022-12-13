using Services.Ads;
using Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ShopUI
{
    public class RewardedAdItem : MonoBehaviour
    {
        [SerializeField] private Button _rewardedVideoButton;
        
        [SerializeField] private GameObject[] _adActiveObjects;
        [SerializeField] private GameObject[] _adInactiveObjects;
        
        private IAdsService _adsService;
        private IPersistentProgressService _progressService;

        public void Construct(IAdsService adsService, IPersistentProgressService progressService)
        {
            _adsService = adsService;
            _progressService = progressService;
        }

        public void Initialize()
        {
            _rewardedVideoButton.onClick.AddListener(OnRewardedVideoButtonClicked);
            RefreshAvailableAd();
        }

        public void Subscribe()
        {
            _adsService.RewardedVideoReady += RefreshAvailableAd;
        }

        public void CleanUp()
        {
            _adsService.RewardedVideoReady -= RefreshAvailableAd;
        }

        private void RefreshAvailableAd()
        {
            var adsServiceIsAdsReady = _adsService.IsAdsReady;

            foreach (var adActiveObject in _adActiveObjects)
            {
                adActiveObject.SetActive(adsServiceIsAdsReady);
            }
            
            foreach (var adInactiveObject in _adInactiveObjects)
            {
                adInactiveObject.SetActive(!adsServiceIsAdsReady);
            }
        }

        private void OnRewardedVideoButtonClicked()
        {
            _adsService.ShowRewardedVideo(OnVideoFinished);
        }

        private void OnVideoFinished()
        {
            _progressService.PlayerProgress.WorldData.LootData.Add(_adsService.RewardAmount);
        }
    }
}