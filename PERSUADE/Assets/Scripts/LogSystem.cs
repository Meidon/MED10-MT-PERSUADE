using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class LogSystem : MonoBehaviour {

    public Narrator n;
    int num;
    private string playerInput;
    string savePath;
    string appendText;
    string theTime;
    string theDate;
    public bool end;
    public float timePlayed;
    public string dataLog;
    private float minute;
    private float hour;
    public bool hasLogged;

    private APIAIDorian dorian;
    private APIAICollene collene;
    private APIAIDenna denna;
    private APIAIEryn eryn;
    private APIAIGavin gavin;
    private APIAIKeira keira;
    private APIAIKeno keno;
    private APIAIShenna shenna;
    private APIAITanya tanya;
    private APIAIVeronika veronika;



    void Start () {

        n = FindObjectOfType<Narrator>();
        dorian = FindObjectOfType<APIAIDorian>();
        collene = FindObjectOfType<APIAICollene>();
        denna = FindObjectOfType<APIAIDenna>();
        eryn = FindObjectOfType<APIAIEryn>();
        gavin = FindObjectOfType<APIAIGavin>();
        keira = FindObjectOfType<APIAIKeira>();
        keno = FindObjectOfType<APIAIKeno>();
        shenna = FindObjectOfType<APIAIShenna>();
        tanya = FindObjectOfType<APIAITanya>();
        veronika = FindObjectOfType<APIAIVeronika>();
        theTime = DateTime.Now.ToString("HH-mm-ss");
        theDate = DateTime.Now.ToString("MM-dd-yyyy");
        savePath = Path.GetFullPath("." + @"\TestData\Time_" + theTime + "_Date_" + theDate + ".txt");
        print(savePath);
        if(!File.Exists(savePath))
        {
            string firstLine = "";
            File.WriteAllText(savePath, firstLine);

        }



	}
	
	void Update () {

        timePlayed += Time.deltaTime;

        if (timePlayed >= 60)
        {
            timePlayed = 0;
            minute += 1;

        }

        if (minute >= 60)
        {
            minute = 0;
            hour += 1;
        }

        if (end)
        {
            StartCoroutine(LogEnd(0.5f));
            end = false;
        }

        for (int i = 0; i < n.DataLog.Count; i++)
        {
            if(!dataLog.Contains(n.DataLog[i].ToString()))
                dataLog += "\r\n" + n.DataLog[i].ToString();
        }

        if(denna.GetComponentInChildren<Canvas>().enabled)
        {
            playerInput = denna.playerTextInput;
        }
        else if(dorian.GetComponentInChildren<Canvas>().enabled)
        {
            playerInput = dorian.playerTextInput;
        }
        else if (shenna.GetComponentInChildren<Canvas>().enabled)
        {
            playerInput = shenna.playerTextInput;
        }
        else if (eryn.GetComponentInChildren<Canvas>().enabled)
        {
            playerInput = eryn.playerTextInput;
        }
        else if (tanya.GetComponentInChildren<Canvas>().enabled)
        {
            playerInput = tanya.playerTextInput;
        }
        else if (gavin.GetComponentInChildren<Canvas>().enabled)
        {
            playerInput = gavin.playerTextInput;
        }
        else if (keira.GetComponentInChildren<Canvas>().enabled)
        {
            playerInput = keira.playerTextInput;
        }
        else if (veronika.GetComponentInChildren<Canvas>().enabled)
        {
            playerInput = veronika.playerTextInput;
        }
        else if (keno.GetComponentInChildren<Canvas>().enabled)
        {
            playerInput = keno.playerTextInput;
        }
        else if (collene.GetComponentInChildren<Canvas>().enabled)
        {
            playerInput = collene.playerTextInput;
        }

        if(dorian.isSending && dorian.GetComponentInChildren<Canvas>().enabled)
        {
            StartCoroutine(Log(0.5f));
        }
        if (collene.IsInvoking("SendText") && collene.GetComponentInChildren<Canvas>().enabled)
        {
            StartCoroutine(Log(0.5f));
        }
        if (tanya.IsInvoking("SendText") && tanya.GetComponentInChildren<Canvas>().enabled)
        {
            StartCoroutine(Log(0.5f));
        }
        if (denna.IsInvoking("SendText") && denna.GetComponentInChildren<Canvas>().enabled)
        {
            StartCoroutine(Log(0.5f));
        }
        if (shenna.IsInvoking("SendText") && shenna.GetComponentInChildren<Canvas>().enabled)
        {
            StartCoroutine(Log(0.5f));
        }
        if (eryn.IsInvoking("SendText") && eryn.GetComponentInChildren<Canvas>().enabled)
        {
            StartCoroutine(Log(0.5f));
        }
        if (gavin.IsInvoking("SendText") && gavin.GetComponentInChildren<Canvas>().enabled)
        {
            StartCoroutine(Log(0.5f));
        }
        if (veronika.IsInvoking("SendText") && veronika.GetComponentInChildren<Canvas>().enabled)
        {
            StartCoroutine(Log(0.5f));
        }
        if (keira.IsInvoking("SendText") && keira.GetComponentInChildren<Canvas>().enabled)
        {
            StartCoroutine(Log(0.5f));
        }
        if (keno.IsInvoking("SendText") && keno.GetComponentInChildren<Canvas>().enabled)
        {
            StartCoroutine(Log(0.5f));
        }

    }
    IEnumerator Log(float time)
    {
        appendText = "\nPlayerText: " + playerInput;
        File.AppendAllText(savePath, appendText);
        hasLogged = true;
        yield return new WaitForSeconds(time);

    }
    IEnumerator LogEnd(float time)
    {
        yield return new WaitForSeconds(time);
        appendText = "\r\n" + (int)hour + "," + (int)minute + "," + (int)timePlayed + "\r\n PlayerText: "+playerInput;
        File.AppendAllText(savePath, appendText);
    }
}
