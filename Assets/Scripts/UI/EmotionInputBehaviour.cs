using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmotionInputBehaviour : MonoBehaviour
{
	public GameObject EmotionImage;
	public GameObject EmoticonPanelPrefab;

	public Texture2D[] textureArr;
	[SerializeField] private Transform _cameraTransform;
	private GameObject NotificationManager;

	private string prevEmotion;
	public string emotion;
	void Start()
	{
		NotificationManager = GameObject.Find("NotificationManager");

	}

	void Update()
	{
		if (_cameraTransform == null)
		{
			_cameraTransform = GameObject.Find("CenterCamera").transform;
		}
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
		TMP_Text emotionText = GameObject.Find("EmotionInput").GetComponent<TMP_Text>();
		Vector3 pointVector = _cameraTransform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
		GameObject prefab = Instantiate(EmoticonPanelPrefab, pointVector, Quaternion.identity);
		prefab.GetComponent<EmotionPanelBehaviour>().emotion = emotion;
		prefab.GetComponent<EmotionPanelBehaviour>().content = emotionText.text;
		OnNotificate("댓글이 달렸습니다.");
		gameObject.SetActive(false);
	}

	public void onCancle()
	{
		gameObject.SetActive(false);
	}

	void OnNotificate(string text)
	{
		NotificationProvider NotificationProvider = NotificationManager.GetComponent<NotificationProvider>();
		NotificationProvider.text = text;
		NotificationProvider.RequestPending = true;
	}
}