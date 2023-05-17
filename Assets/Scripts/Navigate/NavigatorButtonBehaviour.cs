using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using CustomMath;
using Network;
using DataTypes;
using Newtonsoft.Json;
using GPS;


using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Debug = UnityEngine.Debug;
using Image = UnityEngine.UI.Image;
using Color = UnityEngine.Color;
using static LatLng;
using static Confirm;


public class NavigatorButtonBehaviour : MonoBehaviour
{
    private TMP_Text ButtonText;
    private bool IsPendingEffect = false;
    private bool IsCreated = false;

    private float? FloorY = -1.5f;
    private GameObject GpsManager;

    private float minAccuracy = 100f;
    private GameObject cam;

    private float[,] CoordArr = new float[3, 2] { { 0f, 0f }, { 5.5f, 0f }, { 4f, -20f } };

    [SerializeField] private GameObject AlertPrefab;
    [SerializeField] private GameObject NavigatorManagerPrefab;

    private GameObject AlertObject;
    private GameObject CreatedObject;

    // Start is called before the first frame update
    void Start()
    {
        RoutesRequest();
        cam = GameObject.Find("CenterCamera");

        GpsManager = GameObject.Find("GpsManager");
        ButtonText = transform.Find("NavigatorButtonText").gameObject.GetComponent<TMP_Text>();
        ButtonText.text = "길찾기 시작";
        Confirm Floor = GameObject.Find("ConfirmManager")?.GetComponent<Confirm>();
        FloorY = Floor?.ReticleVector.y;
        gameObject.GetComponent<Image>().color = new Color32(75, 238, 130, 255);
    }

    // Update is called once per frame
    void Update()
    {
        double latitude = GpsManager.GetComponent<GpsProvider>().latitude;
        double longitude = GpsManager.GetComponent<GpsProvider>().longitude;

        float accuracy = GpsManager.GetComponent<GpsProvider>().accuracy;

        // // 위치 보안 로직
        // if (IsCreated)
        // {
        //     if (latitude != 0.0f && longitude != 0.0f)
        //     {
        //         if (accuracy != 0.0f && minAccuracy > accuracy)
        //         {
        //             minAccuracy = accuracy;
        //             destroyNavigateObject();
        //             renderNavigateObjectTest(new LatLng(latitude, longitude), new LatLng(35.9474220, 128.463775));
        //         }
        //     }
        // }

        // 해당 오브젝트가 카메라를 벗어났을 때, 경고를 띄워줌
        if (checkObjectInCamera())
        {
            if (AlertObject != null)
            {
                Destroy(AlertObject);
                AlertObject = null;
            }
        }
        else
        {
            if (AlertObject == null)
            {
                AlertObject = Instantiate(AlertPrefab);
            }
        }

        // 클릭 이벤트가 발생시 어떻게 처리해야되는지에 대한 로직
        if (IsPendingEffect)
        {
            IsPendingEffect = false;

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

    bool checkObjectInCamera()
    {
        if (LineList.Count != 0)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam.GetComponent<Camera>());
            bool isVisible = GeometryUtility.TestPlanesAABB(planes, LineList[0].GetComponent<Renderer>().bounds);
            // Do something if the object is visible
            return isVisible;
        }
        return true;

    }
    private void OnStartNavigate()
    {
        double latitude = GpsManager.GetComponent<GpsProvider>().latitude;
        double longitude = GpsManager.GetComponent<GpsProvider>().longitude;

        gameObject.GetComponent<Image>().color = new Color32(233, 60, 43, 255);
        ButtonText.text = "길찾기 중지";

        // LatLng origin = new LatLng(latitude, longitude);
        // LatLng destination = new LatLng(35.9474220, 128.463775);
        // renderNavigateObject(origin, destination);

        for (int i = 0; i < CoordArr.GetLength(0) - 1; i++)
        {
            // renderNavigateObjectTest(new Vector3(CoordArr[i, 0], 0, CoordArr[i, 1]), new Vector3(CoordArr[i + 1, 0], 0, CoordArr[i + 1, 1]));
        }

        IsPendingEffect = true;
    }

    private void OnStopNavigate()
    {
        gameObject.GetComponent<Image>().color = new Color32(75, 238, 130, 255);
        ButtonText.text = "길찾기 시작";
        destroyNavigateObject();
        IsPendingEffect = true;
    }

    public List<GameObject> LineList = new List<GameObject>();
    public List<GameObject> ModelList = new List<GameObject>();
    private LatLng MyCoord;

    private int order = 0;
    [SerializeField] private GameObject ModelPrefab;


    private void renderNavigateObjectTest(Vector3 stanCoord, Vector3 realCoord, string name)
    {
        order = 0;
        // 라인 객체 생성
        GameObject line = new GameObject();

        // 해당 라인의 이름
        line.name = name;

        // 선의 높이 조정 -> -1로 되있는 부분은 나중에 대상 객체의 높이를 받아오면 그걸로 설정하면 될듯함.
        line.transform.position = new Vector3(0, -1f, 0);


        // 라인렌더러 컴포넌트 추가
        line.AddComponent<LineRenderer>();

        LineRenderer lr = line.GetComponent<LineRenderer>();

        // 라인 매터리얼 설정
        Material arrowMat = Resources.Load<Material>("Materials/Arrow");

        lr.material = arrowMat;

        // 라인의 길이 설정
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;

        // 라인 색상 설정
        lr.startColor = Color.white;
        lr.endColor = Color.white;

        //라인의 포지션 설정
        lr.SetPosition(0, stanCoord);
        lr.SetPosition(1, realCoord);

        // 이거 뭐였지.. 기억안나는데
        lr.useWorldSpace = false;

        // 라인에 라인 움직임 정의 컴포넌트 추가
        line.AddComponent<LineBehaviour>();

        // 라인 오브젝트를 밤은 배열에 만든 라인을 추가
        LineList.Add(line);

        if (line.name != "Start")
        {
            line.SetActive(false);
        }

        // 라인 오브젝트의 끝지점에 확인 오브젝트를 추가
        renderModel(lr);
    }

