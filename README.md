# Prism-Leap-Watson
![](Mira%20Prism%20Leap%20Motion%20Watson.jpg)
![](ezgif.com-video-to-gif.gif)

## Project Overview 
* Using [Mira Prism](https://www.mirareality.com/) and [Leap Motion](https://www.leapmotion.com/), this allow the user to hand a controller free experience
* Using [Watson Services](https://www.ibm.com/watson/products-services/) : [Speech to Text](https://www.ibm.com/watson/services/speech-to-text/), [Text to Speech](https://www.ibm.com/watson/services/text-to-speech/), 
and [Conversation](https://www.ibm.com/watson/services/conversation/), user can create shapes by talking to Watson

## Setup 
#### Software/SDK
**Note this is for PC setup** 
* [Unity](https://store.unity.com/?_ga=2.174474786.1882622745.1511205620-1336275404.1503067450) - 2017.03f
* [Mira](https://www.mirareality.com/download)
* [Watson](https://github.com/watson-developer-cloud/unity-sdk/releases/tag/2.0.0)
* [Leap Motion](https://developer.leapmotion.com/get-started)
* [Leap Motion Core Assets](https://developer.leapmotion.com/unity/#116)
* [Leap Motion Interaction Engine](https://developer.leapmotion.com/unity/#116)
* Make sure you have download Unity Remote 5 on your iPhone and connect your phone to your desktop

#### Connect Mira with Leap Motion
1. Create a new unity project, import mira unity sdk and start with the 101_HelloWorld_ Scene 
2. Go to Edit -> Project Settings -> Editor Settings, Select your iPhone as a Device and Normal for Resolution, for game view have it 16:9
3. Hit Play and make sure you can look around 
4. Import the Leap Motion Core Assets into unity, go to LeapMotion -> Core -> Examples -> and drag the Leap Hands Demo (VR) into the scene 
![](Mira%20and%20Leap%20Motion%20in%20Scene.png)
5. Drag LMHeadMountedRig into MiraARCamera as a child then remove the Leap Hands Demo (VR) Scene 
![](Move%20Entity.png)
6. Go to CenterEyeAnchor component that under LMHeadMountedRig and unchecked marked the camera component 
![](CenterEyeAnchor.png)
7. Attached your Leap Motion ontop of your Mira and press play 
8. You should now be able to see your hands models in the AR spaces. 

##### Tips 
- Change the color of the light source to white to see the hands more clear 
![](Hands%20Offset.png)
- If you go to LeapSpace under CenterEyeAnchor and checked Allow Manual Device Offset, you can manual change the Offset of the hands 

#### Setting Watson API 
1. Go to https://github.com/watson-developer-cloud/unity-sdk and follow the Before you begin section, make sure you have SST, TTS and Conversation API keys 
2. Import the Watson SDK package into Unity 
3. In MiraARCamera Entity add Example Streaming(Script) and Example TTS(Script). 
4. Create a new Script ``GlobalVaribalesAndPostRequest.cs`` and have it a component under MiraARCamera, we will be using this to manage all of the variables
5. Open our new script and replace the code with the follow code and then save your changes
```
// Create all the global variables that we will be using 
    public static string CurrentUserMessage;
    public static bool NewUserMessage = false; 
    public static string CurrentWatsonMessage;
    public static bool NewWatsonMessage = false;
    
    void Update()
    {
        //When there is a new message we want to send a Post Request to conversation
        if (NewUserMessage != false) {
            NewUserMessage = false;
            Debug.Log("Sending a Post request");
            string url = "URL Link to Conversation";
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
```
6. Next Open ExampleStream.cs and put in your credentials
7. Then on after line 173 ``string text = alt.transcript;``, insert the following code and save 
```
if (res.final) {
    //Save the user text 
    GlobalVariablesAndPostRequest.CurrentUserMessage = text;
    //Setting the flag = true
    GlobalVariablesAndPostRequest.NewUserMessage = true;
    Log.Debug("This is user message", text);
}
```
8. Open ExampleTextToSpeech and put in your credentials
9. Comment line 66 ``//Runnable.Run(Examples());`` and create private void Update()
10. In the Update function add the following code and save 
```
    private void Update()
    {
        if (GlobalVariablesAndPostRequest.NewWatsonMessage) {
            GlobalVariablesAndPostRequest.NewWatsonMessage = false;
            Log.Debug("ExampleTextToSpeech", "Attempting synthesize.");
            _textToSpeech.Voice = VoiceType.en_US_Allison;
            _textToSpeech.ToSpeech(GlobalVariablesAndPostRequest.CurrentWatsonMessage, HandleToSpeechCallback, true);
        }
    }

```
#### Notes 
- In this case I'm sending a post a request to conversation instead of using Watson SDK unity Conversation example. 
- You can any site to host your conversation enviroment, i.e. herkou, for this case I used Node-Red. You can find the flow in this repo


#### Interaction Engine 
1. Import Leap Motion Interaction Engine 1.10 into the project 
2. Then go to Assets -> LeapMotion -> Modules -> InteractionEngine -> Prefabs and drag the Interaction Manager.prefab and put it as a child of LMHeadMountedRig
3. In the HandModels entity, remove RigidRoundHand L and R 
4. Go to CenterEyeAnchor -> LeapSpace -> LeapHandController Entity and on the Hand Pool Script set the Left/Right Model as CapsuleHand L/R 
5. Now everything is setup, any object you want to interact with must have the Rigidbody and Interaction Behaviour Script
