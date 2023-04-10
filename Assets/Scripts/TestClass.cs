using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using NRKernal;
using UnityEngine;
using TMPro;
using Debug = UnityEngine.Debug;
using GPS;

public class TestClass : NRTrackableBehaviour
{
    private float latitude;
    private float longitude;
    private float accuracy;



    private GameObject GpsManager;

    public TMP_Text latitudeText;
    public TMP_Text longitudeText;
    public TMP_Text accuracyText;

    [SerializeField] private Transform _cameraTransform;

    // private GameObject _planeDetector;


    // Start is called before the first frame update
    void Start()
    {
        GpsManager = GameObject.Find("GpsManager");
    }

    // Update is called once per frame
    void Update()
    {
        latitude = GpsManager.GetComponent<GpsProvider>().latitude;
        longitude = GpsManager.GetComponent<GpsProvider>().longitude;
        accuracy = GpsManager.GetComponent<GpsProvider>().accuracy;

        latitudeText.text = latitude.ToString();
        longitudeText.text = longitude.ToString();
        // accuracyText.text = 
        // latitudeText.text = latitude.ToString();
        // longitudeText.text = longitude.ToString();
        // Debug.Log(GPSEncoder.GPSToUCS(latitude, longitude));
        // Debug.Log(GPSEncoder.GPSToUCS(latitude + 0.001f, longitude + 0.001f));
    }
}
