using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseToolState : PlayerState
    {
    //constructor functions similar to an awake method
    public PlayerUseToolState(PlayerController playerController, PlayerStateMachine playerStateMachine) : base(playerController, playerStateMachine)
        {
        }

    public override void EnterState()
        {
        //Debug.Log("Player entered using tool state");
        //play using tool animation
        }

    public override void ExitState()
        {
        //suspend using tool animation
        }

    public override void UpdateState()
        {
        if (PlayerController.toolCooldown <= 0)
            {
            PlayerController.currentTool.UseTool(PlayerController);
            GameController.instance.toolsUsed++;
            }

        //enter moving state if one or more movement keys are pressed and movement is not neutral, else idle state
        if (Input.GetAxisRaw("Vertical") != 0 | Input.GetAxisRaw("Horizontal") != 0) PlayerController.StateMachine.ChangeState(PlayerController.MovingState);
        else PlayerController.StateMachine.ChangeState(PlayerController.IdleState);
        }
    }
