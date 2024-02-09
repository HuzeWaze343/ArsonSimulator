using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAlarm : MonoBehaviour
	{
	List<BurnableTile> burnableTiles;
	void Start()
		{
		//assemble list of tiles that the firealarm is keeping watch over
		Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(1f, 1f, 1f), Quaternion.identity, LayerMask.GetMask("Tiles"));
		burnableTiles = new List<BurnableTile>();
		int i = 0;
		while (i < colliders.Length)
            {
			burnableTiles.Add(colliders[i].GetComponentInParent<BurnableTile>());
			i++;
            }
		}

	void Update()
		{
		//check the tiles the firealarm is watching
		//if fire is detected on one of the tiles, start the fail timer and destroy the fire alarm script as its no longer needed
        foreach (var tile in burnableTiles)
            {
			if (tile.isBurning)
				{
				Debug.Log("Fire alarm detected a fire");
				GameObject.Find("GameController").GetComponent<GameController>().IsTimerActive = true;

				Destroy(this);
				}
            }
		}
	}
