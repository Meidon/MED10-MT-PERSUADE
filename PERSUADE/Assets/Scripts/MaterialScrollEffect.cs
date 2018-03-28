using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialScrollEffect : MonoBehaviour {
    public float offsetSpeed = 0.5f;
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update () {
        float yOffset = Time.time * offsetSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(0,yOffset));

        if(yOffset >= 20)
        {
            yOffset = 0;
        }
	}
}
