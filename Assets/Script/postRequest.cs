using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class postRequest : MonoBehaviour {
    public Text ConversationInput;
    private string prevString;
    public Text ConversationOutput;
	// Use this for initialization
	void Start () {
        //WWWForm formDate = new WWWForm();
        //formDate.AddField("text", "hello");
        //formDate.AddField("userId", "asdfkljkzcxvuixcz");

        //WWW www = new WWW(url, formDate);
        //StartCoroutine(request(www));

    }

    private void Update()
    {
        if (ConversationInput.text != null) {
            if (prevString != ConversationInput.text) {
                prevString = ConversationInput.text;
                string url = "Link to your Watson Conversation Hosting";
                WWWForm formDate = new WWWForm();
                formDate.AddField("text", ConversationInput.text);
                formDate.AddField("userId", "randomString");
                WWW www = new WWW(url, formDate);
                StartCoroutine(request(www));
            }
        }
    }

    IEnumerator request(WWW www) {
        yield return www;
        string message = www.text;
        ConversationOutput.text = message; 
        Debug.Log(message);
    }
}
