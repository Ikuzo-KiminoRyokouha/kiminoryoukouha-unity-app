using UnityEngine;
using NRKernal;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;
using Network;


public class NRInputTest : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Axios axios;
    // Start is called before the first frame update
    void Start()
    {
        axios = new Axios();
        StartCoroutine(axios.Get("/users", onSuccess, onError, true));
    }

    // Update is called once per frame
    void Update()
    {
    }


    void onSuccess(UnityWebRequest www)
    {
        Debug.Log("user : " + www.downloadHandler.text);
    }

    void onError(string error)
    {
        Debug.Log("error : " + error);
    }

    public void OnPointerEnter(PointerEventData e)
    {
        Debug.Log("onPointerEnterCalled : " + e);
    }

    public void OnPointerClick(PointerEventData e)
    {
        Debug.Log("onPointerClickCalled : " + e);
    }

    public void OnPointerExit(PointerEventData e)
    {
        Debug.Log("onPointerExitCalled : " + e);
    }
}
