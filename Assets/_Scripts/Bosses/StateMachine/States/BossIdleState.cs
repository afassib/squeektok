using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Bardent.Assets._Scripts.Bosses.StateMachine.States
{
    public class BossIdleState : BossState
    {
        public BossIdleState(BossBase etity, BossStateMachine stateMachine, string animBoolName) : base(etity, stateMachine, animBoolName)
        {
        }

        
    }
}