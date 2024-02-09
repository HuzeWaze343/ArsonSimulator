using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool
    {
    public abstract float ToolCooldown { get; set; }
    public abstract void UseTool(PlayerController playerController);
    }
