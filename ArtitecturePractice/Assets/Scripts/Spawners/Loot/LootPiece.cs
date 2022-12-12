using System;
using System.Collections;
using Data.Player;
using TMPro;
using UnityEngine;

namespace Spawners.Loot
{
    public class LootPiece : MonoBehaviour
    {
        [SerializeField] private GameObject _scull;
        [SerializeField] private GameObject _pickupFx;
        [SerializeField] private TextMeshPro _lootText;
        [SerializeField] private GameObject _pickupPrefab;
        [SerializeField] private float _destroyTime;
        
        private Loot _loot;
        
        private bool _isLooted;
        
        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
        }

        public void Initialize(Loot loot)
        {
            _loot = loot;
        }

        public void OnTriggerEnter(Collider col)
        {
            PickUp();
        }

        private void PickUp()
        {
            if (_isLooted)
            {
                return;
            }
            
            _isLooted = true;

            UpdateWorldData();
            HideLoot();
            PlayPickUpFx();
            ActivatePopUp();
            
            StartCoroutine(StartDestroyTimer());
        }

        private void UpdateWorldData()
        {
            _worldData.LootData.Collect(_loot);
        }

        private void PlayPickUpFx()
        {
            Instantiate(_pickupFx, transform.position, Quaternion.identity);
        }

        private void ActivatePopUp()
        {
            _lootText.text = _loot.Value.ToString();
            _pickupPrefab.SetActive(true);
        }

        private void HideLoot()
        {
            _scull.SetActive(false);
        }

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(_destroyTime);
            
            Destroy(gameObject);
        }
    }
}