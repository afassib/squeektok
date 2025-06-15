using Bardent.Assets._Scripts.Bosses;
using UnityEngine;

namespace Bardent
{
    public class BossAttackState : BossState
    {
        public BossAttackState(BossBase etity, BossStateMachine stateMachine, string animBoolName) : base(etity, stateMachine, animBoolName)
        {
        }
    }
}
