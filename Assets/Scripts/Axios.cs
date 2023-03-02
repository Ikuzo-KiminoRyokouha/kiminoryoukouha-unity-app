using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

using static TokenInfo;
using static TokenManager;
using static CustomConstraint;

public class Axios : MonoBehaviour{

    public delegate void OnSuccess(UnityWebRequest www);
    public delegate void OnError(string error);

    public IEnumerator Get(string path, OnSuccess onSuccess, OnError onError, bool needAuth = false){       
        
        UnityWebRequest www = UnityWebRequest.Get(CustomConstraint.BASE_URL + path);

        if(needAuth){
            www.SetRequestHeader("Authorization",  "Bearer " + TokenManager.GetToken("accessToken") );
        }

         yield return www.SendWebRequest();

         if(www.result != UnityWebRequest.Result.Success){
            if(www.responseCode == 401){
               StartCoroutine(RequestRefreshToken(onSuccess, onError));
               yield break;
            }
            onError(www.error);
            yield break;
         }
        onSuccess(www);
    }

    public IEnumerator Post(string path, WWWForm body, OnSuccess onSuccess, OnError onError,bool needAuth = false){       
        
        UnityWebRequest www = UnityWebRequest.Post(CustomConstraint.BASE_URL + path,body);

      if(needAuth){
            www.SetRequestHeader("Authorization",  "Bearer " + TokenManager.GetToken("accessToken") );
        }

         yield return www.SendWebRequest();

         if(www.result != UnityWebRequest.Result.Success){
            if(www.responseCode == 401){
               StartCoroutine(RequestRefreshToken(onSuccess, onError));
               yield break;
            }
            onError(www.error);
            yield break;
         }
        onSuccess(www);
    }

    private IEnumerator RequestRefreshToken(OnSuccess onSuccess, OnError onError){
        UnityWebRequest www = UnityWebRequest.Get(CustomConstraint.BASE_URL + "/auth/token/refresh");

        www.SetRequestHeader("Cookie",  TokenManager.GetToken("refreshToken") );

        yield return www.SendWebRequest();
        if(www.result != UnityWebRequest.Result.Success){
            onError(www.error);
            yield break;
         }
         
        TokenManager.SetToken("accessToken",TokenInfo.CreateFromJSON(www.downloadHandler.text).accessToken);
        onSuccess(www);
    }

}