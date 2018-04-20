using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Reflection;
using ApiAiSDK;
using ApiAiSDK.Model;
using ApiAiSDK.Unity;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;

public class APIAITanya : MonoBehaviour
{

    public Text answerTextField;
    public Text inputTextField;
    public string playerTextInput;
    public InputField playerField;
    private ApiAiUnity apiAiUnity;
    public bool animRespond;
    public int VoiceType;
    //public bool isSending;
    public Narrator n;
    public LogSystem lS;
    private string botName = " | Tanya: ";

    private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings
    {
        NullValueHandling = NullValueHandling.Ignore,
    };

    private readonly Queue<Action> ExecuteOnMainThread = new Queue<Action>();


    void Start()
    {
        ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) =>
        {
            return true;
        };

        const string ACCESS_TOKEN = "149b532f79db46e0bbd3be0932367eea";
        animRespond = false;
        //isSending = false;
        var config = new AIConfiguration(ACCESS_TOKEN, SupportedLanguage.English);
        n = GameObject.FindGameObjectWithTag("Narrator").GetComponent<Narrator>();
        lS = FindObjectOfType<LogSystem>();
        apiAiUnity = new ApiAiUnity();
        apiAiUnity.Initialize(config);

    }



    void Update()
    {
        if (apiAiUnity != null)
        {
            apiAiUnity.Update();
        }

        while (ExecuteOnMainThread.Count > 0)
        {
            ExecuteOnMainThread.Dequeue().Invoke();
        }
    }

    private void RunInMainThread(Action action)
    {
        ExecuteOnMainThread.Enqueue(action);
    }

    public void PluginInit()
    {

    }

    public void SendText()
    {
        var text = inputTextField.text;
        playerTextInput = text;
        Debug.Log(text);
        
        AIResponse response = apiAiUnity.TextRequest(text);

        if (response != null)
        {
            Debug.Log("Resolved query: " + response.Result.ResolvedQuery);
            var outText = JsonConvert.SerializeObject(response, jsonSettings);
            animRespond = true;
            playerField.ActivateInputField();
            //isSending = true;
            Debug.Log("Result: " + outText);

            answerTextField.text = response.Result.Fulfillment.Speech;
            n.textInput = "Tanya: " + answerTextField.text;
            StartCoroutine(Wait(1));
        }
        else
        {
            Debug.LogError("Response is null");
        }
        StartCoroutine(lS.Log(text + botName + answerTextField.text));
    }
    IEnumerator Wait(float delay)
    {
        GetComponent<TestVoice>().Speak(answerTextField.text, VoiceType);
        yield return new WaitForSeconds(delay);

        animRespond = false;
    }
}