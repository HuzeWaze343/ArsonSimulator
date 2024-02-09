using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : PlayerState
    {
    //constructor functions similar to an awake method
    public PlayerMovingState(PlayerController playerController, PlayerStateMachine playerStateMachine) : base(playerController, playerStateMachine)
        {
        }

    public override void EnterState()
        {
        //Debug.Log("Player entered moving state");
        //play movement animation
        }

    public override void ExitState()
        {
        //suspend moving animation
        }

    public override void UpdateState()
        {
        //change state to using tool if input is detected & tool is off cooldown
        if (Input.GetAxisRaw("UseTool") != 0 && PlayerController.toolCooldown <= 0) PlayerController.StateMachine.ChangeState(PlayerController.UseToolState);

        Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        moveDirection = Quaternion.Euler(0, 45, 0) * moveDirection; //rotate movement direction to deal with iso perspective

        //change state to idle if no movement input is detected
        if (moveDirection.magnitude == 0) PlayerController.StateMachine.ChangeState(PlayerController.IdleState);

        PlayerController.rb.AddForce(moveDirection * PlayerController.moveSpeed, ForceMode.Force);
        }
    }
