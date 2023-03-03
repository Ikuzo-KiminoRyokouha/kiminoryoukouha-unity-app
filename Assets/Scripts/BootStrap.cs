using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


using static Axios;
using static TokenManager;

public class BootStrap : MonoBehaviour
{
    private Axios axios;

    // Start is called before the first frame update
    void Start(){
        axios = new Axios();
        StartCoroutine(axios.Get("/users", onSuccess, onError,true));
    }

    // Update is called once per frame
    void Update(){
        
    }

    void onSuccess(UnityWebRequest www){
        SceneManager.LoadScene("MainScene");
    }

    void onError(string error){

    }
}
