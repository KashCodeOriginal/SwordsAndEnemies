 using UnityEngine.Events;

 namespace Units.Base
{
    public interface IHealth
    {
        public event UnityAction<float> IsHealthChanged;
        public event UnityAction IsUnitDead;
        public void TakeDamage(float damage);
        public void SetUp(float healthPoints, float maxHealthPoints);
        public float HealthPoints { get; }
        public float MaxHealthPoints { get; }
    }
}