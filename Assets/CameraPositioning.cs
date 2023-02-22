using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GpsProvider;
using Debug = UnityEngine.Debug;


public class CameraPositioning : GpsProvider
{
    public IEnumerator gpsCoRoutine;
    public Camera cam;
 
    // Start is called before the first frame update
    void Start()
    { 
        gpsCoRoutine = UseGps();
        
        StartCoroutine(gpsCoRoutine);
        cam = GetComponent<Camera>();

        cam.transform.position = GeoToWorldPosition(latitude,longitude,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
