using UnityEngine;

namespace Bardent
{
    public class NormalNPC : BaseNPC
    {
        #region parameters

        private Animator animator;
        private Player player;

        #endregion

        #region methods

        protected override void Awake()
        {
            base.Awake();
            isAwake = true;
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
