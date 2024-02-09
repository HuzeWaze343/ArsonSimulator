using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraSpin : MonoBehaviour
	{
	void Update()
		{
		transform.rotation = Quaternion.Euler(0f, 0.1f, 0f) * transform.rotation;
		}
	}
