using Pathfinding;
using UnityEngine;

namespace Units.Enemy.Animation
{
    [RequireComponent(typeof(EnemyAnimator))]
    [RequireComponent(typeof(AIPath))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        [SerializeField] private AIPath _aiPath;
        [SerializeField] private EnemyAnimator _enemyAnimator;
        
        [SerializeField] private float _minimalWalkingVelocity;

        private void Update()
        {
            if (ShouldMove())
            {
                _enemyAnimator.Move(_aiPath.velocity.magnitude);
            }
            else
            {
                _enemyAnimator.StopMovement();
            }
        }

        private bool ShouldMove()
        {
            if (_aiPath.velocity.magnitude >= _minimalWalkingVelocity)
            {
                return true;
            }

            return false;
        }
    }
}
