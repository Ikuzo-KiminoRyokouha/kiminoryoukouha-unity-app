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

	// Update is called once per frame
	void Update()
	{
		Vector3 origin = gameObject.transform.position;
		if (origin.y < FloorY + 0.5f)
		{
			gameObject.transform.position = new Vector3(origin.x, origin.y + 0.05f, origin.z);
		}

	}

	public void OnPointerClick(PointerEventData eventData)
	{
		NavigationProvider.goNext();
	}
}
