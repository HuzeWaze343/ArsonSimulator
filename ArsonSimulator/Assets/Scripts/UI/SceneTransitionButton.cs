using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionButton : MonoBehaviour
	{
	public void TransitionToScene(string scene)
        {
		SceneManager.LoadScene(scene);
        }
	}
