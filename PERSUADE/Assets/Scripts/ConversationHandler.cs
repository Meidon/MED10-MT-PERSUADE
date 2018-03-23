using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationHandler : MonoBehaviour {

    public bool isRoaming;
    public GameObject ChatbotUID;
    public Camera PlayerCam;
    public GameObject PlayerController;
    public bool isUIEnabled;
    public Canvas tempC;
    public bool lockCursor = true;
    public Canvas childC;

	void Start () {
        isRoaming = true;
        isUIEnabled = false;
        PlayerCam = Camera.main;
        PlayerController = GameObject.FindGameObjectWithTag("Player");
        childC = GetComponentInChildren<Canvas>();
	}
	
	void Update () {
		
        if(isRoaming)
        {
            isUIEnabled = false;
            PlayerController.GetComponent<Invector.CharacterController.vThirdPersonInput>().enabled = true;
        }
        if(isUIEnabled)
        {
            PlayerController.GetComponent<Invector.CharacterController.vThirdPersonInput>().enabled = false;

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            isRoaming = true;
            lockCursor = true;
            if(tempC != null)
            {
                tempC.enabled = false;
            }
                
        }

        Ray r = PlayerCam.ScreenPointToRay(Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(r, out hit, 20))
        {
            if(hit.transform.gameObject.tag == "Chatbot")
            {
                if(isUIEnabled == false)
                {
                    childC.enabled = true;
                } else
                {
                    childC.enabled = false;
                }
                
                tempC = hit.transform.gameObject.GetComponentInChildren<Canvas>();
            }
            else
            {
                tempC = null;
                childC.enabled = false;
            }

            if(tempC != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    lockCursor = false;
                    isRoaming = false;
                    isUIEnabled = true;
                    tempC.enabled = true;
                    
                }
            }

        }

        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockCursor;
    }
}
