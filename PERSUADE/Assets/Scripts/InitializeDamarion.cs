using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class InitializeDamarion : MonoBehaviour
{
    private ChatbotDamarion bot;
    public InputField inputField;
    public Text robotOutput;
    public bool animRespond;

    void Start()
    {
        bot = new ChatbotDamarion();
        animRespond = false;
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
            animRespond = true;
            inputField.text = string.Empty;
            StartCoroutine(Wait(1));
        }
    }

    IEnumerator Wait (float delay)
    {
        yield return new WaitForSeconds(delay);

        animRespond = false;
    }


}
