using System;

namespace Services.Ads
{
    public interface IAdsService : IService
    {
        public event Action RewardedVideoReady;
        public bool IsAdsReady { get; }
        public int RewardAmount { get; }
        public void Initialize();
        public void ShowRewardedVideo(Action onVideoFinished);
    }
}