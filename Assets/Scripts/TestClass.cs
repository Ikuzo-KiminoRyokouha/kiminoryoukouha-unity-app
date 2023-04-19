using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using NRKernal;
using UnityEngine;
using TMPro;
using Debug = UnityEngine.Debug;
using Color = UnityEngine.Color;
using GPS;
using static LatLng;

public class TestClass : NRTrackableBehaviour
{
    private double latitude;
    private double longitude;
    private float accuracy;
    private float earthRadius = 6378137f;
    private float degreeToRadian = Mathf.PI / 180f;

    private Camera cam;

    private GameObject alertObject;

    private int minAccuracy = 1000;

    // 해야될것 
    // 1. 선 그리기
    // 2. 선 방향 정의
    // 3. 선의 시작점과 끝점에 오브젝트 배치
    // 4. 해당 오브젝트에 방향 표시
    // 5. 시야에 선이 보이지 않거나 다음 오브젝트를 바라보지 않는 다면 화살표로 방향제어

    private GameObject GpsManager;

    public TMP_Text latitudeText;
    public TMP_Text longitudeText;
    public TMP_Text accuracyText;

    public GameObject prefab;

    public List<GameObject> lineList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        getCamera();
        renderLine();
        GpsManager = GameObject.Find("GpsManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (cam == null)
        {
            getCamera();
        }
        latitude = GpsManager.GetComponent<GpsProvider>().latitude;
        longitude = GpsManager.GetComponent<GpsProvider>().longitude;
        accuracy = GpsManager.GetComponent<GpsProvider>().accuracy;

        // LatLng origin = new LatL+ng(128.463775634766d, 35.9473876953125d);
        // LatLng target = new LatLng(128.463806152344d, 35.9473876953125d);
        // Vector3 realCoord = GeoToWorldPosition(origin, target);


        if (minAccuracy > accuracy)
        {
            destoryLine();
            renderLine();
        }




        latitudeText.text = latitude.ToString();
        longitudeText.text = longitude.ToString();
        accuracyText.text = accuracy.ToString();
        Vector2 originLatLng = new Vector2(128.463775634766f, 35.9473876953125f);
        Vector2 objLatLng = new Vector2(128.463806152344f, 35.9473876953125f);
        Vector3 originCoord = GPSEncoder.GPSToUCS(originLatLng);
        Vector3 objCoord = GPSEncoder.GPSToUCS(objLatLng);
        // Vector3 originCoord = GPSEncoder.GPSToUCS(128.4638f, 35.94738f);
        // Vector3 objCoord = GPSEncoder.GPSToUCS(128.4638f, 35.9474f);
        Vector3 realCoord = originCoord - objCoord;
        Debug.Log(realCoord);
        // latitudeText.text = latitude.ToString();
        // longitudeText.text = longitude.ToString();
        // Debug.Log(GPSEncoder.GPSToUCS(latitude, longitude));
        // Debug.Log(GPSEncoder.GPSToUCS(latitude + 0.001f, longitude + 0.001f));
    }

    void getCamera()
    {
        cam = GameObject.Find("CenterCamera").GetComponent<Camera>();
    }

    bool checkObjectInCamera(GameObject obj)
    {
        Vector3 viewPos = cam.WorldToViewportPoint(obj.transform.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            return true;
        }
        return false;
    }

    void checkLineInCamera()
    {

        if (checkObjectInCamera(lineList[0]))
        {

        }
    }


    void renderLine()
    {
        GameObject line = new GameObject();
        // Vector2 originLatLng = new Vector2(128.463806152344f, 35.9472999572754f);
        // Vector2 objLatLng = new Vector2(128.463806152344f, 35.9473876953125f);
        // Vector3 originCoord = GPSEncoder.GPSToUCS(originLatLng);
        // Vector3 objCoord = GPSEncoder.GPSToUCS(objLatLng);
        // // Vector3 originCoord = GPSEncoder.GPSToUCS(128.4638f, 35.94738f);
        // // Vector3 objCoord = GPSEncoder.GPSToUCS(128.4638f, 35.9474f);
        // Vector3 realCoord = originCoord - objCoord;
        LatLng origin = new LatLng(128.463806152344d, 35.9472999572754d);
        LatLng target = new LatLng(128.463806152344d, 35.9473876953125d);
        Vector3 realCoord = GeoToWorldPosition(origin, target);
        line.transform.position = new Vector3(0, -1, 0);
        line.AddComponent<LineRenderer>();
        LineRenderer lr = line.GetComponent<LineRenderer>();
        lr.SetWidth(0.1f, 0.1f);
        lr.SetColors(Color.black, Color.black);
        lr.SetPosition(0, new Vector3(0, 0, 0));
        lr.SetPosition(1, realCoord);
        lr.useWorldSpace = false;
        lineList.Add(line);
    }

    void destoryLine()
    {
        lineList.ForEach((gameObject) => { Destroy(gameObject); });
        lineList.Clear();
    }

    protected float GetUnityX(double latitude, double longitude)
    {
        float x = (float)(earthRadius * Mathf.Cos((float)(latitude * degreeToRadian)) * Mathf.Sin((float)(longitude * degreeToRadian)));
        return x;
    }

    protected float GetUnityZ(double latitude, double longitude)
    {
        float z = (float)(earthRadius * Mathf.Cos((float)(latitude * degreeToRadian)) * Mathf.Cos((float)(longitude * degreeToRadian)));
        return z;
    }

    protected Vector3 GeoToWorldPosition(LatLng origin, LatLng target)
    {
        Vector3 originPos = new Vector3(GetUnityX(origin.latitude, origin.longitude), 0f, GetUnityZ(origin.latitude, origin.longitude));

        float x = GetUnityX(target.latitude, target.longitude);
        float z = GetUnityZ(target.latitude, target.longitude);
        Vector3 pos = new Vector3(x - originPos.x, 0f, z - originPos.z);
        return pos;
    }


}
