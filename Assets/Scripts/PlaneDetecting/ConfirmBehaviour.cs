using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.SceneManagement;

public class ConfirmBehaviour : MonoBehaviour
{
    private GameObject ConfirmManager;
    public TMP_Text ConfirmDebugMessage;

    private GameObject PlaneDetector;
    private GameObject Reticle;

    // 오브젝트가 불려오기 전에 호출
    void Awake()
    {
        PlaneDetector = GameObject.Find("PlaneDetector");
        Reticle = GameObject.Find("Reticle");
        Destory(PlaneDetector);
        Destory(Reticle);
    }

    // Start is called before the first frame update
    void Start()
    {
        ConfirmManager = GameObject.Find("ConfirmManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (ConfirmManager.GetComponent<Confirm>().Reticle.CurrentPlane != null)
        {
            // [Debug]
            // ConfirmDebugMessage.text = "혹시 모르니까 ㅎㅎ";
            Vector3 v = ConfirmManager.GetComponent<Confirm>().ReticleVector;
            ConfirmDebugMessage.text = v.ToString();
        }
        else
        {
            // [Debug]
            // ConfirmDebugMessage.text = "일단 뭐.. 그렇게 됐습니다.";
        }

    }

    // 삭제되기 전에 호출되는 함수
    void OnDestroy()
    {
        Instantiate(PlaneDetector, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(Reticle, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void onResolve()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void onReject()
    {
        Destroy(gameObject);
    }
}
