using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationStopButtonBehaviour : MonoBehaviour
{
	NavigationProvider NavigationProvider;
	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void onClick()
	{

		NavigationProvider obj = GameObject.Find("NavigationManager").GetComponent<NavigationProvider>();
		obj.IsRender = false;
	}
}
