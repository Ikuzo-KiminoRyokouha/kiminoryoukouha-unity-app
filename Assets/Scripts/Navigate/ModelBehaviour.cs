using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;

public class ModelBehaviour : MonoBehaviour, IPointerClickHandler
{
	private float FloorY = -1.5f;
	NavigationProvider NavigationProvider;

	[SerializeField] private Transform _cameraTransform;

	// Start is called before the first frame update
	void Start()
	{
		Confirm Floor = GameObject.Find("ConfirmManager")?.GetComponent<Confirm>();
		if (Floor != null)
		{
			FloorY = Floor.ReticleVector.y;
		}
		GameObject NavigationManager = GameObject.Find("NavigationManager");
		NavigationProvider = NavigationManager.GetComponent<NavigationProvider>();
	}

	private Vector3 origin;
	private bool IsInit = false;
	// Update is called once per frame
	void Update()
	{
		if (_cameraTransform == null)
		{
			_cameraTransform = GameObject.Find("CenterCamera").transform;
		}


		Vector3 wearerPos = _cameraTransform.position;
		if (modelInit())
		{
			gameObject.transform.position = new Vector3(origin.x, origin.y + wearerPos.y, origin.z);
		}

	}

	bool modelInit()
	{
		Vector3 ori = gameObject.transform.position;

		if (ori.y < FloorY + 0.5f && !IsInit)
		{
			gameObject.transform.position = new Vector3(ori.x, ori.y + 0.05f, ori.z);
			return IsInit;
		}
		else
		{
			if (!IsInit)
			{
				origin = ori;
				IsInit = true;
			}
			return IsInit;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		NavigationProvider.goNext();
	}
}
