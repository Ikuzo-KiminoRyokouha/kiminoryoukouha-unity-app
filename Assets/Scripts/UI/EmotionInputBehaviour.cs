using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionInputBehaviour : MonoBehaviour
{
	public GameObject EmotionImage;

	public Texture2D[] textureArr;

	private string prevEmotion;
	public string emotion;
	void Start()
	{
	}

	void Update()
	{
		if (emotion != prevEmotion)
		{
			if (emotion == "Angry")
			{
				EmotionImage.GetComponent<RawImage>().texture = textureArr[0];
			}
			else if (emotion == "Heart")
			{
				EmotionImage.GetComponent<RawImage>().texture = textureArr[1];
			}
			else if (emotion == "Laugh")
			{
				EmotionImage.GetComponent<RawImage>().texture = textureArr[2];
			}
			else if (emotion == "Simuruk")
			{
				EmotionImage.GetComponent<RawImage>().texture = textureArr[3];
			}
			else if (emotion == "Surprise")
			{
				EmotionImage.GetComponent<RawImage>().texture = textureArr[4];
			}
			prevEmotion = emotion;
		}
	}

	public void onSubmit()
	{
		gameObject.SetActive(false);
	}

	public void onCancle()
	{
		gameObject.SetActive(false);
	}
}