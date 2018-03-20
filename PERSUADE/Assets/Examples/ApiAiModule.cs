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

public class ApiAiModule : MonoBehaviour
{

    public Text answerTextField;
    public Text inputTextField;
    private ApiAiUnity apiAiUnity;
    public bool animRespond;

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
            
        const string ACCESS_TOKEN = "0841d56a1f304f318d207aa46cb569cd";
        animRespond = false;
        var config = new AIConfiguration(ACCESS_TOKEN, SupportedLanguage.English);

        apiAiUnity = new ApiAiUnity();
        apiAiUnity.Initialize(config);

        //apiAiUnity.OnError += HandleOnError;
        //apiAiUnity.OnResult += HandleOnResult;
    }

    //void HandleOnResult(object sender, AIResponseEventArgs e)
    //{
    //    RunInMainThread(() => {
    //        var aiResponse = e.Response;
    //        if (aiResponse != null)
    //        {
    //            Debug.Log(aiResponse.Result.ResolvedQuery);
    //            var outText = JsonConvert.SerializeObject(aiResponse, jsonSettings);
    //            var speechOutput = aiResponse.Result.Fulfillment.Speech;
    //            Debug.Log(outText);
                
    //            answerTextField.text = speechOutput;
                
    //        } else
    //        {
    //            Debug.LogError("Response is null");
    //        }
    //    });
    //}
    
    //void HandleOnError(object sender, AIErrorEventArgs e)
    //{
    //    RunInMainThread(() => {
    //        Debug.LogException(e.Exception);
    //        Debug.Log(e.ToString());
    //        answerTextField.text = e.Exception.Message;
    //    });
    //}

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
            StartCoroutine(wait(1));
        } else
        {
            Debug.LogError("Response is null");
        }

    }
    IEnumerator wait(float delay)
    {
        yield return new WaitForSeconds(delay);

        animRespond = false;
    }
}
