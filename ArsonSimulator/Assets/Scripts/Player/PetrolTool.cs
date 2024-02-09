using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetrolTool : Tool
    {
    public override float ToolCooldown { get; set; } = 1f;

    //TOOL FUNCTIONALITY
    //Check that the tile the player is standing on is ignitable and apply petrol to it if its ignitable
    //Ignitable tiles with petrol on them Spread fire to neighbouring ignitable tiles instantly, as opposed to waiting until the tile reaches a spreadthreshold
    //Ignitable tiles with petrol on will also ignite if a flare merely flies over them
    public override void UseTool(PlayerController playerController)
        {
        RaycastHit hit;
        if(Physics.Raycast(playerController.rb.position, -playerController.transform.up, out hit, LayerMask.GetMask("Tiles")))
            {
            BurnableTile b = hit.collider.transform.GetComponentInParent<BurnableTile>();
            if (b != null)
                {
                b.hasPetrol = true;
                Transform t = b.transform.Find("Petrol");
                t.gameObject.SetActive(true);
                t.rotation = Quaternion.Euler(90, Random.Range(0f, 360f), 0); //rotates puddle transform for some visual variety

                playerController.audioSource.clip = Resources.Load<AudioClip>("Sounds/Pour");
                playerController.audioSource.Play();

                playerController.toolCooldown = ToolCooldown;
                }
            }
        }
    }
