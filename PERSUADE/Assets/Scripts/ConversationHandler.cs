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
    public Canvas logBook;
    public List<GameObject> ChatbotCanvas = new List<GameObject>();

	void Start () {
        isRoaming = true;
        isUIEnabled = false;
        PlayerCam = Camera.main;
        PlayerController = GameObject.FindGameObjectWithTag("Player");
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

        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            isRoaming = true;
            lockCursor = true;


            for (int i = 0; i < ChatbotCanvas.Count - 1; i++)
            {
                ChatbotCanvas[i].GetComponentInChildren<Canvas>().enabled = false;
            }

        }


        Ray r = PlayerCam.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(r.origin, r.direction * 10, Color.yellow);
        RaycastHit hit;

        if (Physics.Raycast(r, out hit, 3))
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
                StartCoroutine(delay(0.5f));
                childCE.enabled = false;
                childCQ.enabled = false;
                logBook.enabled = true;
            }

            if(tempC != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    lockCursor = false;
                    isRoaming = false;
                    isUIEnabled = true;
                    tempC.enabled = true;
                    logBook.enabled = false;
                    
                }
            }

        }

        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockCursor;
    }

    IEnumerator delay(float delay)
    {
        tempC = null;
        yield return new WaitForSeconds(delay);
        
    }
}
