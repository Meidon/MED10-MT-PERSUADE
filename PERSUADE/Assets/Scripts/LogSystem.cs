using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class LogSystem : MonoBehaviour {

    public Narrator n;
    public int num;
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
    private bool end_running = false;
    public TMPro.TextMeshProUGUI Completion;
    private float closeCountdown = 5f;

    void Start () {

        n = FindObjectOfType<Narrator>();
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

        if(minute == 30)
        {
            n.GOC.enabled = true;
            Completion.SetText("Completion: <#0fffff>"+num/144f*100+ "%</color>\nApp will terminate in: <#0fffff>" + (int)closeCountdown+"</color> seconds.");
        }

        if(n.GOC.enabled == true)
        {
            closeCountdown -= 1 * Time.deltaTime;
        }

        if(minute == 30 && timePlayed >= 5f)
        {
            end = true;
        }

        if (end)
        {
            if(!end_running)
                StartCoroutine(LogEnd(0.5f));
            end = false;
        }

        for (int i = 0; i < n.DataLog.Count; i++)
        {
            if(!dataLog.Contains(n.DataLog[i].ToString()))
                dataLog += "\r\n" + n.DataLog[i].ToString();
        }
    }
    public IEnumerator Log(string txt)
    {
        appendText = "\nPlayerText: " + txt;
        File.AppendAllText(savePath, appendText);
        hasLogged = true;
        yield return new WaitForSeconds(0.5f);

    }
    IEnumerator LogEnd(float time)
    {
        end_running = true;
        yield return new WaitForSeconds(time);
        appendText = "\r\n" + (int)hour + "," + (int)minute + "," + (int)timePlayed + "\r\n Progress: "+num/144f*100+"%";
        File.AppendAllText(savePath, appendText);
        Application.Quit();
    }
}
