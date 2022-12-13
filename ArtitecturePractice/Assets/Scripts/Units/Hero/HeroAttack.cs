using System;
using Data.Player;
using Services.Input;
using Services.PersistentProgress;
using Services.ServiceLocator;
using Units.Base;
using Units.Enemy.Logic;
using UnityEngine;

namespace Units.Hero
{
    public class HeroAttack : MonoBehaviour, IProgressLoadable
    {
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private LayerMask _layerMask;

        private IInputService _inputService;
        
        private Collider[] _hits = new Collider[5];
        
        private HeroStats _playerStats;

        public void SetUp(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            if (_inputService.IsAttackButtonUp() && !_heroAnimator.IsAttacking)
            {
                _heroAnimator.PlayAttack();
            }
        }

        private void OnAttack()
        {
            if (Hit() > 0)
            {
                foreach (var hit in _hits)
                {
                    if (hit == null)
                    {
                        return;
                    }
                    
                    if (hit.transform.parent.TryGetComponent(out IHealth health))
                    {
                        health.TakeDamage(_playerStats.Damage);
                    }
                }
            }
        }

        private int Hit()
        {
            return Physics.OverlapSphereNonAlloc(AttackStartPoint(), _playerStats.DamageRadius, _hits, _layerMask);
        }

        private Vector3 AttackStartPoint()
        {
            var position = transform.position;
            return new Vector3(position.x, _characterController.center.y / 2, position.z) + transform.forward;
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            _playerStats = playerProgress.HeroStats;
        }
    }
}
