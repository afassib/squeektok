using UnityEngine;

namespace Bardent
{
    public class BossStateMachine
    {
        // Current State
        public BossState currentState {  get; private set; }

        public BossStateMachine(BossState currentState)
        {
            this.currentState = currentState;
            currentState.Enter();
        }

        public void ChangeState(BossState newState)
        {
            currentState.Exit();
            currentState = newState;
            currentState.Enter();
        }
    }
}
