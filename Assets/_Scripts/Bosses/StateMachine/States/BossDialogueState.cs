using System.Collections;
using UnityEngine;

namespace Bardent.Assets._Scripts.Bosses.StateMachine.States
{
    public class BossDialogueState : BossState
    {
        public BossDialogueState(BossBase etity, BossStateMachine stateMachine, string animBoolName) : base(etity, stateMachine, animBoolName)
        {
        }
    }
}