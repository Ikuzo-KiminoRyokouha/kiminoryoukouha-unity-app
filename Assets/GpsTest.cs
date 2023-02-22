using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Android;
using static GpsProvider;
using Debug = UnityEngine.Debug;

public class GpsTest : GpsProvider
{
    public TextMeshProUGUI textMeshPro;
    public GameObject prefab;
    public IEnumerator gpsCoRoutine;
    private float objLat =35.947339f;
    private float objLon = 128.46379f;


    // Start is called before the first frame update
    void Start()
    {
        gpsCoRoutine = UseGps();
        
        StartCoroutine(gpsCoRoutine);

        textMeshPro = GetComponent<TextMeshProUGUI>();

        textMeshPro.text =latitude + " " + longitude;

        // // Continuously update location data
        // while (true)
        // {
        //     Debug.Log("Hello");
        //     yield return new WaitForSeconds(1);

        //     if (Input.location.status == LocationServiceStatus.Running)
        //     {
        //         // latitude = Input.location.lastData.latitude;
        //         // longitude = Input.location.lastData.longitude;
                                
                Vector3 position = GeoToWorldPosition(latitude, longitude, 0);

                Debug.Log("object position : " +  position);
            
                Instantiate(prefab, position, Quaternion.identity);



        //     }
        // }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}