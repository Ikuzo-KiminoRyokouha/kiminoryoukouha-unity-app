using System.Numerics;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class LineBehaviour : MonoBehaviour
{
	private Material LineMaterial;
	private float FloorY = -1.5f;

	private float startScaleX;

	private float lineLength;

	private float speed = 0.02f;

	private bool IsSpeedUp = false;

	[SerializeField] private Transform _cameraTransform;


	// Start is called before the first frame update
	void Start()
	{
		Confirm Floor = GameObject.Find("ConfirmManager")?.GetComponent<Confirm>();
		if (Floor != null)
		{
			FloorY = Floor.ReticleVector.y;
		}
		LineRenderer lr = gameObject.GetComponent<LineRenderer>();
		LineMaterial = lr.materials[0];
		Vector2 originScale = LineMaterial.GetTextureScale("_MainTex");
		lineLength = Vector3.Distance(lr.GetPosition(1), lr.GetPosition(0));
		startScaleX = lineLength * 10;
		speed = lineLength * 0.02f;
		LineMaterial.SetTextureScale("_MainTex", new Vector2(startScaleX, originScale.y));
	}

	Vector3 origin;
	// Update is called once per frame
	void Update()
	{
		if (_cameraTransform == null)
		{
			_cameraTransform = GameObject.Find("CenterCamera").transform;
		}
		if (origin == null)
		{
			origin = gameObject.transform.position;
		}

		Vector3 wearerPos = _cameraTransform.position;

		gameObject.transform.position = new Vector3(origin.x, FloorY + wearerPos.y, origin.z);




		Vector2 originScale = LineMaterial.GetTextureScale("_MainTex");

		if (originScale.x >= startScaleX + lineLength * 2.0f)
		{
			IsSpeedUp = false;
		}
		else if (originScale.x <= startScaleX - lineLength * 2.0f)
		{
			IsSpeedUp = true;
		}

		Vector2 resultScale = new Vector2();

		if (IsSpeedUp)
		{
			resultScale = originScale + new Vector2(speed, 0.0f);
		}
		else
		{
			resultScale = originScale - new Vector2(speed, 0.0f);

		}


		LineMaterial.SetTextureScale("_MainTex", resultScale);
	}
}
