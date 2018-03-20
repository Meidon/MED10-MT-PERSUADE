using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Narrator : MonoBehaviour {

    public Text exampleText;
    public Text robotOutput;
    private string keyword1;
    private string keyword2;
    private string output;

	// Use this for initialization
	void Start () {
        keyword1 = "Name: Damarion";
        keyword2 = "Girlfriend: Juliet";
	}
	
	// Update is called once per frame
	void Update () {

        if(robotOutput.text.Contains("Damarion"))
        {
            output.Insert(0, keyword2);
            UpdateText();
        }

        if (robotOutput.text.Contains("Juliet"))
        {
                output.Insert(1,keyword2);
                UpdateText();

            
        }
    }

    private void UpdateText()
    {
        exampleText.text = output;
    }
}
