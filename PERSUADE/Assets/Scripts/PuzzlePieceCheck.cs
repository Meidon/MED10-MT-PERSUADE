using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePieceCheck : MonoBehaviour {

    public string keyword;
    private Narrator n;

	void Start () {

        n = FindObjectOfType<Narrator>();
        if(keyword == "")
        {
            keyword = "No Keyword Found";
        }
        this.GetComponent<Image>().enabled = false;
	}
	
	void Update () {

        for (int i = 0; i < n.DataLog.Count; i++)
        {
            if (n.DataLog[i].Contains(keyword) && this.GetComponent<Image>().enabled == false)
            {
                this.GetComponent<Image>().enabled = true;
            }
        }

	}
}
