using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Network;
using Newtonsoft.Json;
using DataTypes;

using Debug = UnityEngine.Debug;

public class LoadNavigateData : MonoBehaviour
{
    public WalkRoute routes;

    public string startX;
    public string startY;
    public string endX;
    public string endY;
    public string startName;
    public string endName;
    public string searchOption;
    public bool IsTest = false;

    // Start is called before the first frame update
    void Start()
    {
        Axios axios = new Axios("https://apis.openapi.sk.com/tmap");
        Dictionary<string, string> headers = new Dictionary<string, string>();
        WWWForm form = new WWWForm();
        form.AddField("startX", "128.4611931");
        form.AddField("startY", "35.945906");
        form.AddField("endX", "128.4639955");
        form.AddField("endY", "35.9473421");
        form.AddField("startName", "hi");
        form.AddField("endName", "fuck");
        form.AddField("searchOption", 10);
        // form.AddField("sort", "custom");
        Debug.Log("로그 보내기");
        headers["appKey"] = "l7xxbefea111d09640dab1bf5fec3a669c50";
        StartCoroutine(axios.Post("/routes/pedestrian?version=1", form, onSuccess, onError, false, headers));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void onSuccess(UnityWebRequest www)
    {
        string res = www.downloadHandler.text;
        routes = JsonConvert.DeserializeObject<WalkRoute>(res, Converter.Settings);
    }

    private void onError(string error)
    {
        Debug.Log("error : " + error);
    }
}
