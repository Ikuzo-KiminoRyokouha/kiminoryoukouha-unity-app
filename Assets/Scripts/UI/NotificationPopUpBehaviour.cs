using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationPopUpBehaviour : MonoBehaviour
{
	private RawImage BackgroundImage;
	private TMP_Text NotificationText;
	public string text;
	// Start is called before the first frame update
	void Start()
	{
		BackgroundImage = transform.GetChild(0).GetChild(0).GetComponent<RawImage>();
		NotificationText = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
		NotificationText.text = text;
		Destroy(gameObject, 3.0f);
	}

	// Update is called once per frame
	void Update()
	{
		Color c = BackgroundImage.color;
		float originAlpha = BackgroundImage.color.a;
		c.a = originAlpha - 1 / (180.0f);
		BackgroundImage.color = c;
		NotificationText.color = c;
	}
}
