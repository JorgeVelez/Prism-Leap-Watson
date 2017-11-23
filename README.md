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
4. Open the Example Streaming Script and add in your credentials: username/password/url 
5. Create a ``public Text CurrentUserMessage;`` on line 32 
6. In line 177 add 
```
//When the results its final, we want to save it to CurrentUserMessage
if(res.final) {
  CurrentUserMessage.text = text;
  Log.Debug("This is user message", text);
}
```
7. Save and then open Example TextToSpeech and add in your credentials: username/password/url 
8. On line 30 add ``public Text WatsonResponse;`` and on 31 ``private string prevString;``
9. On line 78 add 
```
private void Update() 
{
  if(WatsonResponse.text != null) {
    if(prevString != WatsonResponse.text) {
      //When there is a new text that comes in we want 
      prevString = WatsonResponse.text; 
      
    }
  }
}
```
