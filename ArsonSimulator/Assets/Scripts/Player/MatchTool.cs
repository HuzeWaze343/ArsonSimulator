using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchTool : Tool
    {
    public override float ToolCooldown { get; set; } = 1f;

    //TOOL FUNCTIONALITY
    //Check the tile that the player is currently standing on and attempt to ignite it if the tile is ignitable
    public override void UseTool(PlayerController playerController)
        {
        RaycastHit hit;
        if (Physics.Raycast(playerController.rb.position, -playerController.transform.up, out hit, LayerMask.GetMask("Tiles"))) //fire a ray downwards looking for any tiles
            {
            BurnableTile b = hit.collider.transform.GetComponentInParent<BurnableTile>(); //check that the hit tile is burnable
            if (b != null)
                {
                b.isBurning = true;

                playerController.audioSource.clip = Resources.Load<AudioClip>("Sounds/Match");
                playerController.audioSource.Play();

                playerController.toolCooldown = ToolCooldown;

                }
            }
        }
    }
