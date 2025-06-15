using Bardent.Assets._Scripts.Bosses;
using UnityEngine;

namespace Bardent
{
    public class BossSleepingState : BossState
    {
        public BossSleepingState(BossBase etity, BossStateMachine stateMachine, string animBoolName) : base(etity, stateMachine, animBoolName)
        {
        }

        public override void ReceiveMessage(BossMessage messageType)
        {
            if(messageType==BossMessage.WakeBoss)
                boss._stateMachine.ChangeState(boss.S_BossEntranceState);
        }
    }
}
