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
    public Canvas childCE;
    public Canvas childCQ;

	void Start () {
        isRoaming = true;
        isUIEnabled = false;
        PlayerCam = Camera.main;
        PlayerController = GameObject.FindGameObjectWithTag("Player");
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
            //childCQ.enabled = true;

        }

        if (Input.GetKeyDown(KeyCode.F1))
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
                    childCE.enabled = true;
                    childCQ.enabled = false;
                } else
                {
                    childCE.enabled = false;
                    childCQ.enabled = true;
                }
                
                tempC = hit.transform.gameObject.GetComponentInChildren<Canvas>();
            }
            else
            {
                tempC = null;
                childCE.enabled = false;
                childCQ.enabled = false;
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
