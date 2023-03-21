using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using Debug = UnityEngine.Debug;
using GPS;
using Network;

public class GoogleMaps : GpsProvider
{
    private readonly string appKey = "AIzaSyDTJDEBVSaSE88LGTvad02BBoKSU6ejGAk";
    public RawImage mapImage;
    public Axios axios;

    // API 호출 함수
    IEnumerator CallApi()
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture("https://maps.googleapis.com/maps/api/staticmap?center=Berkeley,CA&zoom=14&size=400x400&key=AIzaSyDTJDEBVSaSE88LGTvad02BBoKSU6ejGAk");
        yield return request.SendWebRequest();

        // 요청이 성공하면, 응답으로 받은 데이터를 Texture2D로 변환하여 RawImage에 할당합니다.
        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            mapImage.texture = texture;
        }
        else
        {
            Debug.LogError("Failed to get map image: " + request.error);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CallApi());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
