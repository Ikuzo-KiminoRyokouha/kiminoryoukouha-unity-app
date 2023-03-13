using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DataTypes;
using Network;
using Newtonsoft.Json;

using Debug = UnityEngine.Debug;



public class LoadPlanData : MonoBehaviour
{
    private Axios axios;
    private PlanData planData;

    void Awake()
    {
        axios = new Axios();
        StartCoroutine(axios.Get("/plan/all/1", onSuccess, onError, true));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void onSuccess(UnityWebRequest www)
    {
        string res = www.downloadHandler.text;
        // 받아온 계획데이터를 역직렬화 시킨다.
        planData = JsonConvert.DeserializeObject<PlanData>(res);
    }


    private void onError(string error)
    {
        Debug.Log("error : " + error);
    }
}