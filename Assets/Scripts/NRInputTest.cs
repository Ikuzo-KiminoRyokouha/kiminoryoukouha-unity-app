using UnityEngine;
using NRKernal;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;

public class NRInputTest : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {        
    }
    
    public void OnPointerEnter(PointerEventData e){
        Debug.Log("onPointerEnterCalled : " + e);
    }

    public void OnPointerClick(PointerEventData e){
        Debug.Log("onPointerClickCalled : " + e);
    }

    public void OnPointerExit(PointerEventData e){
        Debug.Log("onPointerExitCalled : "  + e);
    }
}
