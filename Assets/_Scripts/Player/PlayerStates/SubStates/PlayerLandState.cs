using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if (xInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else if (isAnimationFinished)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }       
    }

    public override void Enter()
    {
        base.Enter();
        player.collider2D_.sharedMaterial = player.highFrictionMaterial;
        foreach (Collider2D go in player.grounds)
        {
            go.sharedMaterial = player.highFrictionMaterial;
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.collider2D_.sharedMaterial = player.zeroFrictionMaterial;
        foreach (Collider2D go in player.grounds)
        {
            go.sharedMaterial = player.zeroFrictionMaterial;
        }
    }
}
