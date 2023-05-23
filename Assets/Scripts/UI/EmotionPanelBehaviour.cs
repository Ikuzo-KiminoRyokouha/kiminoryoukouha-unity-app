using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class EmotionPanelBehaviour : MonoBehaviour
{
	public string content;
	public string emotion;
	public Texture2D[] textureArr;

	// Start is called before the first frame update
	void Start()
	{
		TMP_Text emotionText = transform.GetChild(0).GetChild(0).Find("EmotionText").gameObject.GetComponent<TMP_Text>();
		RawImage emoticon = transform.GetChild(0).GetChild(0).Find("EmotionEmoticon").gameObject.GetComponent<RawImage>();
		emotionText.text = content;


		if (emotion == "Angry")
		{
			emoticon.texture = textureArr[0];
		}
		else if (emotion == "Heart")
		{
			emoticon.texture = textureArr[1];
		}
		else if (emotion == "Laugh")
		{
			emoticon.texture = textureArr[2];
		}
		else if (emotion == "Simuruk")
		{
			emoticon.texture = textureArr[3];
		}
		else if (emotion == "Surprise")
		{
			emoticon.texture = textureArr[4];
		}

	}

	// Update is called once per frame
	void Update()
	{

	}
}
