﻿using UnityEngine;
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

public class APIAIKeno : MonoBehaviour
{

    public Text answerTextField;
    public Text inputTextField;
    private ApiAiUnity apiAiUnity;
    public bool animRespond;
    public Narrator n;

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

        const string ACCESS_TOKEN = "8a2bc91e409f4c63871e60ba4033932c";
        animRespond = false;
        var config = new AIConfiguration(ACCESS_TOKEN, SupportedLanguage.English);
        n = GameObject.FindGameObjectWithTag("Narrator").GetComponent<Narrator>();
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

        Debug.Log(text);

        AIResponse response = apiAiUnity.TextRequest(text);

        if (response != null)
        {
            Debug.Log("Resolved query: " + response.Result.ResolvedQuery);
            var outText = JsonConvert.SerializeObject(response, jsonSettings);
            animRespond = true;
            Debug.Log("Result: " + outText);

            answerTextField.text = response.Result.Fulfillment.Speech;
            n.textInput = answerTextField.text;
            StartCoroutine(Wait(1));
        }
        else
        {
            Debug.LogError("Response is null");
        }

    }
    IEnumerator Wait(float delay)
    {
        yield return new WaitForSeconds(delay);

        animRespond = false;
    }
}