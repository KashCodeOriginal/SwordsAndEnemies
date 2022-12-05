using System;
using Pathfinding;
using Units.Base;
using UnityEngine;

namespace Enemy.Logic
{
    public class MoveToPlayer : Aggre
    {
        [SerializeField] private AIDestinationSetter _aiDestinationSetter;
        [SerializeField] private AIPath _aiPath;
        
        private const float REACHED_POINT_DISTANCE = 1;

        private Transform _currentTarget;


        private void Update()
        {
            IsHeroReached();
        }

        public void SetTarget(Transform target)
        {
            _currentTarget = target;
        
            _aiDestinationSetter.target = _currentTarget;
        }

        private void IsHeroReached()
        {
            var distance = Vector3.Distance(transform.position, _currentTarget.position) <= REACHED_POINT_DISTANCE;
        }

        private void OnEnable()
        {
            _aiPath.enabled = true;
        }

        private void OnDisable()
        {
            _aiPath.enabled = false;
        }
    }
}
