using System;
using Units.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Units.Enemy.Logic
{
    public class CkeckAttackRange : MonoBehaviour
    {
        [SerializeField] private EnemyAttack enemyAttack;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void Start()
        {
            _triggerObserver.OnTriggerEntered += TriggerEntered;
            _triggerObserver.OnTriggerExited += TriggerExited;
            
            enemyAttack.DisableAttack();
        }

        private void TriggerEntered(Collider coll)
        {
            enemyAttack.EnableAttack();
        }

        private void TriggerExited(Collider coll)
        {
            enemyAttack.DisableAttack();
        }
    }
}
