using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdleState : PlayerGroundedState {
	public PlayerCrouchIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
	}

	public override void Enter() {
		base.Enter();
        player.collider2D_.sharedMaterial = player.highFrictionMaterial;
        foreach (Collider2D go in player.grounds)
        {
            go.sharedMaterial = player.highFrictionMaterial;
        }
        Movement?.SetVelocityZero();
		player.SetColliderHeight(playerData.crouchColliderHeight);
	}

	public override void Exit() {
		base.Exit();
		player.SetColliderHeight(playerData.standColliderHeight);
        player.collider2D_.sharedMaterial = player.zeroFrictionMaterial;
        foreach (Collider2D go in player.grounds)
        {
            go.sharedMaterial = player.zeroFrictionMaterial;
        }
    }

	public override void LogicUpdate() {
		base.LogicUpdate();

		if (!isExitingState) {
			if (xInput != 0) {
				stateMachine.ChangeState(player.CrouchMoveState);
			} else if (yInput != -1 && !isTouchingCeiling) {
				stateMachine.ChangeState(player.IdleState);
			}
		}
	}
}
