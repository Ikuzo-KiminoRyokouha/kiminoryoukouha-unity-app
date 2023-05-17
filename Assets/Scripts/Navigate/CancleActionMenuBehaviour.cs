using System.Numerics;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Vector3 = UnityEngine.Vector3;
using Debug = UnityEngine.Debug;
public class CancleActionMenuBehaviour : MonoBehaviour, IPointerClickHandler
{
    public GameObject MenuButton;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("transfrom");
        Debug.Log(transform.localEulerAngles);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localEulerAngles.z < 90f)
            transform.Rotate(Vector3.forward * Time.deltaTime * 180f);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MenuButton.GetComponent<EmotionMenuButtonBehaviour>().onClick();
    }
}
