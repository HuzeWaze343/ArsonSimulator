using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
    {
    //constructor functions similar to an awake method
    public PlayerIdleState(PlayerController playerController, PlayerStateMachine playerStateMachine) : base(playerController, playerStateMachine)
        {
        }

    public override void EnterState()
        {
        //Debug.Log("Player entered idle state");
        //play idle animation
        }

    public override void ExitState()
        {
        //suspend idle animation
        }

    public override void UpdateState()
        {
        //enter use tool state if key is pressed and tool is off cooldown
        if (Input.GetAxisRaw("UseTool") != 0 && PlayerController.toolCooldown <= 0) PlayerController.StateMachine.ChangeState(PlayerController.UseToolState);

        //enter moving state if one or more movement keys are pressed and movement is not neutral
        if (Input.GetAxisRaw("Vertical") != 0 | Input.GetAxisRaw("Horizontal") != 0) PlayerController.StateMachine.ChangeState(PlayerController.MovingState);
        }
    }
