using UI.GameplayScreen;
using Units.Base;
using Units.Enemy.Animation;
using UnityEngine;
using UnityEngine.Events;

namespace Units.Enemy.Logic
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        public event UnityAction<float> IsHealthChanged;
        public event UnityAction IsUnitDead;
        
        [SerializeField] private EnemyAnimator _enemyAnimator;

        [SerializeField] private float _healthPoints;
        [SerializeField] private float _maxHealthPoint;

        public float HealthPoints => _healthPoints;

        public float MaxHealthPoints => _maxHealthPoint;

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
            
            _enemyAnimator.PlayHit();
        }

        public void SetUp(float healthPoints, float maxHealthPoints)
        {
            _healthPoints = healthPoints;
            _maxHealthPoint = maxHealthPoints;
        }
    }
}
