using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroHandler : MonoBehaviour {

    public bool gotAcess = false;
    public APIAIDoorman doorman;
    public ParticleSystem gateway;
    public BoxCollider PortalCol;

	void Start () {
        doorman = GameObject.Find("ChatbotDoorman").GetComponent<APIAIDoorman>();
        gateway.enableEmission = false;
        PortalCol.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(doorman.answerTextField.text.Contains("open") || doorman.answerTextField.text.Contains("enter"))
        {
            gotAcess = true;
        }

        if(gotAcess)
        {
            gateway.enableEmission = true;
            PortalCol.enabled = true;
            
        }
	}

}
