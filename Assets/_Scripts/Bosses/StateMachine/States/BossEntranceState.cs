using Bardent.Assets._Scripts.Bosses;
using UnityEngine;

namespace Bardent
{
    public class BossEntranceState : BossState
    {
        public BossEntranceState(BossBase etity, BossStateMachine stateMachine, string animBoolName) : base(etity, stateMachine, animBoolName)
        {
        }

        public override void ReceiveMessage(BossMessage messageType)
        {
            if(messageType==BossMessage.EntranceEnded)
                boss._stateMachine.ChangeState(boss.S_BossIdleState);
        }
    }
}
