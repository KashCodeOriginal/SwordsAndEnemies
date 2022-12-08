using Data.Player;
using Services.PersistentProgress;
using Units.Enemy.Logic;
using UnityEngine;
using UnityEngine.Events;

namespace Units.Hero
{
    public class HeroHealth : MonoBehaviour, IProgressSavable, IHealth
    {
        [SerializeField] private float _healthPoints;
        [SerializeField] private float _maxHealthPoints;

        [SerializeField] private HeroAnimator _heroAnimator;

        public float HealthPoints => _heroState.CurrentHP;

        public float MaxHealthPoints => _heroState.MaxHP;

        private HeroState _heroState;

        public event UnityAction<float> IsHealthChanged;

        public event UnityAction IsUnitDead;

        public void LoadProgress(PlayerProgress playerProgress)
        {
            _heroState = playerProgress.HeroState;

            _healthPoints = _heroState.CurrentHP;
            
            IsHealthChanged?.Invoke(_healthPoints);
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            playerProgress.HeroState.CurrentHP = _healthPoints;
            playerProgress.HeroState.MaxHP = _maxHealthPoints;
        }

        public void SetUp(float healthPoints, float maxHealthPoints)
        {
            _healthPoints = healthPoints;
            _maxHealthPoints = maxHealthPoints;
        }

        public void TakeDamage(float damage)
        {
            if (_healthPoints - damage <= 0)
            {
                IsHealthChanged?.Invoke(_healthPoints);
                IsUnitDead?.Invoke();
                return;
            }
        
            _healthPoints -= damage;
            
            IsHealthChanged?.Invoke(_healthPoints);
            
            _heroAnimator.PlayHit();
        }
    }
}