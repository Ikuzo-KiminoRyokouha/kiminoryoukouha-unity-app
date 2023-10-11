using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Network;
using DataTypes;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine.Networking;

public class NavigationProvider : MonoBehaviour
{

	private float FloorY = -1.5f;

	public List<GameObject> LineList = new List<GameObject>();
	public List<GameObject> ModelList = new List<GameObject>();
	private LatLng MyCoord;

	private int order = 0;
	[SerializeField] private GameObject ModelPrefab;
	[SerializeField] private GameObject ARMapRenderer;
	[SerializeField] private GameObject ARMapLabel;


	public bool IsRender = false;

	private bool PrevIsRender = false;
	private GameObject NavigationStopButton;

	private GameObject NotificationManager;

	// Start is called before the first frame update
	void Start()
	{
		NotificationManager = GameObject.Find("NotificationManager");
		NavigationStopButton = GameObject.Find("NavigationStopButton");
		// ARMapRenderer = GameObject.Find("ARMapRenderer");
		// ARMapLabel = GameObject.Find("ARMapLabel");
		ToggleNavigationStopButtonVisible(false);
		Confirm Floor = GameObject.Find("ConfirmManager")?.GetComponent<Confirm>();
		if (Floor != null)
		{
			FloorY = Floor.ReticleVector.y;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (IsRender != PrevIsRender && IsRender == true)
		{
			PrevIsRender = IsRender;
			RoutesRequest();
			OnNotificate("道探しが始まりました");
			GameObject.Find("ARMap").transform.Find("ARMapInfoPannel").gameObject.SetActive(true);
			GameObject.Find("ARMap").transform.Find("ARPlayer").gameObject.SetActive(true);
			ToggleNavigationStopButtonVisible(IsRender);
		}
		else if (IsRender != PrevIsRender && IsRender == false)
		{
			PrevIsRender = IsRender;
			destroyNavigateObject();
			OnNotificate("道探しが終了しました.");

			ToggleNavigationStopButtonVisible(IsRender);
		}
	}

	void OnNotificate(string text)
	{
		NotificationProvider NotificationProvider = NotificationManager.GetComponent<NotificationProvider>();
		NotificationProvider.text = text;
		NotificationProvider.RequestPending = true;
	}
	void ToggleNavigationStopButtonVisible(bool visible)
	{
		NavigationStopButton.SetActive(visible);
	}

	void RoutesRequest()
	{
		Debug.Log("how much this function called");
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
		// headers["appKey"] = "5iTqFA4zDo1dZYLISgxvV4801EKX0ozN4tL6Uhwr";
		StartCoroutine(axios.Post("/routes/pedestrian?version=1&format=json&callback=result", form, onSuccess, onError, false, headers));
	}

	private void onSuccess(UnityWebRequest www)
	{
		string res = www.downloadHandler.text;
		Debug.Log("res : " + res);
		WalkRoute routes = JsonConvert.DeserializeObject<WalkRoute>(res, Converter.Settings);
		renderRoutesLine(routes);
	}

	private void onError(string error)
	{
		Debug.Log("error : " + error);
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
		for (int i = 0; i < coordinates.Count; i++)
		{
			Vector3 pointVector = GPS.GPSEncoder.GPSToUCS((float)coordinates[i][1], (float)coordinates[i][0]);
			pointVectorArr.Add(pointVector);
		}

		// 그리기
		for (int i = 0; i < pointVectorArr.Count - 1; i++)
		{
			string lineName = "";
			if (i == 0)
			{
				lineName = "Start";
			}
			else if (i == pointVectorArr.Count - 2)
			{
				lineName = "End";
			}
			else
			{
				lineName = "Line" + i.ToString();
			}
			renderNavigateObjectTest(pointVectorArr[i], pointVectorArr[i + 1], lineName);
		}

		ARMapLabel.SetActive(true);
		ARMapRenderer.GetComponent<NavigatorMinimap>().drawMap();

	}


	private void renderNavigateObjectTest(Vector3 stanCoord, Vector3 realCoord, string name)
	{
		order = 0;
		// 라인 객체 생성
		GameObject line = new GameObject();

		// 해당 라인의 이름
		line.name = name;
		// 선의 높이 조정 -> -1로 되있는 부분은 나중에 대상 객체의 높이를 받아오면 그걸로 설정하면 될듯함.
		line.transform.position = new Vector3(0, FloorY, 0);


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

		// ARMapRenderer.SetActive(false);
		ARMapLabel.SetActive(false);
	}

	void renderModel(LineRenderer lr)
	{
		Vector3 destCoord = lr.GetPosition(1);
		Vector3 originCoord = lr.GetPosition(0);
		destCoord.y = FloorY;
		float x = destCoord.x - originCoord.x;
		float z = destCoord.z - originCoord.z;
		float degree = Mathf.Atan2(z, x) * Mathf.Rad2Deg;
		GameObject model = Instantiate(ModelPrefab, destCoord, Quaternion.identity);
		model.AddComponent<ModelBehaviour>();
		if (ModelList.Count != 0)
		{
			model.SetActive(false);
		}
		model.transform.Find("Body Basemesh").gameObject.transform.rotation = Quaternion.Euler(new Vector3(-90, degree, 0));
		ModelList.Add(model);
	}
	public void goNext()
	{

		if (order == LineList.Count() - 1)
		{
			// order = 0;
			// //끝
			// IsRender = false;
		}
		LineList[order].SetActive(false);
		ModelList[order].SetActive(false);
		order = order + 1;
		LineList[order].SetActive(true);
		ModelList[order].SetActive(true);
	}


}
