using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using Debug = UnityEngine.Debug;


public  class GpsProvider : MonoBehaviour{
    protected float latitude =  35.94734f;
    protected float longitude =  128.4638f;

    protected IEnumerator UseGps(){
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location services are not enabled");
            yield break;
        }

        // Start location service
        Input.location.Start();
        int maxWait = 20;
        // Wait until location service is initialized
        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            maxWait--;
            yield return new WaitForSeconds(1);
            if(maxWait < 0){
                break;
            }
        }

        // Check if location service has failed
        if (Input.location.status == LocationServiceStatus.Failed || maxWait <  0)
        {
            Debug.Log("Location services failed");
            yield break;
        }

        // Location service is initialized and ready
        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
    }

     protected Vector3 GeoToWorldPosition(float lat, float lon, float height)
    {
        Vector3 origin = new Vector3(0, 0, 0); // 원점 위치
        float scale = 100000f; // 스케일

        // 경도와 위도를 유니티 좌표계로 변환
        float x = (lon * Mathf.PI / 180) * scale * Mathf.Cos(0);
        float z = (lat * Mathf.PI / 180) * scale * Mathf.Cos(0);
        float y = height;

        Vector3 position = new Vector3(x, y, z) - origin;

        return position;
    }
}
