using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.Networking;


namespace Network
{

    public class Axios
    {

        public delegate void OnSuccess(UnityWebRequest www);
        public delegate void OnError(string error);
        public string BASE_URL = URL.BASE;
        public Axios()
        {
        }
        public Axios(string baseURL)
        {
            BASE_URL = baseURL;
        }
#nullable enable
        public IEnumerator Get(string path, OnSuccess onSuccess, OnError onError, bool? needAuth = false, Dictionary<string, string>? headers = null)
        {
            Debug.Log(BASE_URL + path);
            UnityWebRequest www = UnityWebRequest.Get(BASE_URL + path);
            if (needAuth != null)
            {
                if ((bool)needAuth)
                {
                    Debug.Log("Authorization" + "Bearer " + TokenManager.GetToken("accessToken"));
                    www.SetRequestHeader("Authorization", "Bearer " + TokenManager.GetToken("accessToken"));
                }
            }


            if (headers != null)
            {
                foreach (KeyValuePair<string, string> item in headers)
                {
                    www.SetRequestHeader(item.Key, item.Value);
                }
            }

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                if (www.responseCode == 401)
                {
                    CoroutineHandler.StartStaticCoroutine(RequestRefreshToken(onSuccess, onError));
                    yield break;
                }
                onError(www.error);
                yield break;
            }
            onSuccess(www);
            www.Dispose();
        }

        public IEnumerator Post(string path, WWWForm body, OnSuccess onSuccess, OnError onError, bool? needAuth = false, Dictionary<string, string>? headers = null)
        {

            UnityWebRequest www = UnityWebRequest.Post(BASE_URL + path, body);
            if (needAuth != null)
            {
                if ((bool)needAuth)
                {
                    www.SetRequestHeader("Authorization", "Bearer " + TokenManager.GetToken("accessToken"));
                }
            }


            if (headers != null)
            {
                foreach (KeyValuePair<string, string> item in headers)
                {
                    www.SetRequestHeader(item.Key, item.Value);
                }
            }

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                if (www.responseCode == 401)
                {
                    CoroutineHandler.StartStaticCoroutine(RequestRefreshToken(onSuccess, onError));
                    yield break;
                }
                onError(www.error);
                yield break;
            }
            onSuccess(www);
            www.Dispose();
        }

        private IEnumerator RequestRefreshToken(OnSuccess onSuccess, OnError onError)
        {
            UnityWebRequest www = UnityWebRequest.Get(BASE_URL + "/auth/token/refresh");

            www.SetRequestHeader("Cookie", TokenManager.GetToken("refreshToken"));

            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                onError(www.error);
                yield break;
            }

            TokenManager.SetToken("accessToken", TokenInfo.CreateFromJSON(www.downloadHandler.text).accessToken);
            onSuccess(www);
        }

    }
}