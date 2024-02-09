using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignPost : MonoBehaviour
	{
    bool isActive = false;
    GameObject signPostUI;
    TMP_Text signTextField;
    [SerializeField]
    string signTextContent = "ERR: NO TEXT ASSIGNED";
    private void Awake()
        {
        signPostUI = GameObject.FindGameObjectWithTag("SignPostUI");
        signTextField = signPostUI.GetComponentInChildren<TMP_Text>();
        }
    void Start()
		{
        signPostUI.SetActive(false);
		}

	void Update()
		{
        if (isActive) SetSignPosition();
		}
    private void OnTriggerEnter(Collider other)
        {
        if(other.CompareTag("Player"))
            {
            isActive = true;
            signPostUI.SetActive(true);
            signTextField.text = signTextContent;
            SetSignPosition();
            Debug.Log("Player entered area");
            }
        }
    private void OnTriggerExit(Collider other)
        {
        if(other.CompareTag("Player"))
            {
            isActive = false;
            signPostUI.SetActive(false);
            Debug.Log("Player left area");
            }
        
        }
    private void SetSignPosition()
        {
        signPostUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }
    }
