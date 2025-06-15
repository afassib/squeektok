using System.Collections;
using UnityEngine;

namespace Bardent.Assets._Scripts.Bosses.StateMachine.States
{
    public class BossPreDialogueState : BossState
    {
        public BossPreDialogueState(BossBase etity, BossStateMachine stateMachine, string animBoolName) : base(etity, stateMachine, animBoolName)
        {
        }
    }
}