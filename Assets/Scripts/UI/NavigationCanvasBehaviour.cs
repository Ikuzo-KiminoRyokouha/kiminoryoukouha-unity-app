using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NavigationCanvasBehaviour : MonoBehaviour
{
	public GameObject NavigationSearchCanvas;
	public GameObject MenuAnchor;


	private TMP_InputField DestInput;

	// Start is called before the first frame update
	void Start()
	{
		DestInput = transform.GetChild(0).Find("NavigationEndInput").GetComponent<TMP_InputField>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void onSubmit()
	{
		NavigationProvider obj = GameObject.Find("NavigationManager").GetComponent<NavigationProvider>();
		obj.IsRender = true;
		cleanUp();
	}

	public void onCancle()
	{
		cleanUp();
	}

	void cleanUp()
	{
		DestInput.text = "";
		NavigationSearchCanvas.SetActive(false);
		MenuAnchor.SetActive(true);
	}


}
