using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeechRecognitionController : MonoBehaviour
{

    public GoogleSTTController googleSTTController;

    public UnityEvent<string> OnSpeechResult = new UnityEvent<string>();
    public UnityEvent<string> OnSpeechError = new UnityEvent<string>();

    public bool isRecording = false;
    private AudioClip audioClip;

    public void StartRecording()
    {
        if ( !isRecording )
        {
            isRecording = true;
            StartCoroutine(CaptureAudioAndSend());
        }
    }
    
    public void StopRecording()
    {
        if ( isRecording)
        {
            isRecording = false;
            Microphone.End(null);
            if ( audioClip != null )
            {
                float[] samples = new float[audioClip.samples];
                audioClip.GetData(samples, 0);
                byte[] audioData = ConvertSamplesToByteArray(samples);

                StartCoroutine(googleSTTController.SendAudioClip(audioData));
            }
        }

    }

    private IEnumerator CaptureAudioAndSend()
    {
        int minFreq, maxFreq;
        Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);

        AudioClip audioClip = Microphone.Start(null, false, 2, maxFreq);
        yield return new WaitForSeconds(2);
        Microphone.End(null);
    }

    private byte[] ConvertSamplesToByteArray(float[] samples)
    {
        byte[] audioData = new byte[samples.Length * 2];
        for( int i = 0; i < samples.Length; i++ )
        {
            short sample = (short)(samples[i] * short.MaxValue);
            byte[] sampleBytes = System.BitConverter.GetBytes(sample);
            audioData[i * 2] = sampleBytes[0];
            audioData[i * 2 + 1] = sampleBytes[1];
        }
        return audioData;
    }

    public void OnSpeechResultReceived(string recognizedText)
    {
        OnSpeechResult.Invoke(recognizedText);
    }

    public void HandleSpeechError(string error)
    {
        Debug.LogError("Speech recognition error : " + error);
        OnSpeechError.Invoke(error);    
    }

}
