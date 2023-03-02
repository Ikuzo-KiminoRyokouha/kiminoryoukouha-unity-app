using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using Debug = UnityEngine.Debug;


public class TMap : GpsProvider
{

    private readonly string appKey = "l7xxbefea111d09640dab1bf5fec3a669c50";
    private string apiUrl = "https://apis.openapi.sk.com/tmap/staticMap?version=1&appKey={0}&coordType=WGS84GEO&width=512&height=512&zoom=14&format=PNG&longitude={1}&latitude={2}&markers={1}%2C{2}";
    public RawImage mapImage;

// API 호출 함수
    IEnumerator CallApi()
    {
        // API 호출 URL 생성
        string url = string.Format(apiUrl, appKey, longitude,latitude);
        Debug.Log(url);
        // API 호출
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

        yield return request.SendWebRequest();

        // API 호출 결과 처리
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Tmap API Error: " + request.error);
        }
        else
        {
            // RawImage에 지도 이미지 적용
            mapImage.texture = DownloadHandlerTexture.GetContent(request);
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
