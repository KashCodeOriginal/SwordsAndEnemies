 using UnityEngine.Events;

namespace Units.Enemy.Logic
{
    public interface IHealth
    {
        public event UnityAction<float> IsHealthChanged;
        public event UnityAction IsUnitDead;
        public void TakeDamage(float damage);
        
        public float HealthPoints { get; }
        public float MaxHealthPoints { get; }
    }
}