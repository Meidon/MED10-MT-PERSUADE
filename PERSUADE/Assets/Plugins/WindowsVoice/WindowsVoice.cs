using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine.UI;
public class WindowsVoice : MonoBehaviour {
  [DllImport("WindowsVoice")]
  public static extern void initSpeech();
  [DllImport("WindowsVoice")]
  public static extern void destroySpeech();
  [DllImport("WindowsVoice")]
  public static extern void addToSpeechQueue(string s, float Voice);
  [DllImport("WindowsVoice")]
  public static extern void clearSpeechQueue();
  [DllImport("WindowsVoice")]
  public static extern void statusMessage(StringBuilder str, int length);
  public static WindowsVoice theVoice = null;
    public int voiceType = 0;
	// Use this for initialization
	void OnEnable () {
    if (theVoice == null)
    {
      theVoice = this;
      Debug.Log("Initializing speech");
      initSpeech();
    }
	}
  public void speak(string msg, int VoiceType) {
      addToSpeechQueue(msg,VoiceType);
  }
  void OnDestroy()
  {
    if (theVoice == this)
    {
      Debug.Log("Destroying speech");
      destroySpeech();
      theVoice = null;
    }
  }
  public static string GetStatusMessage()
  {
    StringBuilder sb = new StringBuilder(40);
    statusMessage(sb, 40);
    return sb.ToString();
  }
}