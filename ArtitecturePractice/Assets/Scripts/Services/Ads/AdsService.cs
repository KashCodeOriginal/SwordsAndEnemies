using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Services.Ads
{
    public class AdsService : IAdsService/*, IUnityAdsShowListener, IUnityAdsLoadListener, IUnityAdsInitializationListener*/
    {
        public event Action RewardedVideoReady;
        private event Action _onVideoFinished;

        private const string ANDROID_GAME_ID = "5071271";
        private const string IOS_GAME_ID = "5071270";

        private const string RewardedVideoID = "RewardedVideo";

        private string _gameID;

        private readonly bool _testMode = true;

        private bool _isAdsReady;

        public bool IsAdsReady => _isAdsReady;

        public int RewardAmount => 13;

        public void Initialize()
        {
            /*_gameID = Application.platform == RuntimePlatform.Android ? ANDROID_GAME_ID : IOS_GAME_ID;
        
            Advertisement.Initialize(_gameID, _testMode, this);*/
        }

        public void ShowRewardedVideo(Action onVideoFinished)
        {
            /*Advertisement.Show(RewardedVideoID, this);
            
            _isAdsReady = false;
            
            _onVideoFinished = onVideoFinished;*/
        }
        
        public void OnInitializationComplete()
        {
            _isAdsReady = true;
            
            Debug.Log("Unity ads successfully loaded");
        }

        /*public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log("Unity ads loading failed");
        }*/

        public void OnUnityAdsAdLoaded(string placementId)
        {
            if (placementId == _gameID)
            {
                RewardedVideoReady?.Invoke();
            }
        }

        /*public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log("Unity ads failed to load");
        }*/

        public void OnUnityAdsShowStart(string placementId)
        {
            Debug.Log("Unity ads started");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            Debug.Log("Unity ads clicked");
        }

        /*public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            Debug.Log("Unity ads completed");

            switch (showCompletionState)
            {
                case UnityAdsShowCompletionState.SKIPPED:
                    Debug.Log("Unity ads skipped");
                    break;
                case UnityAdsShowCompletionState.COMPLETED:
                    _onVideoFinished?.Invoke();
                    Debug.Log("Unity ads completed");
                    break;
                case UnityAdsShowCompletionState.UNKNOWN:
                    Debug.Log("Unity ads unknown");
                    break;
            }

            _onVideoFinished = null;
        }*/

        /*public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.Log("Unity ads show failed" + error + message);
        }*/
    }
}
