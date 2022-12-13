using System;
using UnityEngine;

namespace Units.Enemy.Animation
{
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Attack = Animator.StringToHash("Attack_1");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Hit = Animator.StringToHash("Hit");
        
        private static readonly int _idleStateHash = Animator.StringToHash("idle");
        private static readonly int _attackStateHash = Animator.StringToHash("attack01");
        private static readonly int _walkingStateHash = Animator.StringToHash("move");
        private static readonly int _deathStateHash = Animator.StringToHash("die");

        private Animator _animator;

        public event Action<AnimatorState> OnStateEntered;
        public event Action<AnimatorState> OnStateExited;
        
        public AnimatorState State { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayDeath() => _animator.SetTrigger(Die);
        public void PlayHit() => _animator.SetTrigger(Hit);

        public void Move(float speed)
        {
            _animator.SetBool(IsMoving, true);
            _animator.SetFloat(Speed, speed);
        }

        public void StopMovement() => _animator.SetBool(IsMoving, false);

        public void PlayAttack() => _animator.SetTrigger(Attack);

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            OnStateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash)
        {
            OnStateExited?.Invoke(StateFor(stateHash));
        }

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;

            if (stateHash == _idleStateHash)
            {
                state = AnimatorState.Idle;
            }
            else if (stateHash == _attackStateHash)
            {
                state = AnimatorState.Attack;
            }
            else if (stateHash == (_walkingStateHash))
            {
                state = AnimatorState.Walking;
            }
            else if (stateHash == _deathStateHash)
            {
                state = AnimatorState.Died;
            }
            else
            {
                state = AnimatorState.Unknown;
            }

            return state;
        }
    }
}