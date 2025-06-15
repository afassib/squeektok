using System.Collections;
using Bardent.Assets._Scripts.Bosses.StateMachine.States;
using Bardent.CoreSystem;
using UnityEngine;

namespace Bardent.Assets._Scripts.Bosses
{
    public enum BossMessage
    {
        WakeBoss,
        EntranceEnded,
    }
    public class BossBase : MonoBehaviour
    {

        #region Boss attributes
        [SerializeField] public Core Core;
        [SerializeField] public Animator anim;
        // Data
        [SerializeField] public D_BossData _bossData = null;
        // State Machine
        public BossStateMachine _stateMachine { get; private set; }

        // states
        public BossAttackState S_BossAttackState { get; private set; }
        public BossDialogueState S_BossDialogueState { get; private set; }
        public BossEntranceState S_BossEntranceState { get; private set; }
        public BossIdleState S_BossIdleState { get; private set; }
        public BossPreDialogueState S_BossPreDialogueState { get; private set; }
        public BossSleepingState S_BossSleepingState { get; private set; }
        
        #endregion

        #region Unity methods
        private void Awake()
        {
            // initialize states
            S_BossAttackState = new BossAttackState(this, _stateMachine, "Attack");
            S_BossDialogueState = new BossDialogueState(this, _stateMachine, "Dialogue");
            S_BossEntranceState = new BossEntranceState(this, _stateMachine, "Entrance");
            S_BossIdleState = new BossIdleState(this, _stateMachine, "Idle");
            S_BossPreDialogueState = new BossPreDialogueState(this, _stateMachine, "Predialogue");
            S_BossSleepingState = new BossSleepingState(this, _stateMachine, "Sleep");
            _stateMachine = new BossStateMachine(S_BossSleepingState);
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            _stateMachine.currentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            _stateMachine.currentState.LogicPhysics();
        }
        #endregion

        public void AnimationTrigger(AnimationtriggerType type)
        {
            //m_Graphics.
            switch (type)
            {
                case AnimationtriggerType.EntryFinished:
                    break;
                case AnimationtriggerType.Attack:
                    break;
                case AnimationtriggerType.AttackFinished:
                    break;
                default:
                    break;
            }
        }

        public void ReceiveMessage(BossMessage messageType)
        {
            _stateMachine.currentState.ReceiveMessage(messageType);
        }

    }
}