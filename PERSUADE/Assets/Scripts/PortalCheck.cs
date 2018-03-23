using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCheck : MonoBehaviour {

    public Vector3 pos;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.transform.SetPositionAndRotation(pos, Quaternion.identity);
        }
    }

}
