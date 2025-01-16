using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Rendering;
using TMPro;
using System;

public class EarthImageryClient : MonoBehaviour
{
    public string apiKey = "wfvy9ZYYbORLrf7GwppHhvBU173z8vaE2q1zWDxb"; // Replace with your NASA API key
     string apiUrl = "https://api.nasa.gov/planetary/earth/imagery?lon=100.75&lat=1.5&date=2014-02-01&api_key=";
    public Image image;
    public TMP_Text longi, latti;

    public string longitude, latitude, dimension, date;

    void Start()
    {
        apiUrl = $"https://api.nasa.gov/planetary/earth/assets?lon={longitude}&lat={latitude}&dim={dimension}&date={date}&api_key=";
        StartCoroutine(GetEarthImagery());

        Debug.Log(apiUrl + apiKey);

    }
    public void regenerate()
    {
        apiUrl = $"https://api.nasa.gov/planetary/earth/assets?lon={longitude}&lat={latitude}&dim={dimension}&date={date}&api_key=";
        StartCoroutine(GetEarthImagery());

    }
    IEnumerator GetEarthImagery()
    {
        string fullUrl = apiUrl + apiKey;
        UnityWebRequest request = UnityWebRequest.Get(fullUrl);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            ProcessData(jsonResponse);


        }
    }

    void ProcessData(string jsonResponse)
    {
        Debug.Log(jsonResponse);
        // You can parse the JSON response and use the data as needed.
        EarthImageryData data = JsonUtility.FromJson<EarthImageryData>(jsonResponse);
       
        StartCoroutine(LoadImage(data.url));
    }

    IEnumerator LoadImage(string imageUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }

    }

}
