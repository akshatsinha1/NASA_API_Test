using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Rendering;
using TMPro;
using System;

public class NasaApiClient : MonoBehaviour
{
    public string apiKey = "wfvy9ZYYbORLrf7GwppHhvBU173z8vaE2q1zWDxb"; // Replace with your NASA API key
    public string apiUrl = "https://api.nasa.gov/planetary/earth/imagery?lon=100.75&lat=1.5&date=2014-02-01&api_key=";

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
        NasaData data = JsonUtility.FromJson<NasaData>(jsonResponse);
        titleText.text = data.title;
        explanationText.text = data.explanation;
        date.text = data.date;
        StartCoroutine(LoadImage(data.hdurl));
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

//class jsonss
//{//"copyright":"The Deep Sky Collective","date":"2024-12-13","explanation":"An intriguing pair of interacting galaxies, M51 is the 51st entry in Charles Messier's famous catalog. Perhaps the original spiral nebula, the large galaxy with whirlpool-like spiral structure seen nearly face-on is also cataloged as NGC 5194. Its spiral arms and dust lanes sweep in front of its smaller companion galaxy, NGC 5195. Some 31 million light-years distant, within the boundaries of the well-trained constellation Canes Venatici, M51 looks faint and fuzzy to the eye in direct telescopic views. But this remarkably deep image shows off stunning details of the galaxy pair's striking colors and fainter tidal streams. The image includes extensive narrowband data to highlight a vast reddish cloud of ionized hydrogen gas recently discovered in the M51 system and known to some as the H-alpha cliffs. Foreground dust clouds in the Milky Way and distant background galaxies are captured in the wide-field view. A continuing collaboration of astro-imagers using telescopes on planet Earth assembled over 3 weeks of exposure time to create this evolving portrait of M51.  Watch: The 2024 Geminid Meteor Shower","hdurl":"https://apod.nasa.gov/apod/image/2412/M51_HaLRGB_APOD2048.jpg","media_type":"image","service_version":"v1","title":"M51: Tidal Streams and H-alpha Cliffs","url":"https://apod.nasa.gov/apod/image/2412/M51_HaLRGB_APOD1024.jpg"}
