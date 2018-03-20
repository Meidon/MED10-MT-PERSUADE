using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animControls : MonoBehaviour {

    public Animator anim;
    //public GameObject animObject;

	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.Return))
        {
            anim.SetBool("isTalking", true);
            StartCoroutine(w(1));
        }

        //if(animObject.GetComponent<APIAIDoorman>().animRespond == false)
        //{
        //    anim.SetBool("isTalking", false);
        //}

	}

    IEnumerator w (float del)
    {
        yield return new WaitForSeconds(del);
        anim.SetBool("isTalking", false);
    }
}
