using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
    {
    public Rigidbody rb;
    public AudioSource audioSource;

    #region State Machine Stuff
    public PlayerStateMachine StateMachine { get; set; }
    public PlayerIdleState IdleState { get; set; }
    public PlayerMovingState MovingState {get;set;}
    public PlayerUseToolState UseToolState { get; set; }
    #endregion
    #region Movement Variables
    [SerializeField] public float moveSpeed = 10.0f;
    #endregion
    #region Tool Variables
    public float toolCooldown;
    public Tool currentTool;
    int toolToEquip;

    public List<Tool> tools = new List<Tool> {
        new MatchTool(),
        new PetrolTool(),
        new FlareGunTool()
        };
    #endregion

    private void Awake()
        {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine);
        MovingState = new PlayerMovingState(this, StateMachine);
        UseToolState = new PlayerUseToolState(this, StateMachine);
        }

    void Start()
    {
        //sort out some variables
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.drag = moveSpeed * 0.5f;

        audioSource = GetComponent<AudioSource>();

        //equip initial tool (matches)
        currentTool = tools[0];

        //prep state machine
        StateMachine.Initialize(IdleState);
    }

    void Update()
    {
        if (Time.timeScale == 0) return; //dont do anything if game is paused
        if (toolCooldown > 0) toolCooldown -= Time.deltaTime; //reduce tool cooldown if a tool is currently on cooldown
        SwapTools(); //swap tools if appropriate
        StateMachine.CurrentState.UpdateState();
    }
    void SwapTools()
        {
        //try swap tools based on scrollwheel
        float input = Input.GetAxis("Mouse ScrollWheel");
        if (input != 0)
            {
            toolToEquip = tools.IndexOf(currentTool) - Convert.ToInt32(input);
            }

        //try swap tools based on keyboard input
        if (Input.GetKeyDown(KeyCode.Alpha1)) toolToEquip = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) toolToEquip = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) toolToEquip = 2;


        if (toolToEquip == -1) toolToEquip = tools.Count - 1; //loop to final tool if attempting to equip tool -1
        else if (toolToEquip == tools.Count) toolToEquip = 0; //loop to first tool if attempting to equip a tool too far in index

        //swap to new tool
        if (toolToEquip != tools.IndexOf(currentTool))
            {
            currentTool = tools[toolToEquip];

            Debug.Log("Equipped tool: " + currentTool.GetType());
            }
        }
    }
