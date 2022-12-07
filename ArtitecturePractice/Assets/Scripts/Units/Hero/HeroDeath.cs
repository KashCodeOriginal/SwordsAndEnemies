using Hero;
using Units.Enemy.Logic;
using UnityEngine;

namespace Units.Hero
{
   public class HeroDeath : MonoBehaviour
   {
      [SerializeField] private HeroHealth _heroHealth;
      [SerializeField] private HeroMovement _heroMovement;
      [SerializeField] private HeroAttack _heroAttack;

      [SerializeField] private GameObject _deathFX;

      [SerializeField] private HeroAnimator _heroAnimator;

      private bool _isDead;

      public bool IsDead => _isDead;

      private void Start()
      {
         _heroHealth.IsUnitDead += Die;
      }

      private void Die()
      {
         if (_isDead)
         {
            return;
         }
      
         _isDead = true;
      
         /*_heroMovement.StopMovement();*/
         _heroAttack.enabled = false;
         

         Instantiate(_deathFX, transform.position, Quaternion.identity);
         
         _heroAnimator.PlayDeath();
      }

      private void OnDisable()
      {
         _heroHealth.IsUnitDead -= Die;
      }
   }
}
