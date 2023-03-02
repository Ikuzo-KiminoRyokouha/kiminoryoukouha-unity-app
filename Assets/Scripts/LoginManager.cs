using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;
using Debug = UnityEngine.Debug;

using static TokenInfo;
using static TokenManager;
using static CustomConstraint;
using static Axios;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField[] inputFields;
    public TMP_Text[] resultFields;
    public Button button;

    private Axios axios;
    // Start is called before the first frame update
    void Start()
    {
        axios = new Axios();
        button.onClick.AddListener(onSubmit);        
    }

    void onSubmit(){
        WWWForm form = new WWWForm();
        for (int i = 0; i < inputFields.Length; i++)
        {
            int index = i; 
            string text=  inputFields[index].text;
            string key = inputFields[index].name.ToLower();
            form.AddField(key, text);
        }
        StartCoroutine(axios.Post("/auth/signin", form,onSuccess, onError));
    }

    void onSuccess(UnityWebRequest www){
        string refreshToken = www.GetResponseHeader("Set-Cookie");
        refreshToken = refreshToken.Split("; ")[0]  + ";";
        TokenManager.SetToken("refreshToken",refreshToken);
        TokenManager.SetToken("accessToken",TokenInfo.CreateFromJSON(www.downloadHandler.text).accessToken);
        SceneManager.LoadScene("MainScene");
    }


    void onError(string error){
        Debug.LogError(error);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
