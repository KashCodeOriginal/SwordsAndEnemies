using System.Linq;
using Units.Base;
using Units.Enemy.Animation;
using Units.Hero;
using UnityEngine;

namespace Units.Enemy.Logic
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private LayerMask _layerMask;

        private float _damage;
        private float _cooldown;
        private float _cleavage;
        private float _distance;

        private Transform _playerTransform;
        
        private float _currentCooldown;
        private bool _isAttacking;
        
        private bool _attackIsActive;

        private Collider[] _hits = new Collider[1];

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
            {
                StartAttack();
            }
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                hit.transform.GetComponent<IHealth>().TakeDamage(_damage);
            }
        }

        public void SetUp(float damage, float cooldown, float cleavage, float distance)
        {
            _damage = damage;
            _cooldown = cooldown;
            _cleavage = cleavage;
            _distance = distance;
        }

        public void EnableAttack()
        {
            _attackIsActive = true;
        }

        public void DisableAttack()
        {
            _attackIsActive = false;
        }

        private void OnAttackEnded()
        {
            _currentCooldown = _cooldown;

            _isAttacking = false;
        }

        private bool Hit(out Collider hit)
        {
            var hitCount = TryHit(out hit);

            return hitCount > 0;
        }

        public void SetUpPlayer(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        private void StartAttack()
        {
            transform.LookAt(_playerTransform);
            _enemyAnimator.PlayAttack();

            _isAttacking = true;
        }

        private int TryHit(out Collider hit)
        {
            var attackPointCenter = GetAttackPoint();
            var hitCount = TryGetHits(out hit, attackPointCenter);
            return hitCount;
        }

        private int TryGetHits(out Collider hit, Vector3 attackPointCenter)
        {
            var hitCount = Physics.OverlapSphereNonAlloc(attackPointCenter, _cleavage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitCount;
        }

        private Vector3 GetAttackPoint()
        {
            var position = transform.position;
            var attackPointCenter = new Vector3(position.x, position.y + 0.5f, position.z) + transform.forward * _distance;
            return attackPointCenter;
        }

        private void UpdateCooldown()
        {
            if (!CanAttack())
            {
                DecreaseCooldown();
            }
        }

        private void DecreaseCooldown()
        {
            if (_cooldown <= 0)
            {
                return;
            }
            
            _currentCooldown -= Time.deltaTime;
        }

        private bool CanAttack()
        {
            return _currentCooldown <= 0 && !_isAttacking && _attackIsActive;
        }
    }
}
