using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariablesAndPostRequest : MonoBehaviour {
    public static string CurrentUserMessage;
    public static bool NewUserMessage = false; 
    public static string CurrentWatsonMessage;
    public static bool NewWatsonMessage = false; 
    // Update is called once per frame
    void Update()
    {
        if (NewUserMessage != false) {
            NewUserMessage = false;
            Debug.Log("Sending a Post request");
            string url = "Your Conversation API URL";
            WWWForm formDate = new WWWForm();
            formDate.AddField("text", CurrentUserMessage);
            formDate.AddField("userId", "randomstring");
            WWW www = new WWW(url, formDate);
            StartCoroutine(request(www));
        }	
	}

    IEnumerator request(WWW www) {
        yield return www;
        CurrentWatsonMessage = www.text;
        Debug.Log("this is watson message: " + CurrentWatsonMessage);
        NewWatsonMessage = true;
    }
}
