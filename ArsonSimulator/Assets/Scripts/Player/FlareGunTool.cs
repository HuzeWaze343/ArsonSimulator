using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareGunTool : Tool
    {
    public override float ToolCooldown { get; set; } = 1f;

    //FLARE FUNCTIONALITY
    //fires a flare in one of four directions (NE, SE, SW, NW)
    //if the flare flies over a tile that has petrol on it, light the tile on fire
    //when the flare hits an object, cease all movement and ignite the hit object if it is ignitable
    public override void UseTool(PlayerController playerController)
        {
        Vector3 mousePos = Input.mousePosition;
        Vector3 fireDirection = new Vector3();

        if (mousePos.x >= Screen.width / 2) fireDirection.z = -1;
        else fireDirection.z = 1;
        if (mousePos.y >= Screen.height / 2) fireDirection.x = 1;
        else fireDirection.x = -1;

        fireDirection = Quaternion.Euler(0, -45, 0) * fireDirection; //rotate vector for iso perspective

        Vector3 pos = playerController.gameObject.transform.position;

        GameObject.Instantiate(Resources.Load("Prefabs/FlarePrefab"), pos, Quaternion.LookRotation(fireDirection, Vector3.up));

        playerController.audioSource.clip = Resources.Load<AudioClip>("Sounds/FlareGun");
        playerController.audioSource.Play();

        playerController.toolCooldown = ToolCooldown;
        }
    }