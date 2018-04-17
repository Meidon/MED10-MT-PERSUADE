﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Narrator : MonoBehaviour {

    public string textInput;
    public List<string> DataLog = new List<string>();
    public List<string> Keywords = new List<string>();
    public Canvas GOC;

    void Start()
    {
        GOC.enabled = false;
    }

    void Update () {
        for(int i = 0; i < Keywords.Count; i++)
        {
            if(textInput.Contains(Keywords[i].ToString()))
            {
                UpdateLog();
            }
        }
    }

    private void UpdateLog()
    {
        if(!DataLog.Contains(textInput))
            DataLog.Add(textInput);
    }
}
