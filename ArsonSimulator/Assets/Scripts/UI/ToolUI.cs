using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolUI : MonoBehaviour
	{
	GameObject toolHighlightBox;
	PlayerController player;
	[SerializeField]
	Tools matchingTool;
	enum Tools
        {
		Matches,
		Petrol,
		FlareGun
        }
	//I dont like this script but it works xdd
	void Start()
		{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		toolHighlightBox = transform.parent.Find("imgToolHighlight").gameObject;
        }

	void Update()
		{
		bool toolSelected = false;
        switch (matchingTool) //check if the tool that is equipped by the player matches the tool on this UI element
            {
            case Tools.Matches:
				if (player.currentTool is MatchTool) toolSelected = true;
                break;
            case Tools.Petrol:
				if (player.currentTool is PetrolTool) toolSelected = true;
				break;
            case Tools.FlareGun:
				if (player.currentTool is FlareGunTool) toolSelected = true;
				break;
            }

        if(toolSelected) //if the equipped tool matches, lerp the highlight box to this UI element and set the image opacity to 100%
			{
			gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
			toolHighlightBox.transform.position = Vector2.Lerp(toolHighlightBox.transform.position, transform.position, 0.1f);
			}
		else
            { //if the equipped tool doesnt match set the image opacity to 50%
			gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.3f);
            }
		}
	}
