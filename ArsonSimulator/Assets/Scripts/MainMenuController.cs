using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainMenuController : MonoBehaviour
	{
	List<GameObject> burnableTiles = new List<GameObject>();
	void Start()
		{
		burnableTiles = GameObject.FindGameObjectsWithTag("BurnableTile").ToList<GameObject>();
		int rng = Random.Range(0, burnableTiles.Count);
		burnableTiles[rng].GetComponent<BurnableTile>().isBurning = true;
		
		}
	}
