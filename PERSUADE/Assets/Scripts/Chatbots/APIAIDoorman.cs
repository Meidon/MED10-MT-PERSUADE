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

public class APIAIDoorman : MonoBehaviour
{

    public Text answerTextField;
    public Text inputTextField;
    public InputField playerField;
    private ApiAiUnity apiAiUnity1;
    public int VoiceType;
    public bool animRespond;

    private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings
    {
        NullValueHandling = NullValueHandling.Ignore,
    };

    void Start()
    {

        const string ACCESS_TOKEN = "bc692c1fbdb14ecca7f89d4fbfc5e227";
        animRespond = false;
        var config = new AIConfiguration(ACCESS_TOKEN, SupportedLanguage.English);

        apiAiUnity1 = new ApiAiUnity();
        apiAiUnity1.Initialize(config);

    }


    void Update()
    {
        if (apiAiUnity1 != null)
        {
            apiAiUnity1.Update();
        }


    }

    public void SendText()
    {
        var text = inputTextField.text;

        Debug.Log(text);
        try
        {
            AIResponse response = apiAiUnity1.TextRequest(text);

            if (response != null)
            {
                Debug.Log("Resolved query: " + response.Result.ResolvedQuery);
                var outText = JsonConvert.SerializeObject(response, jsonSettings);
                animRespond = true;
                playerField.ActivateInputField();
                Debug.Log("Result: " + outText);

                answerTextField.text = response.Result.Fulfillment.Speech;
                StartCoroutine(Wait(1));
            }
            else
            {
                Debug.LogError("Response is null");
            }
        } catch (Exception ex)
        {
            Debug.LogException(ex);
        }


    }

    IEnumerator Wait(float delay)
    {
        GetComponent<TestVoice>().Speak(answerTextField.text, VoiceType);
        yield return new WaitForSeconds(delay);

        animRespond = false;
    }
}
