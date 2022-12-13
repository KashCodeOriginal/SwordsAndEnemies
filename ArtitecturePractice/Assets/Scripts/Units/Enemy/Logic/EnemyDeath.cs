using System.Collections;
using Units.Enemy.Animation;
using UnityEngine;
using UnityEngine.Events;

namespace Units.Enemy.Logic
{
    public class EnemyDeath : MonoBehaviour
    {
        public event UnityAction IsEnemyDied;
    
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private GameObject _deathFx;
        [SerializeField] private float _timeToDestroyAfterDeath;
    
        private void Start()
        {
            _health.IsUnitDead += Die;
        }

        private void Die()
        {
            _health.IsUnitDead -= Die;
        
            PlayDeath();

            IsEnemyDied?.Invoke();

            StartCoroutine(DestroyBodyTimer());
        }

        private void PlayDeath()
        {
            _enemyAnimator.PlayDeath();

            Instantiate(_deathFx, transform.position, Quaternion.identity);
        }

        private IEnumerator DestroyBodyTimer()
        {
            yield return new WaitForSeconds(_timeToDestroyAfterDeath);
        
            Destroy(gameObject);
        }

        private void OnDisable()
        {
            _health.IsUnitDead -= Die;
        }
    }
}
