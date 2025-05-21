using UnityEngine;

namespace GameBase.Scripts.Gameplay.TempQuest.Quests
{
    public class AnimationTracker : MonoBehaviour
    {
        private Animator animator;
        private bool actionExecuted = false;
        public string animationName = "YourAnimationName";

        public Camera sceneCamera;
        public Camera mainCamera;
        public GameObject player;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 0.95f && !actionExecuted)
            {
                actionExecuted = true;
                PerformAction();
            }
            else if (!stateInfo.IsName(animationName))
            {
                actionExecuted = false;
            }
        }

        void PerformAction()
        {
            player.gameObject.SetActive(true);
            mainCamera.gameObject.SetActive(true);
            sceneCamera.gameObject.SetActive(false);

            this.enabled = false;
        }
    }
}