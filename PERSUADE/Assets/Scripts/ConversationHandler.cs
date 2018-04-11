using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public List<GameObject> ChatbotCanvas = new List<GameObject>();
    public Invector.CharacterController.vThirdPersonController playerControl;

	void Start () {
        isRoaming = true;
        isUIEnabled = false;
        PlayerCam = Camera.main;
        PlayerController = GameObject.FindGameObjectWithTag("Player");
        playerControl = PlayerController.GetComponent<Invector.CharacterController.vThirdPersonController>();
        ChatbotCanvas.AddRange(GameObject.FindGameObjectsWithTag("Chatbot"));
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
            playerControl.input = new Vector2(0, 0);

        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            isRoaming = true;
            lockCursor = true;


            for (int i = 0; i < ChatbotCanvas.Count; i++)
            {
                ChatbotCanvas[i].GetComponentInChildren<Canvas>().enabled = false;
                ChatbotCanvas[i].GetComponentInChildren<InputField>().DeactivateInputField();
            }

        }


        Ray r = PlayerCam.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(r.origin, r.direction * 10, Color.yellow);
        RaycastHit hit;

        if (Physics.Raycast(r, out hit, 2))
        {
            if(hit.collider.transform.tag == "Chatbot")
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
            } else
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
                    tempC.GetComponentInChildren<InputField>().Select();
                    
                }
            }

        }

        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockCursor;
    }

}
