using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ShopUI
{
    public class BaseWindow : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private void Awake()
        {
            OnAwake();
        }

        protected virtual void OnAwake()
        {
            _closeButton.onClick.AddListener(()=>Destroy(gameObject));
        }
    }
}