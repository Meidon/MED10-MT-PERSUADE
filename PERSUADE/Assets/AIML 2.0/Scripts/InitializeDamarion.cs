using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class InitializeDamarion : MonoBehaviour
{
    private ChatbotDamarion bot;
    public InputField inputField;
    public Text robotOutput;

    void Start()
    {
        bot = new ChatbotDamarion();
        bot.LoadBrain();
    }


    void OnDisable()
    {
        bot.SaveBrain();
    }


    public void SendQuestionToRobot()
    {
        if (string.IsNullOrEmpty(inputField.text) == false)
        {
            var answer = bot.getOutput(inputField.text);
            robotOutput.text = answer;
            inputField.text = string.Empty;
        }
    }


}
