using Units.Enemy.Logic;
using Units.Hero;
using UnityEngine;

namespace UI.GameplayScreen
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;
        private IHealth _health;

        private void Start()
        {
            _health.IsHealthChanged += ChangeHp;
        }

        public void SetUp(HeroHealth heroHealth)
        {
            _health = heroHealth;
        }

        private void ChangeHp(float value)
        {
            _hpBar.SetValue(value, _health.MaxHealthPoints);
        }

        private void OnDisable()
        {
            _health.IsHealthChanged -= ChangeHp;
        }
    }
}