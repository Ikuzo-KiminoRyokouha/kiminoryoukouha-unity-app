using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Debug = UnityEngine.Debug;
using GPS;
public class TestClass : GpsProvider
{
    public TMP_Text latitudeText;
    public TMP_Text longitudeText;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UseGps());
    }

    // Update is called once per frame
    void Update()
    {
        latitudeText.text = latitude.ToString();
        longitudeText.text = longitude.ToString();
        Debug.Log(GPSEncoder.GPSToUCS(latitude, longitude));
        Debug.Log(GPSEncoder.GPSToUCS(latitude + 0.001f, longitude + 0.001f));
    }
}
