using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using System.Linq;

public class NavigatorMinimap : MonoBehaviour
{
    private Transform _cameraTransform;
    [SerializeField] private GameObject PlayerPosition;

    [SerializeField] private GameObject DirectionArrow;
    [SerializeField] private GameObject NextDistance0;



    public bool drawPending = false;
    private bool isDrawed = false;
    private float _maxX;
    private float _minX;
    private float _maxY;
    private float _minY;
    private UILineRenderer lineConnector;

    float xAbsMax = 0f;
    float zAbsMax = 0f;

    // Start is called before the first frame update
    void Start()
    {
        lineConnector = GameObject.Find("ARMapRenderer").GetComponent<UILineRenderer>();

        Rect parentRect = transform.GetComponentInParent<RectTransform>().rect;

        _maxX = parentRect.width / 2;
        _minX = -parentRect.width / 2;
        _maxY = parentRect.height / 2;
        _minY = parentRect.height / 2;


    }

    public void drawMap()
    {
        List<Vector2> arr = new List<Vector2>();


        List<GameObject> LineList = GameObject.Find("NavigationManager").GetComponent<NavigationProvider>().LineList;
        Debug.Log(LineList);


        for (int i = 0; i < LineList.Count; i++)
        {
            Vector3[] positions = new Vector3[2];
            LineList[i].GetComponent<LineRenderer>().GetPositions(positions);
            for (int j = 0; j < positions.Length; j++)
            {
                if (xAbsMax < Mathf.Abs(positions[j].x))
                {
                    xAbsMax = Mathf.Abs(positions[j].x);
                }
                if (zAbsMax < Mathf.Abs(positions[j].z))
                {
                    zAbsMax = Mathf.Abs(positions[j].z);
                }
            }
        }

        for (int i = 0; i < LineList.Count; i++)
        {
            Vector3[] positions = new Vector3[2];
            LineList[i].GetComponent<LineRenderer>().GetPositions(positions);
            Debug.Log(positions);
            for (int j = 0; j < positions.Length; j++)
            {
                arr.Add(new Vector2(positions[j].x * (_maxX / xAbsMax) - 15f, positions[j].z * (_maxY / zAbsMax) - 25f));
            }
        }

        lineConnector.Points = arr.ToArray().Distinct().ToArray(); ;
    }

    void checkDirection()
    {
        Vector3 direction = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized;
        Vector3 what = new Vector3((lineConnector.Points[1].x + 15f) * (xAbsMax / _maxX), 0, (lineConnector.Points[1].y + 25f) * (zAbsMax / _maxY)) - _cameraTransform.position;
        float Dot = Vector3.Dot(direction, what);
        Debug.Log(direction);
        Debug.Log(what);
        Debug.Log(Dot);

        float Angle = Mathf.Acos(Dot);

        Debug.Log(GetAngle(direction, what));
    }

    public float GetAngle(Vector3 from, Vector3 to)
    {
        Vector3 v = to - from;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }


    // Update is called once per frame
    void Update()
    {
        if (drawPending && !isDrawed)
        {
            drawMap();
        }

        if (drawPending && isDrawed) { }


        if (_cameraTransform == null)
        {
            _cameraTransform = GameObject.Find("CenterCamera").transform;
        }
        if (_cameraTransform != null && xAbsMax != 0 && zAbsMax != 0)
        {
            Vector3 targetPosition = _cameraTransform.position;
            // PlayerPosition.transform.AnchoredPosition = new Vector3(targetPosition.x * (_maxX / xAbsMax) - 15f, 0, targetPosition.z * (_maxY / zAbsMax) - 25f);
            PlayerPosition.GetComponent<RectTransform>().anchoredPosition = new Vector2(targetPosition.x * (_maxX / xAbsMax) - 15f, targetPosition.z * (_maxY / zAbsMax) - 25f);
            checkDirection();
        }
    }


}
