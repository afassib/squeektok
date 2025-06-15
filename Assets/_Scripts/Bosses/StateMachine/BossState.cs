using System;
using Bardent.Assets._Scripts.Bosses;
using Bardent.CoreSystem;
using UnityEngine;

namespace Bardent
{
    public class BossState
    {
        protected BossStateMachine stateMachine;
        protected BossBase boss;
        protected Core core;

        public float startTime { get; protected set; }

        protected string animBoolName;

        public BossState(BossBase etity, BossStateMachine stateMachine, string animBoolName)
        {
            this.boss = etity;
            this.stateMachine = stateMachine;
            this.animBoolName = animBoolName;
            core = boss.Core;
        }

        public virtual void Enter()
        {
            startTime = Time.time;
            if (boss.anim) boss.anim.SetBool(animBoolName, true);
            core.GetCoreComponent<Graphics>().SetAnimationVariable(animBoolName, true);
            DoChecks();
        }

        public virtual void Exit()
        {
            boss.anim.SetBool(animBoolName, false);
            core.GetCoreComponent<Graphics>().SetAnimationVariable(animBoolName, false);
        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void LogicPhysics()
        {
            DoChecks();
        }

        public virtual void DoChecks()
        {

        }

        public virtual void ReceiveMessage(BossMessage messageType)
        {
            
        }
    }
}
