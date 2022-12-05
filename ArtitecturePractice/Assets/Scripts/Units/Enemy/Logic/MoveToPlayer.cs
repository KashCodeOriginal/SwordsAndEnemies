using Pathfinding;
using UnityEngine;

namespace Enemy.Logic
{
    public class MoveToPlayer : MonoBehaviour
    {
        [SerializeField] private AIDestinationSetter _aiDestinationSetter;
        private const float REACHED_POINT_DISTANCE = 1;

        private Transform _currentTarget;

        private void Update()
        {
            if (IsHeroReached())
            {
            
            }
        }

        public void SetTarget(Transform target)
        {
            _currentTarget = target;
        
            _aiDestinationSetter.target = _currentTarget;
        }

        private bool IsHeroReached()
        {
            return Vector3.Distance(transform.position, _currentTarget.position) <= REACHED_POINT_DISTANCE;
        }
    }
}
