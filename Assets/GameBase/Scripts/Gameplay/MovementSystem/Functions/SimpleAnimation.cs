using UnityEngine;

namespace GameBase.Scripts.Gameplay.MovementSystem.Functions
{
    public class SimpleAnimation
    {
        private const string IdleParam = "Idle";
        private const string MoveParam = "Walk";
        
        private readonly Animator _animator;
        private readonly NavMeshMovement _move;

        public SimpleAnimation(Animator animator, NavMeshMovement move)
        {
            _animator = animator;
            _move = move;
        }

        public void Update()
        {
            if (_move.CurrentVelocity.sqrMagnitude > 0.01f)
                _animator.Play(MoveParam);
            else
                _animator.Play(IdleParam);
        }
    }
}