using System.Numerics;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DataTypes;
using Network;
using Newtonsoft.Json;
using System;

using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using Debug = UnityEngine.Debug;



public class LoadPlanData : MonoBehaviour
{

    enum PlanType
    {
        ActivatedPlan, WaitingPlan, DonePlan
    }

    // 렌더링할 PlanPanel의 Prefab
    public GameObject PlanPanel;
    // PlanPanel이 렌더링 되야할 부모 캔버스
    public GameObject PlanCanvas;

    private Axios axios;
    // 전체 플랜데이터
    private PlanData planData;

    // 화면에 렌더링 되어있는 PlanPanel을 담는 리스트
    private List<GameObject> RenderedPlanPanel = new List<GameObject>();
    // 활성화 된 계획에 대한 데이터 변수
    private List<Plan> activatePlan = new List<Plan>();

    // 대기 중인 계획에 대한 데이터 변수
    private List<Plan> watingPlan = new List<Plan>();

    // 완료한 계획에 대한 데이터 변수
    private List<Plan> donePlan = new List<Plan>();

    // 현재 선택한 계획 모드에 대한 데이터 변수
    private List<Plan> currentPlan = new List<Plan>();

    private PlanType currentPlanType;

    private PlanType CurrentPlanType
    {
        get { return currentPlanType; }
        set
        {
            switch (value)
            {
                case PlanType.ActivatedPlan:
                    currentPlan = activatePlan;
                    break;
                case PlanType.WaitingPlan:
                    currentPlan = watingPlan;
                    break;
                case PlanType.DonePlan:
                    currentPlan = donePlan;
                    break;
            }
            Debug.Log(currentPlan);
            currentPage = 1;
            renderPlanPanel(currentPlan, currentPage);
            currentPlanType = value;
        }
    }

    private byte currentPage = 1;
    private byte CurrentPage
    {
        get { return currentPage; }
        set
        {
            renderPlanPanel(currentPlan, value);
            currentPage = value;
        }
    }

    void Awake()
    {
        CurrentPlanType = PlanType.ActivatedPlan;
        axios = new Axios();
        StartCoroutine(axios.Get("/plan/all/1", onSuccess, onError, true));
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void onSuccess(UnityWebRequest www)
    {
        string res = www.downloadHandler.text;
        // 받아온 계획데이터를 역직렬화 시킨다.
        planData = JsonConvert.DeserializeObject<PlanData>(res);

        // 플랜 데이터를 3개로 나눈다.
        // 1. 활성화된 계획 -> 시작일이 현재일보다 같거나 크고 종료일이 현재일보다 같거나 작아야한다.
        // 2. 대기중인 계획 -> 시작일이 현재일 보다 커야함
        // 3. 종료된 계획 -> 종료일이 현재일보다 작아야함.

        for (int i = 0; i < planData.plans.Length; i++)
        {

            if (planData.plans[i].start >= DateTime.Now && planData.plans[i].end <= DateTime.Now)
            {
                activatePlan.Add(planData.plans[i]);
            }
            // 2. 대기중인 계획
            else if (planData.plans[i].start <= DateTime.Now)
            {
                watingPlan.Add(planData.plans[i]);
            }
            // 3. 끝난 계획
            else
            {
                donePlan.Add(planData.plans[i]);
            }
        }
    }



    private void renderPlanPanel(List<Plan> plans, byte currentPage)
    {
        // 만약 렌더링 된 PlanPanel이 있다면
        if (RenderedPlanPanel.Count > 0)
        {
            // 해당 리스트를 순회하며 게임 오브젝트 모두를 파괴한다.
            for (byte i = 0; i < RenderedPlanPanel.Count; i++)
            {
                Destroy(RenderedPlanPanel[i]);
            }
            RenderedPlanPanel.Clear();
        }

        for (byte i = 0; i < plans.Count; i++)
        {
            // 게임 오브젝트의 위치 벡터
            Vector2 v2 = new Vector2(0, 0);
            // PlanCanvas의 하위에 렌더링
            GameObject pp = Instantiate(PlanPanel, v2, Quaternion.identity, PlanCanvas.transform);


            // PlanPanel의 RectTransform을 변경
            RectTransform rt = pp.GetComponent<RectTransform>();
            // 리스트에 해당 PlanPanel을 추가 함으로써 onDestroy하기에 용이하게 만듬. 
            RenderedPlanPanel.Add(pp);



            rt.localPosition = new Vector3(rt.localPosition.x, 90 - 40 * i, 0);
        }

    }


    private void onError(string error)
    {
        Debug.Log("error : " + error);
    }

    public void changeToActivate()
    {
        Debug.Log("changeToActivate");
        CurrentPlanType = PlanType.ActivatedPlan;
    }

    public void changeToWaiting()
    {
        Debug.Log("changeToWaiting");
        CurrentPlanType = PlanType.WaitingPlan;
    }

    public void changeToDone()
    {
        Debug.Log("changeToDone");
        CurrentPlanType = PlanType.DonePlan;
    }

    private void goToNextPage()
    {

    }

    private void goToPrevPage()
    {

    }
}