    private void destroyNavigateObject()
    {
        // 라인 배열에 있는 게임 오브젝트 다 삭제
        LineList.ForEach(Destroy);
        LineList.Clear();

        // 모델 배열에 있는 게임 오브젝트 다 삭제
        ModelList.ForEach(Destroy);
        ModelList.Clear();
    }

    void renderModel(LineRenderer lr)
    {
        Vector3 destCoord = lr.GetPosition(1);
        Vector3 originCoord = lr.GetPosition(0);
        destCoord.y = -1;
        float x = destCoord.x - originCoord.x;
        float z = destCoord.z - originCoord.z;
        float degree = Mathf.Atan2(z, x) * Mathf.Rad2Deg;
        GameObject model = Instantiate(ModelPrefab, destCoord, Quaternion.identity);
        model.AddComponent<ModelBehaviour>();
        if (ModelList.Count() != 0)
        {
            model.SetActive(false);
        }
        model.transform.Find("Body Basemesh").gameObject.transform.rotation = Quaternion.Euler(new Vector3(-90, degree, 0));
        ModelList.Add(model);
    }

    void RoutesRequest()
    {
        Axios axios = new Axios("https://apis.openapi.sk.com/tmap");
        Dictionary<string, string> headers = new Dictionary<string, string>();
        WWWForm form = new WWWForm();
        form.AddField("startX", "128.4611931");
        form.AddField("startY", "35.945906");
        form.AddField("endX", "128.4639955");
        form.AddField("endY", "35.9473421");
        form.AddField("reqCoordType", "WGS84GEO");
        form.AddField("resCoordType", "WGS84GEO");
        form.AddField("startName", "출발지");
        form.AddField("endName", "도착지");
        headers["appKey"] = "l7xxbefea111d09640dab1bf5fec3a669c50";
        StartCoroutine(axios.Post("/routes/pedestrian?version=1&format=json&callback=result", form, onSuccess, onError, false, headers));
    }

    private void renderRoutesLine(WalkRoute routes)
    {
        List<List<double>> coordinates = new List<List<double>>();

        // 좌표를 배열안에 저장
        for (int i = 0; i < routes.Features.Count(); i++)
        {
            Geometry geo = routes.Features[i].Geometry;
            for (int j = 0; j < geo.Coordinates.Count(); j++)
            {
                List<double> coordinate = new List<double>();
                string type = geo.Type;
                if (type == "Point")
                {
                    if (j == 1)
                    {
                        continue;
                    }
                    coordinate.Add(geo.Coordinates[j].Double ?? 0.0d);
                    coordinate.Add(geo.Coordinates[j + 1].Double ?? 0.0d);
                }
                else if (type == "LineString")
                {
                    if (j == 0 && i != 0)
                    {
                        continue;
                    }
                    for (int k = 0; k < 2; k++)
                    {

                        coordinate.Add(geo.Coordinates[j].DoubleArray[k]);
                    }
                }
                coordinates.Add(coordinate);
            }
        }

        // 배열안에 저장한 좌표를 유니티 좌표로 전부 변환
        Vector2 originPos = new Vector2(35.945906f, 128.4611931f);
        GPS.GPSEncoder.SetLocalOrigin(originPos);
        List<Vector3> pointVectorArr = new List<Vector3>();
        pointVectorArr.Add(new Vector3(0, 0, 0));
        for (int i = 0; i < coordinates.Count(); i++)
        {
            Vector3 pointVector = GPS.GPSEncoder.GPSToUCS((float)coordinates[i][1], (float)coordinates[i][0]);
            pointVectorArr.Add(pointVector);
        }

        // 그리기
        for (int i = 0; i < pointVectorArr.Count() - 1; i++)
        {
            string lineName = "";
            if (i == 0)
            {
                lineName = "Start";
            }
            else if (i == pointVectorArr.Count() - 2)
            {
                lineName = "End";
            }
            else
            {
                lineName = "Line" + i.ToString();
            }
            renderNavigateObjectTest(pointVectorArr[i], pointVectorArr[i + 1], lineName);
        }
    }

    private void onSuccess(UnityWebRequest www)
    {
        string res = www.downloadHandler.text;
        WalkRoute routes = JsonConvert.DeserializeObject<WalkRoute>(res, Converter.Settings);
        renderRoutesLine(routes);
    }

    private void onError(string error)
    {
        Debug.Log("error : " + error);
    }

    public void goNext()
    {
        if (order == LineList.Count() - 1)
        {
            //끝
        }
        LineList[order].SetActive(false);
        ModelList[order].SetActive(false);
        order = order + 1;
        LineList[order].SetActive(true);
        ModelList[order].SetActive(true);
    }

}
