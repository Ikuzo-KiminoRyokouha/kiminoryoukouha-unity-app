using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GpsProvider;
using static LatLng;
using Debug = UnityEngine.Debug;

public class GpsTest : GpsProvider
{
    public GameObject prefab;
    public IEnumerator gpsCoRoutine;
    private float objLat =35.947339f;
    private float objLon = 128.46379f;

    // Start is called before the first frame update
    void Start()
        {
            gpsCoRoutine = UseGps();
            
            StartCoroutine(gpsCoRoutine);

            LatLng origin = new LatLng(latitude, longitude);
            LatLng target = new LatLng(objLat, objLon);

            AddObjUseGeo(origin,target,prefab);
        }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}


    
