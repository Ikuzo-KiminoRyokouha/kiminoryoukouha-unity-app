using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Debug = UnityEngine.Debug;


public class NavigatorButtonBehaviour : MonoBehaviour
{
    private TMP_Text ButtonText;
    private bool IsPendingEffect = false;
    private bool IsCreated = false;
    [SerializeField] private GameObject NavigatorManagerPrefab;
    private GameObject CreatedObject;

    // Start is called before the first frame update
    void Start()
    {
        ButtonText = transform.Find("NavigatorButtonText").gameObject.GetComponent<TMP_Text>();
        Debug.Log("ButtonText : " + ButtonText);
        ButtonText.text = "길찾기 시작";
        gameObject.GetComponent<Image>().color = new Color32(75, 238, 130, 255);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPendingEffect)
        {
            if (IsCreated)
            {
                Destroy(CreatedObject);
                CreatedObject = null;
                IsCreated = false;
            }
            else
            {
                CreatedObject = Instantiate(NavigatorManagerPrefab);
                IsCreated = true;

            }
            IsPendingEffect = false;
        }
    }

    public void OnClick()
    {
        if (IsCreated)
        {
            OnStopNavigate();
        }
        else
        {
            OnStartNavigate();
        }
    }

    private void OnStartNavigate()
    {
        gameObject.GetComponent<Image>().color = new Color32(233, 60, 43, 255);
        ButtonText.text = "길찾기 중지";
        IsPendingEffect = true;
    }

    private void OnStopNavigate()
    {
        gameObject.GetComponent<Image>().color = new Color32(75, 238, 130, 255);
        ButtonText.text = "길찾기 시작";
        IsPendingEffect = true;
    }

}
