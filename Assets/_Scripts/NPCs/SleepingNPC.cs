using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Bardent
{
    public class SleepingNPC : BaseNPC
    {
        #region parameters

        private Animator animator;
        [SerializeField] string animation_Sleeping_Name;
        [SerializeField] string animation_Awake_Name;
        [SerializeField] string animation_Sleeping_Name_Transition;
        [SerializeField] string animation_Awake_Name_Transition;
        [SerializeField] Vector2 sleepingPosition;
        [SerializeField] Vector2 awakePosition;
        [SerializeField] float transitionDuration; 
        private bool isTransitioning=false;

        #endregion

        #region sleep wake methods

        public override void Wake()
        {
            if (isAwake) return;
            if (hasAnimations && animator != null && !isTransitioning && animation_Awake_Name_Transition != "")
            {
                isTransitioning = true;
                animator.Play(animation_Awake_Name_Transition);
            }
            else if(hasTweening && !isTransitioning)
            {
                isTransitioning = true;
                StartCoroutine(WakeCoroutine());
            }
        }

        public override void Sleep()
        {
            if (!isAwake) return;
            if (hasAnimations && animator != null && !isTransitioning && animation_Sleeping_Name_Transition != "")
            {
                isTransitioning = true;
                animator.Play(animation_Awake_Name_Transition);
            }
            else if (hasTweening && !isTransitioning)
            {
                isTransitioning = true;
                StartCoroutine(WakeCoroutine());
            }
        }

        public void OnWakeAnimationFinished()
        {
            isTransitioning = false;
            isAwake = true;
        }

        public void OnSleepAnimationFinished()
        {
            isTransitioning = false;
            isAwake = false;
        }

        protected override IEnumerator WakeCoroutine()
        {
            coreObject.transform.DOLocalMove((Vector3)awakePosition, transitionDuration).OnComplete(() => {
                OnWakeAnimationFinished();
            });
            yield return null;
        }

        protected override IEnumerator SleepCoroutine()
        {
            coreObject.transform.DOMove((Vector3)sleepingPosition, transitionDuration).OnComplete(() => {
                OnSleepAnimationFinished();
            });
            yield return null;
        }

        #endregion

        #region other methods

        protected override void Awake()
        {
            base.Awake();
            isAwake = false;
            if (hasTweening) coreObject.transform.localPosition = (Vector3)sleepingPosition;
        }

        protected override void Start()
        {
            base.Start();
        }
        protected override void Update()
        {
            base.Update();
        }

        public override void Interact()
        {
            base.Interact();
            // Start Conversation
        }



        #endregion
    }
}
