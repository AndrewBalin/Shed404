using UnityEngine;
using Gameplay.AI.Common;

namespace Gameplay.AI
{
    [RequireComponent(typeof(Animator))]
    public class NpcAnimation : MonoBehaviour, INpcAnimation
    {
        private const string IdleStateName = "Idle";
        private const string WalkStateName = "Walk";

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayIdle()
        {
            _animator.Play(IdleStateName);
        }

        public void PlayWalk()
        {
            _animator.Play(WalkStateName);
        }
    }
}