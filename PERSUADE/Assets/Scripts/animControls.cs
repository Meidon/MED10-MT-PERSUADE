using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animControls : MonoBehaviour {

    public Animator anim;
    public InitializeDamarion damarion;

	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.Return))
        {
            anim.SetBool("isTalking", true);
        }

        if(damarion.animRespond == false)
        {
            anim.SetBool("isTalking", false);
        }

	}
}
