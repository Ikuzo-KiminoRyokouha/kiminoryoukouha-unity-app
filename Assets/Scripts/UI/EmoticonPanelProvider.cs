using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GPS;

public class EmoticonPanelProvider : MonoBehaviour
{
	public GameObject EmoticonPanelPrefab;

	public List<Vector2> Coordinates;

	// Start is called before the first frame update
	void Start()
	{
		Vector2 originPos = new Vector2(35.945906f, 128.4611931f);
		GPS.GPSEncoder.SetLocalOrigin(originPos);
		renderEmotionPanel();
	}

	// Update is called once per frame
	void Update()
	{

	}


	public void renderEmotionPanel()
	{
		string[,] testData = new string[6, 2] { { "Laugh", "되게좋다 여기.." }, { "Simuruk", "생각과는 좀 달라..." }, { "Surprise", "여기에 이런곳이 있었구나..!" }, { "Surprise", "비행기가 있는데? ㅋㅋㅋ" }, { "Angry", "아... 학교가기 싫다" }, { "Laugh", "빙글빙글 회전 로타리~" } };
		for (int i = 0; i < Coordinates.Count; i++)
		{
			Vector3 pointVector = GPS.GPSEncoder.GPSToUCS((float)Coordinates[i].y, (float)Coordinates[i].x);
			GameObject prefab = Instantiate(EmoticonPanelPrefab, pointVector, Quaternion.identity);
			prefab.GetComponent<EmotionPanelBehaviour>().emotion = testData[i, 0];
			prefab.GetComponent<EmotionPanelBehaviour>().content = testData[i, 1];
			prefab.GetComponent<EmotionPanelBehaviour>().IsStickyCamera = true;
		}
	}
}
