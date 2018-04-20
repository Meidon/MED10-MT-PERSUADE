using UnityEngine;

public class TestVoice : MonoBehaviour
{
	public int voiceOk;
	public int numVoice;
	public string voiceName;
	public VoiceManager vm;
	
	void Start ()
	{
		vm = FindObjectOfType<VoiceManager>();
		numVoice  = vm.VoiceNumber;
		voiceName = vm.VoiceNames[0];

	}

    public void Speak(string text, int voiceID)
    {
        voiceName = vm.VoiceNames[voiceID];
        vm.Say("<voice required='Name="+voiceName+"'>"+text);

    }
	
}
