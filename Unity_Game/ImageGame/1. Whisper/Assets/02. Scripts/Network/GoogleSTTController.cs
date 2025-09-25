using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSTTController : MonoBehaviour
{
    public string speechToTextURL = "https://speech.googleapis.com/v1/speech:recongnize?key=AIzaSyBbEGLFWJLe8f9FfE8cX4sYg573bOtKtQA";
    public SpeechRecognitionController speechRecognitionController;

    public IEnumerator SendAudioClip(byte[] audioData)
    {
        UnityWebRequest request = new UnityWebRequest(speechToTextURL, "POST");
        request.uploadHandler = new UploadHandlerRaw(audioData);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/octet-stream");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error : " + request.error);
            speechRecognitionController.HandleSpeechError(request.error);
        }
        else
        {
            string responseText = request.downloadHandler.text;
            speechRecognitionController.OnSpeechResultReceived(responseText);
        }
    }
}
