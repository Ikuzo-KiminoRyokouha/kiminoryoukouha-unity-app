using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using NRKernal;

namespace Network
{
    public class BootStrap : MonoBehaviour
    {
        private Axios axios;

        // Start is called before the first frame update
        void Start()
        {
            WWWForm form = new WWWForm();
            form.AddField("email", "right7066@naver.com");
            form.AddField("password", "1234");
            axios = new Axios();
            StartCoroutine(axios.Post("/auth/signin", form, onSuccess, onError));
            // StartCoroutine(axios.Get("/users", onSuccess, onError, true));
        }

        // Update is called once per frame
        void Update()
        {

        }

        void onSuccess(UnityWebRequest www)
        {
            string refreshToken = www.GetResponseHeader("Set-Cookie");
            refreshToken = refreshToken.Split("; ")[0] + ";";
            TokenManager.SetToken("refreshToken", refreshToken);
            TokenManager.SetToken("accessToken", TokenInfo.CreateFromJSON(www.downloadHandler.text).accessToken);
            if (NRInput.SetInputSource(InputSourceEnum.Hands))
            {
                SceneManager.LoadScene("MainScene");
            }

        }

        void onError(string error)
        {

        }
    }


}

