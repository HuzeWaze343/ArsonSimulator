using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
    {
    protected PlayerController PlayerController;
    protected PlayerStateMachine PlayerStateMachine;

    public PlayerState(PlayerController playerController, PlayerStateMachine playerStateMachine)
        {
        this.PlayerController = playerController;
        this.PlayerStateMachine = playerStateMachine;
        }

    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void ExitState() { }
    }
