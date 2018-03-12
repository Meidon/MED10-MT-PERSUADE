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

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isRoaming = true;
            if(tempC != null)
                tempC.enabled = false;
        }

        Ray r = PlayerCam.ScreenPointToRay(Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(r, out hit, 20))
        {
            if(hit.transform.gameObject.tag == "Chatbot")
            {
                tempC = hit.transform.gameObject.GetComponentInChildren<Canvas>();
            }

            if(tempC != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isRoaming = false;
                    isUIEnabled = true;
                    tempC.enabled = true;
                }
            }

        }
    }
}
