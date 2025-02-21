using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Rendering;
using TMPro;
using System;

public class NasaApiClient : MonoBehaviour
{
    public string apiKey = "DEMO_KEY"; // Replace with your NASA API key
    public string apiUrl = "https://api.nasa.gov/planetary/earth/imagery?lon=100.75&lat=1.5&date=2014-02-01&api_key=";

    //Unity UI objects
    public TMP_Text titleText;
    public TMP_Text date;
    public TMP_Text explanationText;
    public Image image;

    void Start()
    {
        StartCoroutine(GetNasaData());
    }

    IEnumerator GetNasaData()
    {
        //compiles the Desired API URL with your API Key
        string fullUrl = apiUrl + apiKey;

        //This is where the API is called
        UnityWebRequest request = UnityWebRequest.Get(fullUrl);

        yield return request.SendWebRequest();

        //Checks for the success of API call

        //If the API request fails
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        //If the API request is successful
        else
        {
            //The API response is saved inside a string value using the download handler
            string jsonResponse = request.downloadHandler.text;
            //The string is sent through to the ProcessData function, where the API reponse will be processed into Unity readable data
            ProcessData(jsonResponse);
        }
    }

    void ProcessData(string jsonResponse)
    {
        // The JSON is parsed and the required values are identified through the EarthImageryData class
        NasaData data = JsonUtility.FromJson<NasaData>(jsonResponse);

        //The individual pieces of data are now applied to the different Unity UI objects
        titleText.text = data.title;
        explanationText.text = data.explanation;
        date.text = "Date: " +data.date;
        explanationText.alignment = TextAlignmentOptions.TopFlush;

        //This function is called to process the image and apply it to a Unity Image UI Object
        StartCoroutine(LoadImage(data.hdurl));
    }

    IEnumerator LoadImage(string imageUrl)
    {
        //A web request is made to get a texcture using the image URL
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        //Checking if the image request is successfull

        //On unsuccessfull request
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        //On successfull request
        else
        {
            //A private Texture2D variable gets data from the successfull request, and the requested image is assigned to it after download
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            //A new texture is created and applied on the Unity Image UI object using the Texture2D from before
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
        
    }
}

[Serializable]
public class NasaData
{
    public string title;
    public string hdurl;
    public string explanation;
    public string date;
}