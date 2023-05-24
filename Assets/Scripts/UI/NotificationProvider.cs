using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationProvider : MonoBehaviour
{
	public GameObject NotificationPrefab;

	public bool RequestPending = false;

	public string text = "";

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if (RequestPending)
		{
			GameObject obj = Instantiate(NotificationPrefab);
			obj.GetComponent<NotificationPopUpBehaviour>().text = text;
			RequestPending = false;
		}
	}
}
