using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavigationButtonBehaviour : MonoBehaviour, IPointerClickHandler
{

	public GameObject NavigationSearchCanvas;
	public GameObject MenuAnchor;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}


	public void OnPointerClick(PointerEventData eventData)
	{
		NavigationSearchCanvas.SetActive(true);
		MenuAnchor.SetActive(false);
	}

}
