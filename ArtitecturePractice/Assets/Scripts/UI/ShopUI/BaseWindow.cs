using System;
using Data.Player;
using Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ShopUI
{
    public abstract class BaseWindow : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        protected IPersistentProgressService _persistentProgressService;
        protected PlayerProgress _playerProgress => _persistentProgressService.PlayerProgress;

        public void Construct(IPersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;
        }

        private void Awake()
        {
            OnAwake();
        }

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy()
        {
            CleanUp();
        }

        protected virtual void OnAwake()
        {
            _closeButton.onClick.AddListener(()=>Destroy(gameObject));
        }

        protected virtual void Initialize() { }
        protected virtual void SubscribeUpdates() { } 
        protected virtual void CleanUp() { }
    }
}